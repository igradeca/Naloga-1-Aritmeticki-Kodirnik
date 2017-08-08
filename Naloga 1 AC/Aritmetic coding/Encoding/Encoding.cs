using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritmetic_coding {
    public class Encoding
    {
        int encodingBits;
        EncodingInitializationData encodingInit;
        ProbabilityData[] probabilityTable;
        ulong frequenciesSum;

        EncodingSimbolData[] encodingTable;

        BitsWriter writer;

        public Encoding(int encodingBits) {

            this.encodingBits = encodingBits;
            encodingInit = new EncodingInitializationData(encodingBits);
            probabilityTable = new ProbabilityData[256];
            frequenciesSum = 0;
        }

        public void InitializeWriter(int inputDataLenght) {

            writer = new BitsWriter(inputDataLenght, encodingBits);
        }

        public string Author() {

            string result = "Ivan" + " " + "Gradecak" + " " + "E5031111";
            return result;
        }

        public byte[] Encode(byte[] inputData) {
            //data.CopyTo(result, 0);

            //Array.Sort(data);
            //InitializeProbabilityTable(data);
            SetProbabilityTable(inputData);
            CompleteInitializationTable();

            writer.InsertSimbolsAndFrequencies(probabilityTable);

            encodingTable = new EncodingSimbolData[frequenciesSum];

            SetEncodingTable(inputData);

            //PrintEncodingTable();
            //writer.PrintResult();

            //TotalFreqCheck(data);

            return writer.ReturnResult();
        }

        private void SetProbabilityTable(byte[] data) {

            var q = data.GroupBy(x => x)
            .Select(g => new { Value = g.Key, Count = g.Count() });

            foreach (var x in q) {
                probabilityTable[x.Value] = new ProbabilityData(x.Value, x.Count);                
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
                        probabilityTable[i].upperBoundary = (ulong)probabilityTable[i].frequency;
                        firstElement = false;
                    } else {
                        probabilityTable[i].probability = (float)probabilityTable[i].frequency / (float)frequenciesSum;
                        probabilityTable[i].lowerBoundary = probabilityTable[previousIndex].upperBoundary;
                        probabilityTable[i].upperBoundary = probabilityTable[i].lowerBoundary + (ulong)probabilityTable[i].frequency;
                    }
                    previousIndex = i;
                }
            }
        }

        private void SetEncodingTable(byte[] data) {

            ulong oldLowerBoundary = encodingInit.minBound;
            ulong oldUpperBoundary = encodingInit.maxBound;            
            ulong step;
            ulong newLowerBoundary, newUpperBoundary;
            ulong E1E2Lower, E1E2Upper;
            ulong E3Lower, E3Upper;
            int E3_Counter = 0;

            byte simbol;
            for (int i = 0; i < data.Length; i++) {

                simbol = data[i];

                E1E2Lower = 0;
                E1E2Upper = 0;
                E3Lower = 0;
                E3Upper = 0;

                step = (oldUpperBoundary - oldLowerBoundary + 1) / frequenciesSum;
                newUpperBoundary = oldLowerBoundary + (step * probabilityTable[simbol].upperBoundary) - 1;
                newLowerBoundary = oldLowerBoundary + (step * probabilityTable[simbol].lowerBoundary);

                ScaleIntervals(newLowerBoundary, newUpperBoundary, ref E1E2Lower, ref E1E2Upper, ref E3Lower, ref E3Upper, ref E3_Counter);

                // za ispis tablice
                //encodingTable[i] = new EncodingSimbolData();
                //encodingTable[i].Add(simbol, step, oldLowerBoundary, oldUpperBoundary, newLowerBoundary, newUpperBoundary, 
                    //E1E2Lower, E1E2Upper, E3Lower, E3Upper, E3_Counter);

                oldLowerBoundary = E3Lower;
                oldUpperBoundary = E3Upper;                
            }

            FinishEncoding(oldLowerBoundary, E3_Counter);
        }

        private void ScaleIntervals(ulong lowerBoundary, ulong upperBoundary, 
            ref ulong E1E2Lower, ref ulong E1E2Upper, ref ulong E3Lower, ref ulong E3Upper, ref int E3_Counter) {

            while ((upperBoundary < encodingInit.secondQuater) || (lowerBoundary >= encodingInit.secondQuater)) {
                if (upperBoundary < encodingInit.secondQuater) {    // E1
                    lowerBoundary = lowerBoundary * 2;
                    upperBoundary = (upperBoundary * 2) + 1;

                    // na izhod se salje bit 0 in E3_Counter krat bit 1
                    writer.WriteSingleBit(false);
                    if (E3_Counter > 0) {                        
                        writer.WriteBits(true, E3_Counter);
                        E3_Counter = 0;
                    }
                } else if (lowerBoundary >= encodingInit.secondQuater) {    // E2
                    lowerBoundary = 2 * (lowerBoundary - encodingInit.secondQuater);
                    upperBoundary = (2 * (upperBoundary - encodingInit.secondQuater)) + 1;

                    // na izhod se salje bit 1 in E3_Counter krat bit 0
                    writer.WriteSingleBit(true);
                    if (E3_Counter > 0) {                        
                        writer.WriteBits(false, E3_Counter);
                        E3_Counter = 0;
                    }
                }
            } 
            
            E1E2Lower = lowerBoundary;
            E1E2Upper = upperBoundary;

            // E3
            while ((lowerBoundary >= encodingInit.firstQuater) && (upperBoundary < encodingInit.thirdQuarter)) {
                lowerBoundary = 2 * (lowerBoundary - encodingInit.firstQuater);
                upperBoundary = 2 * (upperBoundary - encodingInit.firstQuater) + 1;
                ++E3_Counter;
            }

            E3Lower = lowerBoundary;
            E3Upper = upperBoundary;       
        }

        private void FinishEncoding(ulong lowerBoundary, int E3_Counter) {

            if (lowerBoundary < encodingInit.firstQuater) {
                writer.WriteSingleBit(false);
                writer.WriteSingleBit(true);
                writer.WriteBits(true, E3_Counter);
            } else {
                writer.WriteSingleBit(true);
                writer.WriteSingleBit(false);
                writer.WriteBits(false, E3_Counter);
            }
        }

        private void PrintEncodingTable() {

            Console.WriteLine(string.Format("\n  {0,6} {1,7} {2,8} {3,8} {4,8} {5,8} {6,10} {7,7} {8,13}", 
                "Simbol", "Step", "Old L", "Old H", "New L", "New H", "E1/E2", "E3", "E3 Counter"));
            Console.WriteLine("---------------------------------------------------------------------------------------");

            for (int i = 0; i < encodingTable.Length; i++) {
                Console.WriteLine(string.Format("  {0,6} {1,7} {2,8} {3,8} {4,8} {5,8} {6,10} {7,7} {8,13}",
                    (char)encodingTable[i].simbol, encodingTable[i].step, encodingTable[i].oldLowerBoundary, encodingTable[i].oldUpperBoundary, 
                    encodingTable[i].newLowerBoundary, encodingTable[i].newUpperBoundary, (encodingTable[i].E1E2Lower + " " + encodingTable[i].E1E2Upper), 
                    (encodingTable[i].E3Lower + " " + encodingTable[i].E3Upper), encodingTable[i].E3_Counter));
            }
        }

        private void TotalFreqCheck(byte[] data) {

            ulong totalFreq = 0;
            foreach (ProbabilityData item in probabilityTable) {
                totalFreq += (ulong)item.frequency;
            }

            Console.WriteLine("Total freq calc: " + totalFreq);
            Console.WriteLine("Total freq: " + frequenciesSum);
            Console.WriteLine("Input data length: " + data.Length);
        }

        /*
        private void InitializeProbabilityTable(byte[] data) {      // za sortirani niz, to netreba ako nebus isel delat ko na prezentaciji

            byte simbol;
            int frequency;
            int counter = 1;

            for (int i = 1; i < data.Length; i++) {
                if (data[i - 1] != data[i]) {
                    simbol = data[i - 1];
                    frequency = counter;
                    //probabilityTable.Add(new ProbabilityData(simbol, frequency));
                    frequenciesSum += (ulong)counter;
                    //Console.WriteLine(data[i - 1] + " " + counter);
                    counter = 1;
                } else {
                    ++counter;
                }
            }
            simbol = data[data.Length - 1];
            frequency = counter;
            //probabilityTable.Add(new ProbabilityData(simbol, frequency));
            frequenciesSum += (ulong)counter;
            //Console.WriteLine(data[data.Length - 1] + " " + counter);
        }
        */


    }
}
