using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AW_UniversityManagementMvcApp.Gateway;
using AW_UniversityManagementMvcApp.Models.Entity_Models;
using AW_UniversityManagementMvcApp.Models.View_Models;

namespace AW_UniversityManagementMvcApp.Manager
{
    public class TeacherManager
    {
        TeacherGateway aTeacherGateway=new TeacherGateway();

       
        public string Save(Teacher aTeacher)
        {
            bool EmailExist = aTeacherGateway.CheckEmail(aTeacher);

            if (EmailExist)
            {
                return "Email already Exist";
            }

            else
            {
                int rowAffected = aTeacherGateway.Save(aTeacher);
                if (rowAffected > 0)
                {
                    return "Save Successfull";
                }
                return "Save Failed";

            }

        }




        public List<Designation> GetAllDesignation()
        {
            return aTeacherGateway.GetAllDesignation();
        }


        public List<TeacherVM> GetAllTeacherByDepartment(int deptId)
        {

            return aTeacherGateway.GetAllTeacherByDepartment(deptId);
        }
        public string AssignTeacher(CourseAssignVM aCourseAssignVm)
        {
            bool CourseAssigned = aTeacherGateway.CheckCourse(aCourseAssignVm);

            if (CourseAssigned)
            {
                return "This Course already Assigned to a Teacher";
            }
            else
            {
                int rowAffected = aTeacherGateway.AssignTeacher(aCourseAssignVm);
                if (rowAffected > 0)
                {
                    return "Course Assign Successfull";
                }
                return "Course Assign  Failed";
            }

        }

        public TeacherVM GetTeacherInfoByTeacherId(int teacherId)
        {
            return aTeacherGateway.GetTeacherInfoByTeacherId(teacherId);
        }
        public string UnassignAllCourses()
        {
            int rowAffected = aTeacherGateway.UnassignAllCourses();
            if (rowAffected > 0)
            {
                return "All Courses Unassigned";
            }
            return "Course Already Unassigned";
        }
    }
}