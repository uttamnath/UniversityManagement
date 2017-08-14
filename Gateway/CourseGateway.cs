using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AW_UniversityManagementMvcApp.Models.Entity_Models;
using AW_UniversityManagementMvcApp.Models.View_Models;

namespace AW_UniversityManagementMvcApp.Gateway
{
    public class CourseGateway : Gateway
    {
        public int Save(Course aCourse)
        {

           
           Query = "INSERT INTO Course(Code,Name,Credit,Description,DepartmentID,SemesterID,Assignment_Status) Values(@Code, @Name, @Credit, @Description,@DepartmentID,@SemesterID,@Assignment_Status)";
           Command = new SqlCommand(Query, Connection);
           Command.Parameters.Clear();
           Command.Parameters.Add("Code", SqlDbType.VarChar);
           Command.Parameters["Code"].Value = aCourse.Code;
           Command.Parameters.Add("Name", SqlDbType.VarChar);
           Command.Parameters["Name"].Value = aCourse.Name;
           Command.Parameters.Add("Credit", SqlDbType.Decimal);
           Command.Parameters["Credit"].Value = aCourse.Credit;
           Command.Parameters.Add("Description", SqlDbType.VarChar);
           Command.Parameters["Description"].Value = aCourse.Description;
           Command.Parameters.Add("DepartmentID", SqlDbType.Int);
           Command.Parameters["DepartmentID"].Value = aCourse.DepartmentId;
           Command.Parameters.Add("SemesterID", SqlDbType.Int);
           Command.Parameters["SemesterID"].Value = aCourse.SemesterId;
            //command.Parameters.Add("TeacherID", SqlDbType.Int);
            //command.Parameters["TeacherID"].Value = 0;           
           Command.Parameters.Add("Assignment_Status", SqlDbType.VarChar);
            Command.Parameters["Assignment_Status"].Value = "Not Assigned Yet";
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
        public bool CheckCode(Course aCourse)
        {
           
            Query = "SELECT *FROM Course WHERE Code='" + aCourse.Code + "'";
           Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();



            bool hasRow = false;
            if (Reader.HasRows)
            {
                hasRow = true;
            }

            Reader.Close();
            Connection.Close();

            return hasRow;


        }

        public bool CheckName(Course aCourse)
        {
           
           Query = "SELECT *FROM Course WHERE Name='" + aCourse.Name + "'";
           Command = new SqlCommand(Query, Connection);
            Connection.Open();
           Reader = Command.ExecuteReader();

            bool hasRow = false;
            if (Reader.HasRows)
            {
                hasRow = true;
            }

            Reader.Close();
            Connection.Close();

            return hasRow;
        }

        public List<Course> GetAllCourseByDeptID(int DeptId)
        {
            Query = "SELECT * FROM Course WHERE DepartmentID='" + DeptId + "'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Course> allCourseByCodeList = new List<Course>();
            while (Reader.Read())
            {
                Course course = new Course();
                course.Id = (int) Reader["Id"];
                course.Code = Reader["Code"].ToString();
                course.Name =Reader["Name"].ToString();
                course.Credit = (decimal)Reader["Credit"];

                allCourseByCodeList.Add(course);
            }

            Reader.Close();
            Connection.Close();
            return allCourseByCodeList;
        }
        public List<Semester> GetAllSemester()
        {

            Query = "SELECT * FROM Semester";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Semester> allSemester = new List<Semester>();
            while (Reader.Read())
            {
                Semester aSemester = new Semester();
                aSemester.Id = (int)Reader["Id"];
                aSemester.Code = Reader["Code"].ToString();

                allSemester.Add(aSemester);
            }

            Reader.Close();
            Connection.Close();
            return allSemester;
        }

        public Course GetaCourseByCourseId(int courseId)
        {
            Query = "SELECT * FROM Course WHERE Id='" + courseId + "'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Course aCourse = new Course();
            if (Reader.Read())
            {

                aCourse.Id = (int)Reader["Id"];
                aCourse.Code = Reader["Code"].ToString();
                aCourse.Name = Reader["Name"].ToString();
                aCourse.Credit = (decimal)Reader["Credit"];


            }

            Reader.Close();
            Connection.Close();
            return aCourse;
        }

        public List<CourseStatVM> ViewCourseStat(int departmentId)
        {
            Query = "SELECT * FROM ViewCourseStat WHERE DepartmentID='" + departmentId + "'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<CourseStatVM> allCourseByCodeList = new List<CourseStatVM>();
            while (Reader.Read())
            {
                CourseStatVM aCourse = new CourseStatVM();
                aCourse.Code = Reader["Code"].ToString();
                aCourse.Name = Reader["Course_Name"].ToString();
                aCourse.Semester = Reader["Semester"].ToString();
                string s = Reader["Teacher_Name"].ToString();
                string a = Reader["Assignment_Status"].ToString();
                if ((s != "" && a != "Not Assigned Yet"))
                {
                    aCourse.AssignTo = Reader["Teacher_Name"].ToString();
                }
                else
                {
                    aCourse.AssignTo = "Not Assigned Yet";
                }





                allCourseByCodeList.Add(aCourse);
            }

            Reader.Close();
            Connection.Close();
            return allCourseByCodeList;
        }
    }
}