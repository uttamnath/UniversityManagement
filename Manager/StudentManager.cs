using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AW_UniversityManagementMvcApp.Gateway;
using AW_UniversityManagementMvcApp.Models;
using AW_UniversityManagementMvcApp.Models.Entity_Models;

namespace AW_UniversityManagementMvcApp.Manager
{
    public class StudentManager
    {
        StudentGateway aStudentGateway=new StudentGateway();
        DepartmentGateway aDepartmentGateway=new DepartmentGateway();
        public string Save(Student student)
        {
            if (aStudentGateway.DoesStudentExist(student.Email))
            {
                return "This email already exists";
            }
            Department aDepartment=aDepartmentGateway.GetDepartmentById(student.DepartmentId);
            student.DepartmentCode = aDepartment.Code;
            int roll = 0;
            if (aStudentGateway.DoesBatchExist(student.DepartmentCode,student.Date))
            {
                roll = aStudentGateway.GetLastRoll(student.DepartmentCode, student.Date);
                roll++;
            }
            else
            {
                roll = 1;
            }
            student.Roll = roll;
            int rowAffected = aStudentGateway.Save(student);
            if (rowAffected > 0)
            {
                return "Registration Successful";
            }
            return "Registration Failed";
        }

        public Student GetLastSaveStudent()
        {
            return aStudentGateway.GetLastSavedStudent();
        }
    }
}