using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreMvcEfTrial.Models.Entities.Adventureworks;
using CoreMvcEfTrial.Services.Models;

namespace CoreMvcEfTrial.Services.Adventureworks
{
    public interface IAWService
    {
        IQueryable<Employee> GetEmployees(EmployeeQryML oQryML);
    }
}
