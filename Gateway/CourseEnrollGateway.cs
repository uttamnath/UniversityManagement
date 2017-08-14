using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AW_UniversityManagementMvcApp.Models;
using AW_UniversityManagementMvcApp.Models.Entity_Models;
using AW_UniversityManagementMvcApp.Models.View_Models;
using System.Data;

namespace AW_UniversityManagementMvcApp.Gateway
{
    public class CourseEnrollGateway : Gateway
    {
        public List<Student> ViewAllStudents()
        {
            Query = "SELECT * FROM Student order by RegNo";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Student> allStudents = new List<Student>();
            while (Reader.Read())
            {
                Student allStudent = new Student();
                allStudent.Id = (int)Reader["Id"];
                allStudent.RegNo = Reader["RegNo"].ToString();
                
                allStudents.Add(allStudent);
            }
            Reader.Close();
            Connection.Close();
            return allStudents;
        }
        public CourseEnroll GetStudentInfoByRegNo(string regNo)
        {// [Id,Name,Email,ContactNo,Date,Address,DepartmentID,DepartmentCode,Roll,RegNo,Student]
            Query = "SELECT s.Id, s.Name,Email,d.Name deptName FROM Student s join Department d on S.DepartmentID=D.Id " +
                    "WHERE RegNo='" + regNo + "'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            CourseEnroll aStudentDetails = new CourseEnroll();
            if (Reader.Read())
            {
                aStudentDetails.StudentId = (int) Reader["Id"];
                aStudentDetails.StudentName = Reader["Name"].ToString();
                aStudentDetails.Email = Reader["Email"].ToString();
                aStudentDetails.DepartmentName = Reader["deptName"].ToString();
            }

            Reader.Close();
            Connection.Close();
            return aStudentDetails;
        }
        public int EnrollSave(CourseEnroll aEnrollCourse)
        {//StudentId,CourseId,EnrollDate,EnrollmentStatus,CourseEnroll]
            Query = "INSERT INTO CourseEnroll Values(@StudentId, @CourseId, @EnrollDate, @EnrollmentStatus)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("StudentId", SqlDbType.Int);
            Command.Parameters["StudentId"].Value = aEnrollCourse.StudentId;
            Command.Parameters.Add("CourseId", SqlDbType.Int);
            Command.Parameters["CourseId"].Value = aEnrollCourse.CourseId;
            Command.Parameters.Add("EnrollDate", SqlDbType.Date);
            Command.Parameters["EnrollDate"].Value = aEnrollCourse.Date;
            Command.Parameters.Add("EnrollmentStatus", SqlDbType.VarChar);
            Command.Parameters["EnrollmentStatus"].Value = "Enrolled";
                      
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public bool DoesEnrollExist(CourseEnroll aCourseEnroll)
        {//StudentId,CourseId,EnrollDate,EnrollmentStatus,CourseEnroll]
            Query = "SELECT * FROM CourseEnroll WHERE StudentId= @StudentId and CourseId= @CourseId and EnrollmentStatus='Enrolled'";
            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.Clear();
            command.Parameters.Add("StudentId", SqlDbType.Int);
            command.Parameters["StudentId"].Value = aCourseEnroll.StudentId;
            command.Parameters.Add("CourseId", SqlDbType.Int);
            command.Parameters["CourseId"].Value = aCourseEnroll.CourseId;
            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                Connection.Close();
                return true;
            }
            reader.Close();
            Connection.Close();
            return false;
        }
    }
}