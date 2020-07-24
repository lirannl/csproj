using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1
{
    class FindSequence
    {
        public static Sequence GetSequence(string fastaPath, string SeqNum) // The level 2 function, modified to return the sequence found instead of a string.
        {
            using (StreamReader fastaFile = File.OpenText(fastaPath))
            {
                Sequence seq = new Sequence();
                string line;
                for (int i = 1; (line = fastaFile.ReadLine()) != null; i++) // For each line in the fasta file
                {
                    if (line.Substring(0, 1) == ">") seq.ParseDetailsLine(line); // Read the detalis line
                    else if (seq.SeqNr.Contains(SeqNum)) { seq.BasePairs = line; break; } // Once the sequence is found, save its base pairs, and stop searching.
                }
                if (!seq.SeqNr.Contains(SeqNum)) return new Sequence(SeqNr: new string[1] {"Not Found"}); // If the sequence wasn't found after looking through the file, return an invalid sequence
                return seq; // Otherwise return the found sequence
            }
        }
    }
}
