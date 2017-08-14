using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AW_UniversityManagementMvcApp.Models;
using AW_UniversityManagementMvcApp.Models.Entity_Models;
using AW_UniversityManagementMvcApp.Models.View_Models;

namespace AW_UniversityManagementMvcApp.Gateway
{
    public class ResultGateway : Gateway
    {

        public int SaveResult(ResultVM student)
        {
            Query = "INSERT INTO Result VALUES(@studentID, @courseID, @grade)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("studentID", SqlDbType.Int);
            Command.Parameters["studentID"].Value = student.Id;
            Command.Parameters.Add("courseID", SqlDbType.Int);
            Command.Parameters["courseID"].Value = student.CourseId;
            Command.Parameters.Add("grade", SqlDbType.VarChar);
            Command.Parameters["grade"].Value = student.Grade;
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }



        public List<Course> GetEnrollCourses(int id)
        {
            Query = "SELECT Id, Code, EnrollmentStatus FROM  Course Join CourseEnroll ON Course.Id = CourseEnroll.CourseId WHERE EnrollmentStatus ='Enrolled' AND StudentId = " + id;
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Course> courses = new List<Course>();
            while (Reader.Read())
            {
                Course aCourse = new Course();
                aCourse.Id = (int)Reader["Id"];
                aCourse.Code = Reader["Code"].ToString();
                courses.Add(aCourse);
            }
            Reader.Close();
            Connection.Close();
            return courses;
        }

        public List<Student> GetAllStudents()
        {
            Query = "SELECT * FROM Student";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Student> students = new List<Student>();
            while (Reader.Read())
            {
                Student aStudent = new Student();
                aStudent.Id = (int)Reader["Id"];
                aStudent.Name = Reader["Name"].ToString();
                aStudent.Email = Reader["Email"].ToString();
                aStudent.DepartmentCode = Reader["DepartmentCode"].ToString();
                aStudent.RegNo = Reader["RegNo"].ToString();
                students.Add(aStudent);
            }
            Reader.Close();
            Connection.Close();
            return students;
        }



        public ResultVM GetStudentByRegNo(int id)
        {
            Query = "SELECT * FROM Student WHERE Id = @id";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("id", SqlDbType.Int);
            Command.Parameters["id"].Value = id;
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            ResultVM student = null;
            while (reader.Read())
            {
                student = new ResultVM();
                student.Id = (int)reader["Id"];
                student.StudentName = reader["Name"].ToString();
                student.Email = reader["Email"].ToString();
                student.DepartmentName = reader["Name"].ToString();
            }
            reader.Close();
            Connection.Close();
            return student;
        }

        public List<CourseResultVM> ShowStudentResult(int id)
        {
            Query = "SELECT * FROM ViewStudentResult WHERE Id ='" + id + "'";//Where reg. no lagate hbe
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<CourseResultVM> courseResulList = new List<CourseResultVM>();
            while (Reader.Read())
            {
                CourseResultVM aCourseResultVm = new CourseResultVM();

                aCourseResultVm.CourseCode = Reader["Code"].ToString();
                aCourseResultVm.CourseName = Reader["CourseName"].ToString();
                string grade = Reader["Grade"].ToString();
                if (grade == "")
                {
                    aCourseResultVm.Result = "Not Graded Yet";
                }
                else
                {
                    aCourseResultVm.Result = grade;

                }
                var update = courseResulList.Find(c => c.CourseCode == aCourseResultVm.CourseCode);
                int i = courseResulList.FindIndex(c => c.CourseCode == aCourseResultVm.CourseCode);
                if (update != null)
                {
                    courseResulList.RemoveAt(i);
                    
                }
                courseResulList.Add(aCourseResultVm);
            }
            Reader.Close();
            Connection.Close();
            return courseResulList;
        }
    }
}