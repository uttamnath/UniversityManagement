using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AW_UniversityManagementMvcApp.Models.Entity_Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Course Code")]
        [StringLength(50,MinimumLength = 5, ErrorMessage = "Code must be at least five (5) characters long")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Please enter Course Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Course Credit")]
        [Range(0.5, 5, ErrorMessage = "Credit must be a Number ,Range is from 0.5 to 5.0")]
        public decimal Credit { get; set; }
        [Required(ErrorMessage = "Please Enter Course Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please Select a Department")]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please Select a Semister")]
        [DisplayName("Semester")]
        public int SemesterId { get; set; }

    }
}