using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoreMvcEfTrial.Models.Entities.Adventureworks;
using CoreMvcEfTrial.Services.Adventureworks;
using CoreMvcEfTrial.Services.Models;

namespace CoreMvcEfTrial.Controllers
{
    public class AdventureWorksController : Controller
    {
        private AdventureworksContext m_oEntities = new AdventureworksContext();
        private IAWService _oAwService;
        public AdventureWorksController()
        {
            _oAwService = new AWService(m_oEntities);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListEmployee(EmployeeQryML oQryML)
        {
            var list = _oAwService.GetEmployees(oQryML);

            return View(list);
        }
    }
}
