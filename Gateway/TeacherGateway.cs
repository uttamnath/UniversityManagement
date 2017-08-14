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
    public class TeacherGateway : Gateway
    {
        public int Save(Teacher aTeacher)
        {

            Query = "INSERT INTO Teacher Values(@Name, @Address, @Email,@ContactNo,@DesignationID,@DepartmentID,@Credit_To_Be_Taken,@Remaining_credit)";
         Command = new SqlCommand(Query, Connection);
         Command.Parameters.Clear();
         Command.Parameters.Add("Name", SqlDbType.VarChar);
         Command.Parameters["Name"].Value = aTeacher.Name;
         Command.Parameters.Add("Address", SqlDbType.VarChar);
         Command.Parameters["Address"].Value = aTeacher.Address;
         Command.Parameters.Add("Email", SqlDbType.VarChar);
         Command.Parameters["Email"].Value = aTeacher.Email;
         Command.Parameters.Add("ContactNo", SqlDbType.VarChar);
         Command.Parameters["ContactNo"].Value = aTeacher.ContactNo;
         Command.Parameters.Add("DesignationID", SqlDbType.Int);
         Command.Parameters["DesignationID"].Value = aTeacher.DesignationID;
         Command.Parameters.Add("DepartmentID", SqlDbType.Int);
         Command.Parameters["DepartmentID"].Value = aTeacher.DepartmentID;
         Command.Parameters.Add("Credit_To_Be_Taken", SqlDbType.Decimal);
         Command.Parameters["Credit_To_Be_Taken"].Value = aTeacher.CreditToBeTaken;
         Command.Parameters.Add("Remaining_credit", SqlDbType.Decimal);
         Command.Parameters["Remaining_credit"].Value = aTeacher.CreditToBeTaken;

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public bool CheckEmail(Teacher aTeacher)
        {

           
            Query = "SELECT *FROM Teacher WHERE Email='" + aTeacher.Email + "'";
          Command = new SqlCommand(Query, Connection);
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();



            bool hasRow = false;
            if (reader.HasRows)
            {
                hasRow = true;
            }

            reader.Close();
            Connection.Close();

            return hasRow;

        }
        public List<Designation> GetAllDesignation()
        {
    
          Query = "SELECT * FROM Designation";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
           Reader = Command.ExecuteReader();
            List<Designation> allDesignations = new List<Designation>();
            while (Reader.Read())
            {
                Designation aDesignation = new Designation();
                aDesignation.Id = (int)Reader["Id"];
                aDesignation.Name = Reader["Name"].ToString();
                allDesignations.Add(aDesignation);
            }

            Reader.Close();
            Connection.Close();
            return allDesignations;
        }

        public List<TeacherVM> GetAllTeacherByDepartment(int deptId)
        {
            Query = "SELECT * FROM Teacher WHERE DepartmentID='" + deptId + "'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<TeacherVM> allTeacherOfDept = new List<TeacherVM>();
            while (Reader.Read())
            {
                TeacherVM aTeacherVm = new TeacherVM();
                aTeacherVm.Id = (int)Reader["Id"];
                aTeacherVm.Name = Reader["Name"].ToString();
                aTeacherVm.CreditToBeTaken =(decimal) Reader["Credit_To_Be_Taken"];
                aTeacherVm.CreditToBeTaken = (decimal)Reader["Remaining_credit"];
                allTeacherOfDept.Add(aTeacherVm);
            }

            Reader.Close();
            Connection.Close();
            return allTeacherOfDept;
            
        }

        public int AssignTeacher(CourseAssignVM aCourseAssignVm)
        {
            Query = "UPDATE Teacher SET Remaining_credit= Remaining_credit-'" + aCourseAssignVm.Credit + "' WHERE Id='" + aCourseAssignVm.TeacherID + "'";

            Command = new SqlCommand(Query, Connection);
            //Command.Parameters.Clear();
            //Command.Parameters.Add("Credit", SqlDbType.Decimal);
            //Command.Parameters["Credit"].Value = aCourseAssignVm.Credit;

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            if (rowAffected>0)
            {
                Query = "UPDATE Course SET TeacherID='" + aCourseAssignVm.TeacherID + "' ,Assignment_Status='Assigned' where Id='" + aCourseAssignVm.CourseID + "'";

                Command = new SqlCommand(Query, Connection);
                rowAffected = Command.ExecuteNonQuery();
                

            }

            Connection.Close();
            return rowAffected;
        }

        public bool CheckCourse(CourseAssignVM aCourseAssignVm)
        {

            Query = "SELECT *FROM Course WHERE Id='" + aCourseAssignVm.CourseID + "' AND Assignment_Status='Assigned' ";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();



            bool hasRow = false;
            if (reader.HasRows)
            {
                hasRow = true;
            }

            reader.Close();
            Connection.Close();

            return hasRow;
        }

        public TeacherVM GetTeacherInfoByTeacherId(int teacherId)
        {
            Query = "SELECT * FROM Teacher WHERE Id='" + teacherId + "'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            TeacherVM aTeacherVm = new TeacherVM();
            if (Reader.Read())
            {

                aTeacherVm.Id = (int)Reader["Id"];
                aTeacherVm.Name = Reader["Name"].ToString();
                aTeacherVm.CreditToBeTaken = (decimal)Reader["Credit_To_Be_Taken"];
                aTeacherVm.RemainingCredit = (decimal)Reader["Remaining_credit"];

            }

            Reader.Close();
            Connection.Close();
            return aTeacherVm;
        }
        public int UnassignAllCourses()
        {
            Query = "UPDATE Course SET Assignment_Status= 'Not Assigned Yet'  WHERE Assignment_Status ='Assigned'";

            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            if (rowAffected > 0)
            {
                Query = "UPDATE CourseEnroll SET EnrollmentStatus='Not Enrolled'  WHERE EnrollmentStatus ='Enrolled'";

                Command = new SqlCommand(Query, Connection);
                rowAffected = Command.ExecuteNonQuery();
                
            }
            Connection.Close();
            return rowAffected;
           
        }
    }
}