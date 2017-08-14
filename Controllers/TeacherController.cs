using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AW_UniversityManagementMvcApp.Manager;
using AW_UniversityManagementMvcApp.Models.Entity_Models;
using AW_UniversityManagementMvcApp.Models.View_Models;

namespace AW_UniversityManagementMvcApp.Controllers
{
    public class TeacherController : Controller
    {
        TeacherManager aTeacherManager=new TeacherManager();
        DepartmentManager aDepartmentManager=new DepartmentManager();
        //
        // GET: /Teacher/
        public ActionResult Index()
        {
            return View();
        }
        //To Be Done: Teacher save(story 6), Course Assign (story 5) and Course Unassign(story 13) 
        public ActionResult SaveTeacher()
        {
            ViewBag.department = aDepartmentManager.GetAllDepartment();
            ViewBag.designation = aTeacherManager.GetAllDesignation();
            return View();

        }
        [HttpPost]
        public ActionResult SaveTeacher(Teacher aTeacher)
        {
            ViewBag.department = aDepartmentManager.GetAllDepartment();
            ViewBag.designation = aTeacherManager.GetAllDesignation();
            if (ModelState.IsValid)
            {
                ViewBag.Message = aTeacherManager.Save(aTeacher);
            }
         
            return View();

        }
        public ActionResult AssignTeacher()
        {
            ViewBag.department = aDepartmentManager.GetAllDepartment();
            return View();
        }
        [HttpPost]
        public ActionResult AssignTeacher(CourseAssignVM aCourseAssignVm)
        {
            ViewBag.department = aDepartmentManager.GetAllDepartment();
            if (ModelState.IsValid)
            {
                ViewBag.Message = aTeacherManager.AssignTeacher(aCourseAssignVm);
            }
            return View();
        }
        public JsonResult GetTeachersByDepartmentId(int departmentId)
        {
            List<TeacherVM> teacherList = aTeacherManager.GetAllTeacherByDepartment(departmentId);


            return Json(teacherList);
        }
        public JsonResult GetTeacherInfoByTeacherId(int teacherId)
        {
            TeacherVM ateacher = aTeacherManager.GetTeacherInfoByTeacherId(teacherId);

            return Json(ateacher);
        }
        public ActionResult UnassignCourse()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnassignCourse(string status)
        {
            ViewBag.Unassign = aTeacherManager.UnassignAllCourses();
            return View();
        }

	}
}