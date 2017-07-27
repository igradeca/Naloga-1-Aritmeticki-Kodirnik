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
        //List<ProbabilityData> probabilityTable;
        ProbabilityData[] probabilityTable;
        ulong frequenciesSum;

        EncodingSimbolData[] encodingTable;

        public AritmeticMain(int encodingBits) {

            this.encodingBits = encodingBits;
            encodingInit = new EncodingInitializationData(encodingBits);
            //probabilityTable = new List<ProbabilityData>();
            probabilityTable = new ProbabilityData[256];
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
                probabilityTable[x.Value] = new ProbabilityData(x.Value, (ulong)x.Count);                
                frequenciesSum += (ulong)x.Count;
                //probabilityTable.Add(new ProbabilityData(x.Value, x.Count));
                //Console.WriteLine("Value: " + x.Value + " Count: " + x.Count);
            }
        }

        private void CompleteInitializationTable() {

            bool firstElement = true;
            int previousIndex = 0;
            for (int i = 0; i < probabilityTable.Length; i++) {
                if (probabilityTable[i] != null) {
                    if (firstElement) {
                        probabilityTable[i].probability = (float)probabilityTable[i].frequency / (float)frequenciesSum;
                        probabilityTable[i].lowerBoundary = 0;
                        probabilityTable[i].upperBoundary = probabilityTable[i].frequency;
                        firstElement = false;
                    } else {
                        probabilityTable[i].probability = (float)probabilityTable[i].frequency / (float)frequenciesSum;
                        probabilityTable[i].lowerBoundary = probabilityTable[previousIndex].upperBoundary;
                        probabilityTable[i].upperBoundary = probabilityTable[i].lowerBoundary + probabilityTable[i].frequency;
                    }
                    previousIndex = i;
                }
            }
            /*
            probabilityTable[0].probability = (float)probabilityTable[0].frequency / (float)frequenciesSum;
            probabilityTable[0].lowerBoundary = 0;
            probabilityTable[0].upperBoundary = probabilityTable[0].frequency;
            for (int i = 1; i < probabilityTable.Length; i++) {
                probabilityTable[i].probability = (float)probabilityTable[i].frequency / (float)frequenciesSum;
                probabilityTable[i].lowerBoundary = probabilityTable[i - 1].upperBoundary;
                probabilityTable[i].upperBoundary = probabilityTable[i].lowerBoundary + probabilityTable[i].frequency;
            }
            */
        }

        private void SetEncodingTable(byte[] data) {

            ulong oldLowerBoundary = encodingInit.minBound;
            ulong oldUpperBoundary = encodingInit.maxBound;            
            ulong step;
            ulong newLowerBoundary, newUpperBoundary;
            ulong E1orE2_Lower = 0, E1orE2_Upper = 0, E3_Lower = 0, E3_Upper = 0;
            int E3_Counter = 0;

            byte simbol;
            for (int i = 0; i < data.Length; i++) {

                simbol = data[i];

                step = (oldUpperBoundary - oldLowerBoundary + 1) / frequenciesSum;
                newUpperBoundary = oldLowerBoundary + (step * probabilityTable[simbol].upperBoundary) - 1;
                newLowerBoundary = oldLowerBoundary + (step * probabilityTable[simbol].lowerBoundary);

                ScaleIntervals(ref E1orE2_Lower, ref E3, ref E3_Counter, ref newLowerBoundary, ref newUpperBoundary);

                encodingTable[i] = new EncodingSimbolData();
                //encodingTable[i].Add(simbol, step, oldLowerBoundary, oldUpperBoundary, newLowerBoundary, newUpperBoundary, E1orE2, E3, E3_Counter);

                oldLowerBoundary = newLowerBoundary;
                oldUpperBoundary = newUpperBoundary;
            }
            
        }

        private void ScaleIntervals(ref ulong E1orE2, ref ulong E3, ref int E3_Counter, ref ulong lowerBoundary, ref ulong upperBoundary) {

            //bool E1 = false, E2 = false;
            bool E1AndE2Done = false;
            while (!E1AndE2Done) {                
                if (upperBoundary < encodingInit.secondQuater) {        // E1
                    lowerBoundary *= 2;
                    upperBoundary = (upperBoundary * 2) + 1;
                    // na izhod se salje bit 0 in E3_Counter krat bit 1
                } else if (lowerBoundary >= encodingInit.secondQuater) {        // E2
                    lowerBoundary = 2 * (lowerBoundary - encodingInit.secondQuater);
                    upperBoundary = (2 * (upperBoundary - encodingInit.secondQuater)) + 1;
                    // na izhod se salje bit 1 in E3_Counter krat bit 0
                } else {
                    E1AndE2Done = true;
                }
            }

            // E3
            if (lowerBoundary >= encodingInit.firstQuater && upperBoundary < encodingInit.thirdQuarter) {
                lowerBoundary = 2 * (lowerBoundary - encodingInit.firstQuater);
                upperBoundary = 2 * (upperBoundary - encodingInit.firstQuater) + 1;
                ++E3_Counter;
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
        /*
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
        */
        private void TotalFreqCheck(byte[] data) {

            ulong totalFreq = 0;
            foreach (ProbabilityData item in probabilityTable) {
                totalFreq += item.frequency;
            }

            Console.WriteLine("Total freq calc: " + totalFreq);
            Console.WriteLine("Total freq: " + frequenciesSum);
            Console.WriteLine("Input data length: " + data.Length);
        }


    }
}
