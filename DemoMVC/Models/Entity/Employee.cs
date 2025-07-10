namespace DemoMVC.Models.Entity
{
    public class Employee : Person
    {
        public string EmployeeID { get; set; } = default!;
        public int Age { get; set; } = default!;
    }
}