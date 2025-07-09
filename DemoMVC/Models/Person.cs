using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Person
    {
        [Key]
        public required string PersonID { get; set; }
        public required string FullName { get; set; }
        public string? Address { get; set; }
    }
}
