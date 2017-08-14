using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AW_UniversityManagementMvcApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Student Name")]
        [DisplayName("Student Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter email address")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }
        [DisplayName("Contact No.")]
        [Required(ErrorMessage = "Please enter a Contact No.")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Please enter date")]
        public string Date { get; set; }
        [Required(ErrorMessage = "Please enter address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please select a Department")]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public int Roll { get; set; }
        public string RegNo { get; set; }


    }
}