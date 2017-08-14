using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AW_UniversityManagementMvcApp.Manager;
using AW_UniversityManagementMvcApp.Models.View_Models;

namespace AW_UniversityManagementMvcApp.Controllers
{
    public class ClassroomController : Controller
    {
        ClassroomManager aClassroomManager = new ClassroomManager();
        DepartmentManager aDepartmentManager = new DepartmentManager();
        CourseManager aCourseManager=new CourseManager();
        //
        // GET: /Classroom/
        public ActionResult Index()
        {
            return View();
        }
        //To Be Done: Room Allocate(story 8), View Schedule (story 9) and Room Unallocate(story 14)
        public ActionResult AllocateRoom()
        {
            ViewBag.department = aDepartmentManager.GetAllDepartment();
            ViewBag.days = GetDays();
            ViewBag.rooms = aClassroomManager.GetAllRooms();
            return View();
        }



        [HttpPost]
        public ActionResult AllocateRoom(RoomAllocationVM roomAllocation)
        {
            ViewBag.department = aDepartmentManager.GetAllDepartment();
            ViewBag.days = GetDays();
            ViewBag.rooms = aClassroomManager.GetAllRooms();
            if (ModelState.IsValid)
            {
                ViewBag.Message = aClassroomManager.Allocate(roomAllocation);
            }
            
            return View();
        }

        public JsonResult GetCoursesByDepartment(int departmentId)
        {
            var courses = aCourseManager.GetAllCourseByDeptID(departmentId);
            return Json(courses);
        }

        private List<SelectListItem> GetDays()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem(){Value = "", Text = "--Select--"},
                new SelectListItem() { Value = "Sun", Text = "Sunday" },
                new SelectListItem() { Value = "Mon", Text = "Monday" },
                new SelectListItem() { Value = "Tue", Text = "Tuesday" },
                new SelectListItem() { Value = "Wed", Text = "Wednesday" },
                new SelectListItem() { Value = "Thu", Text = "Thursday" },
                new SelectListItem() { Value = "Fri", Text = "Friday" },
                new SelectListItem() { Value = "Sat", Text = "Saturday" }            
            };
            return items;
        }

        public ActionResult ViewClassSchedule()
        {
            ViewBag.department = aDepartmentManager.GetAllDepartment();
            return View();
        }

        public JsonResult GetClassScheduleByDepartment(int departmentId)
        {
            List<ClassSchedule> classSchedules = aClassroomManager.GetGetClassScheduleByDepartment(departmentId);
            return Json(classSchedules);
        }
        public ActionResult UnAllocateClassroom()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnAllocateClassroom(string status)
        {
            ViewBag.Unallocate = aClassroomManager.UnAllocateClassroom();
            return View();
        }
    }
}