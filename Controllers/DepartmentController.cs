using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AW_UniversityManagementMvcApp.Manager;
using AW_UniversityManagementMvcApp.Models.Entity_Models;

namespace AW_UniversityManagementMvcApp.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentManager aDepartmentManager=new DepartmentManager();
        //
        // GET: /Department/
        
        //To Be Done: Department save(story 1) and View all department(story 2) 
        public ActionResult SaveDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveDepartment(Department aDepartment)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = aDepartmentManager.Save(aDepartment);
            }

            return View();
        }

        public ActionResult ViewAllDepartments()
        {
            ViewBag.department = aDepartmentManager.ViewAllDepartment();
            return View();
        }
        
	}
}