using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AW_UniversityManagementMvcApp.Models.View_Models
{
    public class CourseStatVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Semester { get; set; }
        public string AssignTo { get; set; }
    }
}