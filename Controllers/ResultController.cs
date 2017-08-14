using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AW_UniversityManagementMvcApp.Manager;
using AW_UniversityManagementMvcApp.Models;
using AW_UniversityManagementMvcApp.Models.View_Models;

namespace AW_UniversityManagementMvcApp.Controllers
{
    public class ResultController : Controller
    {
        ResultManager aResultManager = new ResultManager();
        //
        // GET: /Result/
        public ActionResult Index()
        {
            return View();
        }
        //To Be Done: Result save(story 11), View Result (story 12) 

        public ActionResult SaveResult()
        {
            ViewBag.Students = aResultManager.GetAllStudents();
            ViewBag.Results = aResultManager.GetAllGrades();
            return View();
        }

        [HttpPost]
        public ActionResult SaveResult(ResultVM student)
        {
            ViewBag.Students = aResultManager.GetAllStudents();
            ViewBag.Message = aResultManager.SaveResult(student);
            ViewBag.Results = aResultManager.GetAllGrades();
            return View();
        }

        public JsonResult GetStudentsById(int Id)
        {
            
            var students = aResultManager.GetAllStudents();
            var student = students.Find(a => a.Id == Id);
            
            return Json(student);
        }

        public JsonResult GetEnrolledCoursesById(int Id)
        {
            var courses = aResultManager.GetEnrollCourses(Id);
            return Json(courses);
        }



        public ActionResult ViewResult()
        {

            ViewBag.Students = aResultManager.GetAllStudents();
            //ViewBag.RegNo ="Value ta asbe 11 theke....Kora ase";
            return View();
        }
        [HttpPost]
        public ActionResult ViewResult(ResultVM result)
        {

            ViewBag.Students = aResultManager.GetAllStudents();
            //ViewBag.RegNo ="Value ta asbe 11 theke....Kora ase";
            //TempData["result"] = result;
            return RedirectToAction("GeneratePdf", "Result");
            //return View();
        }
        //public ActionResult ShowPdfInfo(Student student)
        //{
        //    //ResultVM result = (ResultVM)TempData["result"];
        //    //ViewBag.Student = result;
        //    student.Id = (int)TempData["Id"];
        //    var students = aResultManager.GetAllStudents();
        //    ViewBag.Student = students.Find(a => a.Id == student.Id);
        //    List<CourseResultVM> courseResults = aResultManager.ShowStudentResult(student.Id);
        //    ViewBag.result = courseResults;
        //    return View();
        //}


        public JsonResult ShowResultById(int Id)
        {
            TempData["Id"] = Id;
            var result = aResultManager.ShowStudentResult(Id);
            return Json(result);
        }
        public ActionResult GeneratePdf(Student student)
        {
            student.Id = (int)TempData["Id"];
            var students = aResultManager.GetAllStudents();
            ViewBag.Student = students.Find(a => a.Id == student.Id);
            List<CourseResultVM> courseResults = aResultManager.ShowStudentResult(student.Id);
            ViewBag.result = courseResults;
            return new Rotativa.ViewAsPdf("GeneratePdf")
            {
                FileName = "Result sheet.pdf"
            };
        }
    }
}