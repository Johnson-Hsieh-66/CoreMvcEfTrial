using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoreMvcEfTrial.Models.Entities.Adventureworks;
using CoreMvcEfTrial.Services.Adventureworks;
using CoreMvcEfTrial.Services.Models;
using CoreMvcEfTrial.ViewModels;
using System.Reflection;

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
        public IActionResult ListEmployee(EmployeeVML oEmployeeVML)
        {
            if (oEmployeeVML.bSearch == true)
            {
                EmployeeQryML oQryContent = new EmployeeQryML();
                oEmployeeVML.PaginationModel= Pagination.SetPagedRouteAndQryML(oEmployeeVML.PaginationModel, oEmployeeVML,oQryContent, true);
                oEmployeeVML.ListEmployees = _oAwService.GetEmployees(oQryContent);
                if (oEmployeeVML.ListEmployees != null)
                    oEmployeeVML.PaginationModel.PagedContentList = Pagination.SetPagedList(oEmployeeVML.ListEmployees, oEmployeeVML.Page, oEmployeeVML.PageSize);
            }
            bool isAjaxCall = HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return isAjaxCall ? (ActionResult)PartialView("_PartialListEmployee", oEmployeeVML) : View("ListEmployee", oEmployeeVML);
        }
    }
}
