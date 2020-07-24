using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1
{
    class Level4
    {
        public static void WriteSequencesIndexed(string fastaPath, string indexPath, string queryPath, string outputPath)
        {
            IndexEntry[] index;
            if (File.Exists(indexPath)) index = LoadIndex(indexPath); // Load the index into memory
            else { Console.WriteLine("Error: File \"{0}\" not found.", indexPath); return; }
            string[] query;
            if (File.Exists(queryPath)) query = QueryReader.ReadQuery(queryPath); // Load the query into memory
            else { Console.WriteLine("Error: File \"{0}\" not found.", queryPath); return; }
            string output = "";
            WriteQueryContentsToFile(fastaPath, outputPath, index, query, ref output);
            using (FileStream outputFile = File.OpenWrite(outputPath)) outputFile.SetLength(outputFile.Length - 2); // Remove the last character from the output file.
            if (output == "") return; // If the output is empty, end the function.
            Console.Write(output);
        }
        private static void WriteQueryContentsToFile(string fastaPath, string outputPath, IndexEntry[] index, string[] query, ref string output)
        {
            using (StreamWriter outputFile = File.CreateText(outputPath))
                for (int i = 0; i < query.Length; i++) // Loop through the queries in the query
                {
                    Sequence seq = new Sequence();
                    long indexOfSequence = Array.IndexOf(index.Select(entry => entry.Nr).ToArray(), query[i]); // Search through the sequence numbers in the index
                    // Technically, there is an index entry called "EOF" - however, it is not a valid sequence, therefore, it's hardcoded not to find "EOF".
                    if (indexOfSequence == -1 || query[i] == "EOF") output += string.Format("Sequenece \"{0}\" was not found in the file \"{1}\".\n", query[i], fastaPath);
                    else
                    {
                        long StartingPos = index[indexOfSequence].StartingPosition;
                        long nextSequenceIndex = indexOfSequence + 1; // Start searching for the next sequence from the next sequence number
                        long EndingPos = 0; // Placeholder value
                        if (indexOfSequence < index.Length) // While not looking at the last sequence in the file
                        {
                            while (index[nextSequenceIndex].StartingPosition == index[indexOfSequence].StartingPosition) // If the next SeqNr belongs to the same sequence, keep searching
                            {
                                nextSequenceIndex++;
                            }
                            EndingPos = index[nextSequenceIndex].StartingPosition; // Once the next sequence is found, record its' starting position.
                        }
                        // Handling the edge case of the last sequence in the fasta file: the final entry in the index is not a sequence, but the EOF position of the file
                        // which can be found as the very last index entry.
                        else EndingPos = index[index.Length - 1].StartingPosition;
                        string detailsLine = "";
                        string basePairs = "";
                        using (FileStream fastaFile = File.OpenRead(fastaPath)) // Reading the relevant lines from the fasta file
                        {
                            bool inDetailsLine = true;
                            fastaFile.Seek(StartingPos, 0);
                            for (long charIndex = StartingPos; charIndex < EndingPos; charIndex++)
                            {
                                char CurrChar = Convert.ToChar(fastaFile.ReadByte()); // Read the current character
                                if (CurrChar == '\n') inDetailsLine = false;
                                else if (inDetailsLine) detailsLine += CurrChar;
                                else basePairs += CurrChar;
                            }
                            seq.ParseDetailsLine(detailsLine);
                            seq.BasePairs = basePairs;
                            outputFile.WriteLine(seq.ToString("details"));
                            outputFile.WriteLine(seq.BasePairs);
                        }
                    }
                }

            return;
        }
        public static IndexEntry[] LoadIndex(string indexPath)
        {
            IndexEntry[] index = new IndexEntry[256]; // Create the index object in memory with a starting size
            // Load index into memory
            using (StreamReader indexFile = File.OpenText(indexPath))
            {
                int i;
                string line;
                for (i = 1; (line = indexFile.ReadLine()) != null; i++)
                {
                    if (i > index.Length) System.Array.Resize(ref index, i * 2); // Use an array twice as large if there are more sequences than anticipated
                    string[] entrydetails = new string[2];
                    entrydetails = line.Split(' ');
                    index[i - 1] = new IndexEntry();
                    index[i - 1].Nr = entrydetails[0];
                    index[i - 1].StartingPosition = long.Parse(entrydetails[1]);
                }
                System.Array.Resize(ref index, i - 1); // Correct the final size of the index array
            }
            return index;
        }
    }
}