using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entity
{
    public class Student
    {
        [Key]
        public string StudentID { get; set; } = default!;
        [Required(ErrorMessage = "FullName is requied.")]
        public string FullName { get; set; } = default!;
        public string? Address { get; set; }
    }
}