using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using X.PagedList;
using X.PagedList.Web.Common;

namespace CoreMvcEfTrial.ViewModels
{
    /// <summary>
    /// Let viewmodel to inherit this interface. Just avoid for forgetting to add those properties.
    /// </summary>
    public interface IPagination
    {
        int Page { get; set; }
        int PageSize { get; set; }
        Pagination PaginationModel { get; set; }
    }
    public class Pagination
    {
        //Default value
        private string strHttpMethod = "GET";
        private InsertionMode eInsertionMode = InsertionMode.Replace;
        private string strUpdateTargetId = "PagedListTable";

        public string HttpMethod { get { return strHttpMethod; } set{ strHttpMethod = value; } }
        public InsertionMode InsertionMode { get { return eInsertionMode; } set { eInsertionMode = value; } }
        [Required]
        public string UpdateTargetId { get { return strUpdateTargetId; } set { strUpdateTargetId = value; } }//要被ajax更新的div id
        [Required]
        public string ActionPath { get ; set ; }//ajax的route
        public Dictionary<string, object> RouteParams { get; set; } //參數
        public IPagedList<dynamic> PagedContentList { get; set; }//放被轉成IPagedList型態的資料
        /// <summary>
        /// 為避免最後一頁內容遭刪除後，仍傳最後一頁，而增加此判斷
        /// </summary>
        /// <param name="oList"></param>
        /// <param name="nPage"></param>
        /// <param name="nPageSize"></param>
        /// <returns></returns>
        static public IPagedList<dynamic> SetPagedList(IQueryable<dynamic> oList, int nPage, int nPageSize)
        {
            nPage = nPage < 1 ? 1 : nPage;
            if (oList != null)
            {
                decimal nTotalCount = oList.Count();
                if (!(nTotalCount > ((nPage - 1) * nPageSize)) && nPage > 1)
                    nPage = Convert.ToInt32(Math.Ceiling(nTotalCount / nPageSize));
            }
            IPagedList<dynamic> pagedList = oList.ToPagedList(nPage, nPageSize);
            return pagedList;
        }
       
        static public Pagination SetPagedRouteAndQryML( Pagination oPaginationModel, dynamic oVML,dynamic oQryML,bool bCheckColumnAttribute)
        {
            Dictionary<string, object> oRouteParams = new Dictionary<string, object>();
            foreach (var oPropertyInfo in oVML.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (bCheckColumnAttribute)
                {
                    Object[] oAttribute = oPropertyInfo.GetCustomAttributes(typeof(MapColumnAttribute), true);
                    MapColumnAttribute oColumnAttribute = (MapColumnAttribute)oAttribute.FirstOrDefault();
                    if (oColumnAttribute == null || string.IsNullOrEmpty(oColumnAttribute.ColumnName) || oColumnAttribute.TypeKey == MapKeyType.Action)
                        continue;
                }
                var strKey = oPropertyInfo.Name;
                object oValue = oVML.GetType().GetProperty(strKey).GetValue(oVML);
                var oVmlPropertyInfo = oQryML!=null? oQryML.GetType().GetProperty(strKey):null;
                if (oValue != null)
                {
                    oRouteParams.Add(strKey, oValue);
                    if (oVmlPropertyInfo != null)
                        oVmlPropertyInfo.SetValue(oQryML, oValue);
                }
            }
            oVML.PaginationModel.RouteParams = oRouteParams;
            return oPaginationModel;
        }
    }
}