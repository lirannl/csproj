using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1
{
    class CustomParser
    {
        // Safe parsing of numbers.
        public static long[] SafeParse(string input, bool allowNegatives = true, bool silent = false, params long[] blacklist)
        {
            long[] output = new long[2];
            output[1] = 0; // Mark output as invalid
            try
            {
                output[0] = long.Parse(input);
                output[1] = 0; // Declare the output as invalid
                string blacklist_nums = "";
                for (int i = 0; i < blacklist.Length; i++) // Create a string containing all of the values in blacklist, separated by ", "
                {
                    if (i > 0) blacklist_nums += ", "; // Add the separator from the second element onwards
                    blacklist_nums += blacklist[i].ToString(); // Add the number into the string
                }
                if (!allowNegatives && output[0] < 0) { if (!silent) Console.Write("Error: Invalid number. Number must be above 0"); }
                else if (blacklist.Contains(output[0])) { if (!silent) Console.Write("Error: Cannot have the {0} {1}", "number".Pluralise(blacklist.Length), blacklist_nums); }
                else output[1] = 1; // If no failures are triggered, 
            }
            catch (System.FormatException)
            {
                if (!silent) Console.Write("Error: You must enter a number");
            }
            catch (System.OverflowException)
            {
                if (!silent) Console.Write("Error: You must enter a valid number between 2^64 and -2^64");
            }
            return output;
        }
    }
}
