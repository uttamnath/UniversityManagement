using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AW_UniversityManagementMvcApp.Models.Entity_Models
{
    public class CourseEnroll
    {
        [DisplayName("Student Name")]
        public string StudentName { get; set; }
        public int StudentId { get; set; }

        public string Email { get; set; }
        [DisplayName("Department")]
        public string DepartmentName { get; set; }
        public string Date { get; set; }
        [DisplayName("Select Course")]
        public int CourseId { get; set; }
        public string EnrollmentStatus { get; set; }
       
    }
}