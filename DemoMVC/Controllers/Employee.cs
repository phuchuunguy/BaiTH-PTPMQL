using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    public class Employee : Controller
    {
        public IActionResult ID()
        {
            return View();
        }
        public IActionResult ChucVu()
        {
            return View();
        }
    }
}