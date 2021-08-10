using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreMvcEfTrial.Models;
using CoreMvcEfTrial.Models.Entities.Adventureworks;
using CoreMvcEfTrial.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcEfTrial.Services.Adventureworks
{
    public class AWService : IAWService
    {
        IRepository<Employee> _oEmployeeRepo;
        private DbContext DbContext;
        public AWService(DbContext context)
        {
            DbContext = context;
            _oEmployeeRepo = new GenericRepository<Employee>(context);
        }
        public IQueryable<Employee> GetEmployees(EmployeeQryML oQryML)
        {
            var oResult = _oEmployeeRepo.GetAllQuerable();
            if (!string.IsNullOrEmpty(oQryML.Gender))
                oResult = oResult.Where(e => e.Gender==oQryML.Gender);
            return oResult;
        }
    }
}
