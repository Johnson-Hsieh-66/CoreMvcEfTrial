
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using X.PagedList;
using X.PagedList.Web.Common;

namespace GLRMSPortalApp.ViewModels.Common
{
    public interface IPagination
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }
    public class Pagination<T>
    {
        //Default value
        private string strHttpMethod = "GET";
        private InsertionMode eInsertionMode = InsertionMode.Replace;

        public string HttpMethod { get { return strHttpMethod; } set{ strHttpMethod = value; } }
        public InsertionMode InsertionMode { get { return eInsertionMode; } set { eInsertionMode = value; } }
        [Required]
        public string UpdateTargetId { get ; set ; }//要被ajax更新的div id
        [Required]
        public string ActionPath { get ; set ; }//ajax的route
        public Dictionary<string, object> RouteParams { get; set; } //參數
        public IPagedList<T> PagedContentList { get; set; }
        /// <summary>
        /// 為避免最後一頁內容遭刪除後，仍傳最後一頁，而增加此判斷
        /// </summary>
        /// <param name="tList"></param>
        /// <param name="nPage"></param>
        /// <param name="nPageSize"></param>
        /// <returns></returns>
        public IPagedList<T> SetPagedList(IQueryable<T> tList, int nPage, int nPageSize)
        {
            nPage = nPage < 1 ? 1 : nPage;
            if (tList != null)
            {
                decimal nTotalCount = tList.Count();
                if (!(nTotalCount > ((nPage - 1) * nPageSize)) && nPage > 1)
                    nPage = Convert.ToInt32(Math.Ceiling(nTotalCount / nPageSize));
            }
            IPagedList<T> pagedList = tList.ToPagedList(nPage, nPageSize);
            return pagedList;
        }
    }

}