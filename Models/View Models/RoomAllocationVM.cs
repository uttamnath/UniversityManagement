using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AW_UniversityManagementMvcApp.Models.View_Models
{
    public class RoomAllocationVM
    {
        [Required(ErrorMessage = "Please select a department")]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please select a course")]
        [DisplayName("Course")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Please select a Classroom")]
        [DisplayName("Classroom")]
        public int RoomId { get; set; }
        [Required(ErrorMessage = "Please select a Day")]
        public string Day { get; set; }
        [Required(ErrorMessage = "Please select class starting time")]
        [DisplayName("From")]
        public DateTime FromTime { get; set; }
        [Required(ErrorMessage = "Please select class ending time")]
        [DisplayName("To")]
        public DateTime ToTime { get; set; }

        public string Schedule { get; set; }
    }
}