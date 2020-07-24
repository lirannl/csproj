using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1
{
    class Level1
    {
        public static string getSequenceRange(string fastaPath, string rawStartPos, string halfOfLength)
        {
            using (StreamReader fastafile = File.OpenText(fastaPath)) // Open the fasta file
            {
                string output = "";
                string line;
                long startPos;
                long length;
                // Process the command arguments into a usable number (with errors if the number isn't usable
                long[] startPosParse = CustomParser.SafeParse(rawStartPos, allowNegatives: false);
                long[] lengthParse = CustomParser.SafeParse(halfOfLength, allowNegatives: false, blacklist: 0);
                Sequence[] seqs; // A variable that stores the output
                if (startPosParse[1] == 1 && startPosParse[0] % 2 == 1 && lengthParse[1] == 1) // If the parses are valid and the starting position is odd
                {
                    startPos = startPosParse[0];
                    length = lengthParse[0] * 2; // Convert from number of sequences to number of lines
                    seqs = new Sequence[length / 2]; // Once the length is known, allocate the appropriate amount of memory to the output.
                    for (int i = 1; (line = fastafile.ReadLine()) != null; i++) // For each line in the fasta file
                    {
                        if (i >= (startPos)) // After starting position is reached
                        {
                            long seqNum = (i - startPos) / 2; // Update the current index in the output
                            if (line.Substring(0, 1) == ">") // If the current line starts with a > sign then it's a sequence details line.
                            {
                                seqs[seqNum] = new Sequence();
                                seqs[seqNum].ParseDetailsLine(line);
                            }
                            else // Base pair lines
                            {
                                seqs[seqNum].BasePairs = line;
                            }
                        }
                        if (i == (startPos + length) - 1) break; // Stop reading sequences after the specified number of objects were read
                    }
                    if (seqs[seqs.Length-1] == null) { Console.WriteLine("Error: The number of sequences to print was too large and exceeded the fasta file's length."); return ""; }
                    foreach (Sequence seq in seqs)
                    {
                        output += seq.ToString() + "\n"; // Build an output string out of all of the sequences
                    }
                }
                if (startPosParse[0] % 2 == 0) Console.Write("Error: Starting line must be odd in the {0} argument.\n", 3.Ordinal());
                // Append the appropriate argument numbers to the parsing errors.
                else if (startPosParse[1] == 0) Console.Write(" in the {0} argument.\n", 3.Ordinal());
                if (lengthParse[1] == 0) Console.Write(" in the {0} argument.\n", 4.Ordinal());
                if (output == "") return output;
                return output.Substring(0, output.Length - 1); // Return the output string without the last newline.
            }
        }
    }
}
