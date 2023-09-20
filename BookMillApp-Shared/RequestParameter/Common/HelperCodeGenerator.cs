using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Shared.RequestParameter.Common
{
    public static class HelperCodeGenerator
    {
        public static readonly Random CodeGenerator = new();
        public static string GenerateSixDigit()
        {
            string sicdigit = CodeGenerator.Next(0, 1000000).ToString("D6");
            return sicdigit;
        }
        public static string GenerateOrderReference(string prefix)
        {
            string generatedNumbers = GenerateSixDigit();
            string bookingReference = $"{prefix}_{DateTime.UtcNow.TimeOfDay.Ticks}{generatedNumbers}";
            return bookingReference;
        }
    }
}
