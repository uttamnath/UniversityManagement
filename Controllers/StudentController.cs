using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AW_UniversityManagementMvcApp.Manager;
using AW_UniversityManagementMvcApp.Models;
using AW_UniversityManagementMvcApp.Models.Entity_Models;

namespace AW_UniversityManagementMvcApp.Controllers
{
    public class StudentController : Controller
    {
        StudentManager aStudentManager = new StudentManager();
        DepartmentManager aDepartmentManager = new DepartmentManager();
        CourseEnrollManager aCourseEnrollManager = new CourseEnrollManager();
        //
        // GET: /Student/
        public ActionResult Index()
        {
            return View();
        }
        //To Be Done: Student Registration(story 7), Enroll in a course (story 10) 
        public ActionResult RegisterStudent()
        {
            ViewBag.department = aDepartmentManager.GetAllDepartment();
            return View();
        }
        [HttpPost]
        public ActionResult RegisterStudent(Student student)
        {
            ViewBag.department = aDepartmentManager.GetAllDepartment();
            ViewBag.Message = aStudentManager.Save(student);
            if (ViewBag.Message == "Registration Successful")
            {
                return RedirectToAction("ShowLastSave");
            }
            return View();
        }

        public ActionResult ShowLastSave()
        {
            Student aStudent = aStudentManager.GetLastSaveStudent();
            ViewBag.Student = aStudent;
            Department aDepartment = aDepartmentManager.GetDepartmentById(aStudent.DepartmentId);
            ViewBag.Department = aDepartment.Name;
            return View();
        }

        public ActionResult CoursesEnroll()
        {
            ViewBag.allStudentRegNo = aCourseEnrollManager.GetAllStudents();

            return View();
        }
        [HttpPost]
        public ActionResult CoursesEnroll(CourseEnroll aCourseEnroll)
        {
            ViewBag.allStudentRegNo = aCourseEnrollManager.GetAllStudents();
            ViewBag.Message = aCourseEnrollManager.SaveEnroll(aCourseEnroll);

            return View();
        }

        public JsonResult GetStudentInfoByStudentId(string studentId)
        {
            CourseEnroll aStudentDetails = aCourseEnrollManager.GetStudentInfoByRegId(studentId);
            return Json(aStudentDetails);

        }
        public JsonResult GetDeptWiseCourses(string studentId)
        {
            var deptWiseCourses = aCourseEnrollManager.GetAllCourses(studentId);
            return Json(deptWiseCourses);

        }
    }
}