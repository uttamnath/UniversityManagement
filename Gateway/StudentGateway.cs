using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AW_UniversityManagementMvcApp.Models;
using AW_UniversityManagementMvcApp.Models.Entity_Models;

namespace AW_UniversityManagementMvcApp.Gateway
{
    public class StudentGateway : Gateway
    {
        public bool DoesStudentExist(string email)
        {
            Query = "SELECT * FROM Student WHERE Email= @email";
            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.Clear();
            command.Parameters.Add("email", SqlDbType.VarChar);
            command.Parameters["email"].Value = email;
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

        public int Save(Student student)
        {
            Query = "INSERT INTO Student Values(@name, @email, @ContactNo, @Date, @Address, @DepartmentID, @DepartmentCode, @Roll)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("name", SqlDbType.VarChar);
            Command.Parameters["name"].Value = student.Name;
            Command.Parameters.Add("email", SqlDbType.VarChar);
            Command.Parameters["email"].Value = student.Email;
            Command.Parameters.Add("ContactNo", SqlDbType.VarChar);
            Command.Parameters["ContactNo"].Value = student.ContactNo;
            Command.Parameters.Add("Date", SqlDbType.Date);
            Command.Parameters["Date"].Value = student.Date;
            Command.Parameters.Add("Address", SqlDbType.VarChar);
            Command.Parameters["Address"].Value = student.Address;
            Command.Parameters.Add("DepartmentID", SqlDbType.Int);
            Command.Parameters["DepartmentID"].Value = student.DepartmentId;
            Command.Parameters.Add("DepartmentCode", SqlDbType.VarChar);
            Command.Parameters["DepartmentCode"].Value = student.DepartmentCode;
            Command.Parameters.Add("Roll", SqlDbType.Int);
            Command.Parameters["Roll"].Value = student.Roll;
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public bool DoesBatchExist(string departmentCode, string date)
        {
            string year = date.Substring(0, 4);
            Query = "SELECT * FROM Student WHERE DepartmentCode=@code and YEAR(Date)=@year";
            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.Clear();
            command.Parameters.Add("code", SqlDbType.VarChar);
            command.Parameters["code"].Value = departmentCode;
            command.Parameters.Add("year", SqlDbType.VarChar);
            command.Parameters["year"].Value = year;
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

        public int GetLastRoll(string departmentCode, string date)
        {
            int roll = 0;
            string year = date.Substring(0, 4);
            Query = "select top 1 Roll from Student WHERE DepartmentCode=@code and YEAR(Date)=@year order by Roll desc";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("code", SqlDbType.VarChar);
            Command.Parameters["code"].Value = departmentCode;
            Command.Parameters.Add("year", SqlDbType.VarChar);
            Command.Parameters["year"].Value = year;
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
                roll = (int)Reader["Roll"];

            }
            Reader.Close();
            Connection.Close();
            return roll;
        }

        public Student GetLastSavedStudent()
        {
            Query = "WITH x AS (SELECT *, r = RANK() OVER (ORDER BY Id DESC) from Student) "+
                    "SELECT * FROM x WHERE r = 1";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Student student = null;
            if (Reader.Read())
            {
                student=new Student();
                student.Name = Reader["Name"].ToString();
                student.Email = Reader["Email"].ToString();
                student.ContactNo = Reader["ContactNo"].ToString();
                string date = Reader["Date"].ToString();
                DateTime dt = Convert.ToDateTime(date);
                student.Date = dt.ToString("yyyy-MM-dd");
                student.DepartmentId = (int)Reader["DepartmentId"];
                student.RegNo = Reader["RegNo"].ToString();

            }
            Reader.Close();
            Connection.Close();
            return student;
        }

        public int GetDeptForStudent(string regId)
        {
            int deptId = 0;
            Query = "SELECT DepartmentID FROM Student WHERE RegNo='"+regId+"'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
                deptId = (int)Reader["DepartmentID"];
            }
            Reader.Close();
            Connection.Close();
            return deptId;
        }

        public int GetIdForStudent(CourseEnroll enrollCourse)
        {
            int sId = 0;
            Query = "SELECT Id FROM Student WHERE Name='" + enrollCourse.StudentName + "'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
                sId = (int)Reader["Id"];
            }
            Reader.Close();
            Connection.Close();
            return sId;
        }
    }
}