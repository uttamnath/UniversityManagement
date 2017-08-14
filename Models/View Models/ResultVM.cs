using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AW_UniversityManagementMvcApp.Models.View_Models
{
    public class ResultVM
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Student Reg. No.")]
        public string RegId { get; set; }

        [DisplayName("Name")]
        public string StudentName { get; set; }
        public string Email { get; set; }

        [Required]
        [DisplayName("Select Grade Letter")]
        public string Grade { get; set; }
        public string GradeId { get; set; }
        public string GradeResult { get; set; }

        [DisplayName("Department")]
        public string DepartmentName { get; set; }

        [Required]
        [DisplayName("Select Course")]
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int CourseId { get; set; }
        

    }
}