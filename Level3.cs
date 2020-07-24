using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1
{
    class Level3
    {
        public static void WriteSequencesFromFile(string fastaPath, string querypath, string outputpath) // Find the sequences from a query file, and write them to the specified output file.
        {
            string[] query;
            if (File.Exists(querypath)) query = QueryReader.ReadQuery(querypath); // Load the query into memory
            else { Console.WriteLine("Error: file \"{0}\" not found", querypath); return; } // If the query file wasn't found, output an error and stop the function.
            using (StreamWriter outputFile = File.CreateText(outputpath)) // Open a new output file, clearing the file if it already exists.
            {
                string consoleOutput = "";
                for (int i = 0; i < query.Length; i++)
                {
                    Sequence queryresult = FindSequence.GetSequence(fastaPath, query[i]);
                    if (queryresult.SeqNr[0] == "Not found") // If a sequence wasn't found
                    {
                        consoleOutput += string.Format("Sequenece \"{0}\" was not found in the file \"{1}\".", query[i], fastaPath); // Add an error to the output
                        if (i < query.Length - 1) consoleOutput += '\n';
                    }
                    else
                    {
                        outputFile.WriteLine(queryresult.ToString("details"));
                        outputFile.WriteLine(queryresult.BasePairs);
                        if (i < query.Length - 1)
                        {
                            outputFile.WriteLine(); // Add lines as long as the current line isn't the last one (so that the file doesn't end with an empty line)
                        }
                    }
                }
                if (consoleOutput != "") // If no errors are to be shown
                {
                    Console.Write(consoleOutput);
                }
            }
        }
    }
}
