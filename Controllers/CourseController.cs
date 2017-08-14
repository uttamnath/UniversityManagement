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
    public class CourseController : Controller
    {
        CourseManager aCourseManager=new CourseManager();
        DepartmentManager aDepartmentManager=new DepartmentManager();
        //
        // GET: /Course/
        public ActionResult Index()
        {
            return View();
        }
        //To Be Done: Course save(story 3),View Course Stat(story 6) 
        public ActionResult SaveCourse()
        {
            ViewBag.department = aDepartmentManager.GetAllDepartment();
            ViewBag.semester = aCourseManager.GetAllSemester();
            return View();

        }
        [HttpPost]
        public ActionResult SaveCourse(Course aCourse)
        {
            
            ViewBag.department = aDepartmentManager.GetAllDepartment();
            ViewBag.semester = aCourseManager.GetAllSemester();
            if (ModelState.IsValid)
            {
                ViewBag.Message = aCourseManager.Save(aCourse);
            }
           
            return View();

        }
        public ActionResult ViewCourseStat()
        {
            ViewBag.department = aDepartmentManager.GetAllDepartment();
            return View();
        }
        [HttpPost]
        public ActionResult ViewCourseStat(CourseAssignVM acCourseAssignVm)
        {
            ViewBag.department = aDepartmentManager.GetAllDepartment();
            return View();
        }
        public JsonResult ViewCourseStatByDept(int departmentId)
        {
            List<CourseStatVM> courseStatList = aCourseManager.ViewCourseStat(departmentId);
            return Json(courseStatList);
        }

        public JsonResult GetAllCourseByDeptId(int departmentId)
        {
            List<Course> allCoursesList = aCourseManager.GetAllCourseByDeptID(departmentId);

            return Json(allCoursesList);
        }
        public JsonResult GetaCourseByCourseId(int courseId)
        {

            Course aCourse = aCourseManager.GetaCourseByCourseId(courseId);

            return Json(aCourse);
        }
	}
}