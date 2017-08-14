using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AW_UniversityManagementMvcApp.Models.View_Models
{
    public class CourseAssignVM
    {
        //public int Id { get; set; }
        [DisplayName("Department")]
        public int DepartmentID { get; set; }
        [DisplayName("Teacher")]
        public int TeacherID { get; set; }
        [DisplayName("Credit To Be Taken")]
        public decimal CreditToBeTaken { get; set; }
        [DisplayName("Remaining Credit")]
        public decimal RemainingCredit { get; set; }
        [DisplayName("Course Code")]
        public string CourseID { get; set; }
        public string Name { get; set; }
        public decimal Credit { get; set; }

    }
}