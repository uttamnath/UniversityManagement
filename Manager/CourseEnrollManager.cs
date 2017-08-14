using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AW_UniversityManagementMvcApp.Gateway;
using AW_UniversityManagementMvcApp.Models;
using AW_UniversityManagementMvcApp.Models.Entity_Models;
using AW_UniversityManagementMvcApp.Models.View_Models;
namespace AW_UniversityManagementMvcApp.Manager
{
    public class CourseEnrollManager
    {
        CourseEnrollGateway aCourseEnrollGateway = new CourseEnrollGateway();
        CourseGateway aCourseGateway=new CourseGateway();
        StudentGateway aStudentGateway=new StudentGateway();
        //private CourseEnroll student = null;
        public List<Student> GetAllStudents()
        {
            return aCourseEnrollGateway.ViewAllStudents();
        }
        public CourseEnroll GetStudentInfoByRegId(string regId)
        {

            return aCourseEnrollGateway.GetStudentInfoByRegNo(regId);
        }
        public string SaveEnroll(CourseEnroll enrollCourse)
        {

            int sId =aStudentGateway.GetIdForStudent(enrollCourse);
            enrollCourse.StudentId = sId;
            bool EnrollExist = aCourseEnrollGateway.DoesEnrollExist(enrollCourse);
            if (EnrollExist)
            {
                return "This student has been already Enrolled in this course";
            }
            else
            {
                int rowAffected = aCourseEnrollGateway.EnrollSave(enrollCourse);
                if (rowAffected > 0)
                {
                    return "Enrollment Successful";
                }
                return "Enrollment Failed";
            }
        }

        public List<Course> GetAllCourses(string studentId)
        {
            int deptId = aStudentGateway.GetDeptForStudent(studentId);
            return aCourseGateway.GetAllCourseByDeptID(deptId); 
        }
    }
}
