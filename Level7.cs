using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace A1
{
    class Level7
    {
        public static string FindSequencesWithPartRE(string fastaPath, string re)
        {
            string pattern = re.Replace("*", ".*"); // Replace wildcards with the RE version of a wildcard
            string output = "";
            Sequence seq = new Sequence(); // Start with a large number of potential matches which will increase if needed
            using (StreamReader fastaFile = File.OpenText(fastaPath))
            {
                string line;
                for (int i = 1; (line = fastaFile.ReadLine()) != null; i++) // For each line in the fasta file
                {
                    if (i % 2 == 1) seq.ParseDetailsLine(line);
                    else
                    {
                        seq.BasePairs = line; // Save the base pairs of the current sequence.
                        // If the regex pattern has a match in the current sequence, add the current sequence number to the output string.
                        string reMatch = Regex.Match(seq.BasePairs, pattern).ToString();
                        if (reMatch != "") output += string.Format("{0}\n", seq.SeqNr);
                    }
                }

            }
            if (output == "") output += string.Format("No matches were found matching {0}.\n", re);
            return output.Substring(0, output.Length - 1);
        }
    }
}
