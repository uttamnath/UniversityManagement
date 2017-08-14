using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Serialization;

namespace AW_UniversityManagementMvcApp.Models.Entity_Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Department Code")]
        [StringLength(7, MinimumLength = 2 , ErrorMessage = "Department Code length must be between 2 to 7")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Please Enter Department Name")]
        public string Name { get; set; }

    }
}