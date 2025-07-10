using DemoMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Models.Process
{
    public class AutoGenerateId
    {
        public string GenerateID(string inputID)
        {
            var match = System.Text.RegularExpressions.Regex.Match(inputID, @"^(?<prefix>[A-Za-z]+)(?<number>\d+)$");
            if (!match.Success)
            {
                throw new ArgumentException("Invalid id format");
            }
            string prefix = match.Groups["prefix"].Value;
            string numberPart = match.Groups["number"].Value;
            int number = int.Parse(numberPart) + 1;
            string newNumberPart = number.ToString().PadLeft(numberPart.Length, '0');

            return prefix + newNumberPart;
        }
    }
}
