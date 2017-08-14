using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AW_UniversityManagementMvcApp.Models.Entity_Models
{
    public class Teacher
    {
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
          [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
           [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",ErrorMessage = "Enter a Valid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Contact number")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = " Please select a Designation")]
        [DisplayName("Designation")]
        public int DesignationID { get; set; }
        [Required(ErrorMessage = "Please select a Department")]
        [DisplayName("Department")]
        public int DepartmentID { get; set; }
        [Required(ErrorMessage = "Please taken Credit")]
        [Range(0d, (double)decimal.MaxValue, ErrorMessage = "Credit must Non-Negative")]
        [DisplayName("Credit To Be Taken")]
        public decimal CreditToBeTaken { get; set; }
    }
}