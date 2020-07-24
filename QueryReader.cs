using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1
{
    class QueryReader
    {
        public static string[] ReadQuery(string querypath) // A function that reads queries into memory
        {
            string[] queryContents = new string[16]; // Start with a default size of 2^4
            using (StreamReader queryFile = File.OpenText(querypath)) // The query file only needs to be open while the program reads the queries into memory
                {
                    int i;
                    string query;
                    for (i = 1; (query = queryFile.ReadLine()) != null; i++)
                    {
                        if (i > queryContents.Length) System.Array.Resize(ref queryContents, i * 2); // Use an array twice as large if there are more queries than anticipated
                        if (query.Trim(' ') == "") i--;
                        else queryContents[i - 1] = query.Trim(' ');
                    }
                    System.Array.Resize(ref queryContents, i - 1); // Fix the final size of the array of queries
                }
            return queryContents;
        }
    }
}
