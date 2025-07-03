using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Student
    {
        [Key]
        public required string StudentID { get; set; }
        public required string FullName { get; set; }
        public string? Address { get; set; }
    }
}