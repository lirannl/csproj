using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1
{
    public static class Extensions
    {
        // String methods for this program
        public static string Pluralise(this string input, int number)
        {
            if (Math.Abs(number) == 1) return input; // Add an s given any number other than 1 and -1.
            return input + "s";
        }
        // Convert numbers like "1" to position indications such as "1st"
        public static string Ordinal(this int input)
        {
            switch (input % 10) // Check the last digit and add the appropriate suffix.
            {
                case 1:
                    return input.ToString() + "st";
                case 2:
                    return input.ToString() + "nd";
                case 3:
                    return input.ToString() + "rd";
                default:
                    return input.ToString() + "th";
            }
        }
    }
}
