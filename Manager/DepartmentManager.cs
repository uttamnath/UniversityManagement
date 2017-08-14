using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AW_UniversityManagementMvcApp.Gateway;
using AW_UniversityManagementMvcApp.Models.Entity_Models;

namespace AW_UniversityManagementMvcApp.Manager
{
    public class DepartmentManager
    {
        DepartmentGateway aDepartmentGateway=new DepartmentGateway();

        public string Save(Department aDepartment)
        {
            bool CodeExist = aDepartmentGateway.CheckCode(aDepartment);
            bool NameExist = aDepartmentGateway.CheckName(aDepartment);

           
            if (CodeExist && NameExist)
            {
                return "Department Name And Code already Exixt";
            }
            else if (CodeExist)
            {
               return "Department code already Exixt";
            }
            else if (NameExist)
            {
                return "Department Name already Exixt";
            }
            else
            {
                int rowAffected = aDepartmentGateway.Save(aDepartment);
                if (rowAffected > 0)
                {
                    return "Save Successfull";
                }
                return "Save Failed";
            }
       }

        public List<Department> GetAllDepartment()
        {

            return aDepartmentGateway.GetAllDepartment();
        }
        public List<Department> ViewAllDepartment()
        {

            return aDepartmentGateway.ViewAllDepartment();
        }

        public Department GetDepartmentById(int id)
        {
            return aDepartmentGateway.GetDepartmentById(id);
        }
    }
}