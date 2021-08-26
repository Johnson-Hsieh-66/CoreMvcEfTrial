using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMvcEfTrial.Models.Entities.Adventureworks;

namespace CoreMvcEfTrial.ViewModels
{
    public class EmployeeVML:IPagination
    {
        public int BusinessEntityId { get; set; }
        public string JobTitle { get; set; }
        [MapColumn("BirthDate")]
        public DateTime BirthDate { get; set; }
        [MapColumn("Gender")]
        public string Gender { get; set; }
        public int Page { get; set; }
        private int nPageSize = 10;
        public int PageSize { get { return nPageSize; } set { nPageSize = value; } }
        public IQueryable<Employee> ListEmployees { get; set; }
        private Pagination oPaginationModel = new Pagination();
        public Pagination PaginationModel { get { return oPaginationModel; } set { oPaginationModel = value; } }
        [MapColumn("bSearch")]
        public bool bSearch { get; set; }
    }
}
