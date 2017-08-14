using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AW_UniversityManagementMvcApp.Models.View_Models
{
    public class ClassSchedule
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Schedule { get; set; }
        public int DeptId { get; set; }
    }
}