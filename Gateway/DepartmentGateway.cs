using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AW_UniversityManagementMvcApp.Models.Entity_Models;

namespace AW_UniversityManagementMvcApp.Gateway
{
    public class DepartmentGateway : Gateway
    {

        public int Save(Department aDepartment)
        {
            //[Id,Code,Name,Department
            Query = "INSERT INTO Department Values(@Code, @Name)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("Code", SqlDbType.VarChar);
            Command.Parameters["Code"].Value = aDepartment.Code;
            Command.Parameters.Add("Name", SqlDbType.VarChar);
            Command.Parameters["Name"].Value = aDepartment.Name;
            
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public bool CheckCode(Department aDepartment)
        {
            //[Id,Code,Name,Department
            Query = "SELECT *FROM Department WHERE Code='" + aDepartment.Code + "'";
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
        public bool CheckName(Department aDepartment)
        {

            Query = "SELECT *FROM Department WHERE Name='" + aDepartment.Name + "'";
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
        public List<Department> ViewAllDepartment()
        {
            Query = "SELECT * FROM Department";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Department> allDepartments = new List<Department>();
            
           
            while (Reader.Read())
            {
                Department aDepartment = new Department();
                aDepartment.Code = Reader["Code"].ToString();
                aDepartment.Name = Reader["Name"].ToString();
                allDepartments.Add(aDepartment);
            }

            Reader.Close();
            Connection.Close();
            return allDepartments;
        }

        public List<Department> GetAllDepartment()
        {
            Query = "SELECT * FROM Department";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<Department> allDepartments = new List<Department>();
            //Department aDepartments = new Department()
            //{           
            //    Name = "--Select--"
            //};
            //allDepartments.Add(aDepartments);
            while (Reader.Read())
            {
                Department aDepartment = new Department();
                aDepartment.Id = (int)Reader["Id"];
                aDepartment.Code = Reader["Code"].ToString();
                aDepartment.Name = Reader["Name"].ToString();
                allDepartments.Add(aDepartment);
            }

            Reader.Close();
            Connection.Close();
            return allDepartments;
        }

        public Department GetDepartmentById(int id)
        {
            Query = "SELECT * FROM Department Where Id='"+id+"'";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Department department = null;
            while (Reader.Read())
            {
                department=new Department();
                department.Name = Reader["Name"].ToString();
                department.Code = Reader["Code"].ToString();
                
            }

            Reader.Close();
            Connection.Close();
            return department;
        }
    }
}