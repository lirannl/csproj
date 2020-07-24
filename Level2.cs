using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace A1
{
    class Level2
    {
        public static string getStringFromSeqNum(string fastaPath, string SeqNum) // Search the .fasta file for a sequence number, and output all available information on it.
        {
            using (StreamReader fastaFile = File.OpenText(fastaPath)) // Open the fasta file
            {
                Sequence seq = new Sequence();
                string line;
                for (int i = 1; (line = fastaFile.ReadLine()) != null; i++) // For each line in the fasta file
                {
                        if (line.Substring(0, 1) == ">") seq.ParseDetailsLine(line); // Read the details line
                        else if (seq.SeqNr.Contains(SeqNum)) { seq.BasePairs = line; break; } // Once the sequence is found, save its base pairs, and stop searching.
                }
                if (!seq.SeqNr.Contains(SeqNum)) // If the sequence wasn't found after looking through the file, display an error
                {
                    Console.WriteLine("Error, sequence {0} not found.", SeqNum);
                    return "";
                }
                return seq.ToString(); // Return the sequence that was found
            }
        }
    }
}
