using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritmetic_coding
{
    public class AritmeticMain
    {
        int encodingBits;
        EncodingInitializationData encodingInit;
        List<ProbabilityData> probabilityTable;
        ulong frequenciesSum;

        EncodingSimbolData[] encodingTable;

        public AritmeticMain(int encodingBits) {

            this.encodingBits = encodingBits;
            encodingInit = new EncodingInitializationData(encodingBits);
            probabilityTable = new List<ProbabilityData>();
            frequenciesSum = 0;            
        }

        public string Author() {

            string result = "Ivan" + " " + "Gradecak" + " " + "E5031111";
            return result;
        }

        public byte[] Encode(byte[] data) {

            byte[] result = new byte[data.Length];
            //data.CopyTo(result, 0);

            //Array.Sort(data);
            //InitializeProbabilityTable(data);
            SetProbabilityTable(data);
            CompleteInitializationTable();

            encodingTable = new EncodingSimbolData[frequenciesSum];

            SetEncodingTable(data);
            PrintEncodingTable();

            //TotalFreqCheck(data);

            return result;
        }

        private void SetProbabilityTable(byte[] data) {

            var q = data.GroupBy(x => x)
            .Select(g => new { Value = g.Key, Count = g.Count() });

            foreach (var x in q) {
                probabilityTable.Add(new ProbabilityData(x.Value, x.Count));
                frequenciesSum += (ulong)x.Count;
                //Console.WriteLine("Value: " + x.Value + " Count: " + x.Count);
            }
        }

        private void CompleteInitializationTable() {

            probabilityTable[0].probability = (float)probabilityTable[0].frequency / (float)frequenciesSum;
            probabilityTable[0].lowerBoundary = 0;
            probabilityTable[0].upperBoundary = probabilityTable[0].frequency;
            for (int i = 1; i < probabilityTable.Count; i++) {
                probabilityTable[i].probability = (float)probabilityTable[i].frequency / (float)frequenciesSum;
                probabilityTable[i].lowerBoundary = probabilityTable[i - 1].upperBoundary;
                probabilityTable[i].upperBoundary = probabilityTable[i].lowerBoundary + probabilityTable[i].frequency;
            }
        }

        private void SetEncodingTable(byte[] data) {

            ulong oldLowerBoundary = encodingInit.minBound;
            ulong oldUpperBoundary = encodingInit.maxBound;            
            ulong step;
            ulong newLowerBoundary, newUpperBoundary;

            for (int i = 0; i < data.Length; i++) {

                step = (oldUpperBoundary - oldLowerBoundary + 1) / frequenciesSum;
                //newLowerBoundary = oldLowerBoundary + (step * );
            }
            
        }

        private void PrintEncodingTable() {

            Console.WriteLine(string.Format("\n  {0,6} {1,7} {2,8} {3,8} {4,8} {5,8} {6,8} {7,5} {8,13}", 
                "Simbol", "Step", "Old L", "Old H", "New L", "New H", "E1/E2", "E3", "E3 Counter"));
            Console.WriteLine("-----------------------------------------------------------------------------------");

            for (int i = 0; i < encodingTable.Length; i++) {
                Console.WriteLine(string.Format("  {0,6} {1,7} {2,8} {3,8} {4,8} {5,8} {6,8} {7,5} {8,13}",
                    (char)encodingTable[i].simbol, encodingTable[i].step, encodingTable[i].oldLowerBoundary, encodingTable[i].oldUpperBoundary, 
                    encodingTable[i].newLowerBoundary, encodingTable[i].newUpperBoundary, encodingTable[i].E1orE2, encodingTable[i].E3, encodingTable[i].E3_Counter));
            }
        }

        private void InitializeProbabilityTable(byte[] data) {      // za sortirani niz

            byte simbol;
            int frequency;
            int counter = 1;

            for (int i = 1; i < data.Length; i++) {
                if (data[i - 1] != data[i]) {
                    simbol = data[i - 1];
                    frequency = counter;
                    probabilityTable.Add(new ProbabilityData(simbol, frequency));
                    frequenciesSum += (ulong)counter;
                    //Console.WriteLine(data[i - 1] + " " + counter);
                    counter = 1;
                } else {
                    ++counter;
                }
            }
            simbol = data[data.Length - 1];
            frequency = counter;
            probabilityTable.Add(new ProbabilityData(simbol, frequency));
            frequenciesSum += (ulong)counter;
            //Console.WriteLine(data[data.Length - 1] + " " + counter);
        }

        private void TotalFreqCheck(byte[] data) {

            int totalFreq = 0;
            foreach (ProbabilityData item in probabilityTable) {
                totalFreq += item.frequency;
            }

            Console.WriteLine("Total freq calc: " + totalFreq);
            Console.WriteLine("Total freq: " + frequenciesSum);
            Console.WriteLine("Input data length: " + data.Length);
        }


    }
}
