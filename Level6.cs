using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1
{
    class Level6
    {
        public static string FindSequencesWithMetaData(string fastaPath, string metaPart)
        {
            string output = "";
            Sequence seq = new Sequence(); // Start with a large number of potential matches which will increase if needed
            using (StreamReader fastaFile = File.OpenText(fastaPath))
            {
                string line;
                for (int i = 1; (line = fastaFile.ReadLine()) != null; i++) // For each line in the fasta file
                {
                    if (line.Substring(0, 1) == ">") seq.ParseDetailsLine(line);
                    else
                    {
                        seq.BasePairs = line; // Save the base pairs of the current sequence.
                        // If the part exists in the current sequence, add the current sequence number to the output string.
                        string allMetaData = string.Join("",seq.getMetaData()); // Join all of the metadatas of each sequence together into one string
                        foreach (string SequenceNumber in seq.SeqNr) // Add each 
                        {
                            if (allMetaData.Contains(metaPart)) output += string.Format("{0}\n", SequenceNumber);
                        }
                    }
                }

            }
            if (output == "") output += string.Format("No matches were found with {0} in their metadata.\n", metaPart);
            return output.Substring(0, output.Length - 1);
        }
    }
}
