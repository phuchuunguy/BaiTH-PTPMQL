using DemoMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Models
{
    public class AutoCode
    {
        public string GeneratePersonID(string id)
        {
             string prefix = "PS";
            int numberLength = 3;

            if (string.IsNullOrEmpty(id) || !id.StartsWith(prefix))
                return prefix + "001";

            string numberPart = id.Substring(prefix.Length);
            if (int.TryParse(numberPart, out int number))
            {
                number++;
                return prefix + number.ToString($"D{numberLength}");
            }

            return prefix + "001";
        }
    }
}
