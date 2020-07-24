using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1
{
    class Sequence
    {
        public string[] SeqNr;
        public string[] Organism;
        public string[] NucleicType;
        public string[] Completeness;
        public string BasePairs;

        public Sequence(string[] SeqNr = null, string[] NucleicType = null, string[] Organism = null, string BasePairs = null, string[] Completeness = null) // Constructor
        {
            this.SeqNr = SeqNr;
            this.Organism = Organism;
            this.NucleicType = NucleicType;
            this.BasePairs = BasePairs;
            this.Completeness = Completeness;
        }
        public string[] getMetaData(int index = -1)
        {
            if (index > -1) // When a specific metadata index is specified - return just that one
            {
                string[] output = new string[1];
                output[0] = $"{Organism[index]} {NucleicType[index]}, {Completeness[index]}";
                return output;
            }
            string[] metadata = new string[SeqNr.Length];
            for (int i = 0; i < SeqNr.Length; i++) // Otherwise return an array of all metadatas
            {
                metadata[i] = $"{Organism[i]} {NucleicType[i]}, {Completeness[i]}";
            }
            return metadata;
        }

        public void ParseDetailsLine(string line) // Add details to the calling sequence
        {
            string[] rawInstances = line.Split('>'); // Handling the possiblity of multiple details for the same sequence
            string[] instances = new string[rawInstances.Length - 1];
            for (int i = 1; i < rawInstances.Length; i++) // Create a list of sequence details without the first one, which is always empty
            {
                instances[i - 1] = rawInstances[i];
            }
            SeqNr = new string[instances.Length]; Organism = new string[instances.Length]; NucleicType = new string[instances.Length]; Completeness = new string[instances.Length];
            for (int instance = 0; instance < instances.Length; instance++)
            {
                string[] seqprops = instances[instance].Split(' ');
                // Detect details
                string currSeqNr = seqprops[0];
                string currOrganism = "";
                int endOfOrganism = Array.IndexOf(seqprops, "16S");
                for (int i = 1; i < endOfOrganism; i++)
                {
                    if (i > 1) currOrganism += " "; // Space the words out
                    currOrganism += seqprops[i];
                }
                string sequenceType = "";
                for (int i = endOfOrganism; i < seqprops.Length - 2; i++)
                {
                    if (i > endOfOrganism) sequenceType += " "; // Space the words out
                    sequenceType += seqprops[i];
                }
                string completeness = seqprops[seqprops.Length - 2] + " " + seqprops[seqprops.Length - 1]; // Last two words with a space between them


                // Assign the details to the sequence
                SeqNr[instance] = currSeqNr; Organism[instance] = currOrganism.TrimEnd(' '); NucleicType[instance] = sequenceType.Trim(','); Completeness[instance] = completeness.Trim(' ', ',');
            }
        }
        public override string ToString()
        {
            string details = "";
            for (int i = 0; i < SeqNr.Length; i++)
            {
                details += $">{SeqNr[i]} {getMetaData(i)[0]} ";
            }
            return $"{details}\n{BasePairs}";
        }

        public string ToString(string line)
        {
            switch (line)
            {
                case "details":
                    string details = "";
                    for (int i = 0; i < SeqNr.Length; i++)
                    {
                        details += $">{SeqNr[i]} {getMetaData(i)[0]} ";
                    }
                    return details;

                case "basepairs":
                    return BasePairs;

                default:
                    return "";
            }
        }
    }
}
