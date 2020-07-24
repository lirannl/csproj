using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) { Console.WriteLine("Error: No action was specified."); preventClosingDebug(); }
            else if (args.Length < 2) { Console.WriteLine("Error: No fasta file was specified."); preventClosingDebug(); }
            else if (File.Exists(args[1])) try
            {
                switch (args[0])
                {
                    case "-level1":
                        if (args.Length == 4) Console.Write(Level1.getSequenceRange(args[1], args[2], args[3]));
                        else DisplayMissingArgsError(args, false);
                        preventClosingDebug();
                        break;

                    case "-level2":
                        if (args.Length == 3) Console.Write(Level2.getStringFromSeqNum(args[1], args[2]));
                        else DisplayMissingArgsError(args, false);
                        preventClosingDebug();
                        break;

                    case "-level3":
                        if (args.Length == 4) Level3.WriteSequencesFromFile(args[1], args[2], args[3]);
                        else DisplayMissingArgsError(args, false);
                        preventClosingDebug();
                        break;

                    case "-level4":
                        if (args.Length == 5) Level4.WriteSequencesIndexed(args[1], args[2], args[3], args[4]);
                        else DisplayMissingArgsError(args, false);
                        preventClosingDebug();
                        break;

                    case "-level5":
                        if (args.Length == 3) Console.Write(Level5.FindSequencesWithPart(args[1], args[2]));
                        else DisplayMissingArgsError(args, false);
                        preventClosingDebug();
                        break;

                    case "-level6":
                        if (args.Length == 3) Console.Write(Level6.FindSequencesWithMetaData(args[1], args[2]));
                        else DisplayMissingArgsError(args, false);
                        preventClosingDebug();
                        break;

                    case "-level7":
                        if (args.Length == 3) Console.Write(Level7.FindSequencesWithPartRE(args[1], args[2]));
                        else DisplayMissingArgsError(args, false);
                        preventClosingDebug();
                        break;

                    default:
                        Console.WriteLine("Error: invalid action. Must be -level[1-7].");
                        preventClosingDebug();
                        break;
                }
            }
            catch (IndexOutOfRangeException) // Missing arguments
                {
                    DisplayMissingArgsError(args);
                }
            else
                {
                    Console.WriteLine("Error: File \"{0}\" not found.", args[1]); preventClosingDebug();
                }
        }

        private static void DisplayMissingArgsError(string[] args, bool tooMany = true)
        {
            if (tooMany) Console.WriteLine($"Error: Not enough arguments were provided for \"{args[0]}\".");
            else Console.WriteLine($"Error: Too many arguments were provided for \"{args[0]}\".");
            Console.Write($"When running \"{args[0]}\", you must provide the following arguments: .fasta filename, ");
            switch (args[0])
            {
                case "-level1":
                    Console.WriteLine("line to start reading from, number of sequences to read");
                    break;

                case "-level2":
                    Console.WriteLine("Sequenece identifier (\"number\")");
                    break;

                case "-level3":
                    Console.WriteLine("Query file (a list of sequence identifiers to search), filename for the result");
                    break;

                case "-level4":
                    Console.WriteLine("index filename, Query file (a list of sequence identifiers to search), filename for the result");
                    break;

                case "-level5":
                    Console.WriteLine("DNA part to match (combinations of the 4 base pairs)");
                    break;

                case "-level6":
                    Console.WriteLine("metadata substring to match");
                    break;

                case "-level7":
                    Console.WriteLine("DNA part to match (combinations of the 4 base pairs) - including regular expression wildcards");
                    break;

                        default:
                    break;
            }
            preventClosingDebug();
        }

        public static void preventClosingDebug()
        {
            #if DEBUG
                Console.WriteLine("\nPress enter to close...");
                Console.ReadLine();
            #endif
        }
    }
}
