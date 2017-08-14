using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AW_UniversityManagementMvcApp.Gateway;
using AW_UniversityManagementMvcApp.Models.Entity_Models;
using AW_UniversityManagementMvcApp.Models.View_Models;

namespace AW_UniversityManagementMvcApp.Manager
{
    public class CourseManager
    {
        CourseGateway aCourseGateway=new CourseGateway();
        public string Save(Course aCourse)
        {

            bool CodeExist = aCourseGateway.CheckCode(aCourse);
            bool NameExist = aCourseGateway.CheckName(aCourse);
           
            if (CodeExist && NameExist)
            {
                return "Name And Code already Exixt";
            }
              else if (CodeExist)
            {
                return "Code already Exixt";
            }
            else if (NameExist)
            {
                return "Name already Exixt";
            }
            else 
            {
                int rowAffected = aCourseGateway.Save(aCourse);
                if (rowAffected > 0)
                {
                    return "Save Successfull";
                }
                return "Save Failed";

            }



        }
        public List<Course> GetAllCourseByDeptID(int DeptId)
        {

            return aCourseGateway.GetAllCourseByDeptID(DeptId);
        }
        public List<Semester> GetAllSemester()
        {
            return aCourseGateway.GetAllSemester();
        }
        public Course GetaCourseByCourseId(int courseId)
        {
            return aCourseGateway.GetaCourseByCourseId(courseId);
            
        }

        public List<CourseStatVM> ViewCourseStat(int departmentId)
        {
            return aCourseGateway.ViewCourseStat(departmentId);
        }
    }
}