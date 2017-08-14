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
    public class ResultManager
    {
        ResultGateway aResultGateway = new ResultGateway();

        public string SaveResult(ResultVM student)
        {
            int rowAffected = aResultGateway.SaveResult(student);
            if (rowAffected > 0)
            {
                return "Save successful";
            }
            else
            {
                return "Save failed";
            }
        }
        public List<Course> GetEnrollCourses(int id)
        {
            return aResultGateway.GetEnrollCourses(id);
        }

        public List<Student> GetAllStudents()
        {
            return aResultGateway.GetAllStudents();
        }


        public ResultVM GetStudentByRegNo(int id)
        {
            return aResultGateway.GetStudentByRegNo(id);
        }

        public List<CourseResultVM> ShowStudentResult(int id)
        {
            return aResultGateway.ShowStudentResult(id);
        }

        public List<ResultVM> GetAllGrades()
        {
            List<ResultVM> grades = new List<ResultVM>
            {
                new ResultVM() {GradeId = "A+", GradeResult = "A+"},
                new ResultVM() {GradeId = "A", GradeResult = "A"},
                new ResultVM() {GradeId = "A-", GradeResult = "A-"},
                new ResultVM() {GradeId = "B+", GradeResult = "B+"},
                new ResultVM() {GradeId = "B", GradeResult = "B"},
                new ResultVM() {GradeId = "B-", GradeResult = "B-"},
                new ResultVM() {GradeId = "C+", GradeResult = "C+"},
                new ResultVM() {GradeId = "C", GradeResult = "C"},
                new ResultVM() {GradeId = "C-", GradeResult = "C-"},
                new ResultVM() {GradeId = "D+", GradeResult = "D+"},
                new ResultVM() {GradeId = "D", GradeResult = "D"},
                new ResultVM() {GradeId = "D-", GradeResult = "D-"},
                new ResultVM() {GradeId = "F", GradeResult = "F"}         
            };
            return grades;
        }
    }
}