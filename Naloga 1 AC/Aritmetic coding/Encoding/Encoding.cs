using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritmetic_coding {
    public class Encoding
    {
        int encodingBits;
        ulong[] simbolFrequencies;
        ulong[] lowerBoundaries;
        ulong[] upperBoundaries;
        ulong frequenciesSum;

        BitsWriter writer;

        public Encoding(int encodingBits) {

            this.encodingBits = encodingBits;
            simbolFrequencies = new ulong[256];
            lowerBoundaries = new ulong[256];
            upperBoundaries = new ulong[256];
            frequenciesSum = 0;
        }

        public void InitializeWriter(int inputDataLenght) {

            writer = new BitsWriter(inputDataLenght, encodingBits);
        }        

        public byte[] Encode(byte[] inputData) {

            SetProbabilityTable(inputData);
            CompleteInitializationTable();

            writer.InsertSimbolsAndFrequencies(simbolFrequencies);

            SetEncodingTable(inputData);

            return writer.ReturnResult();
        }

        private void SetProbabilityTable(byte[] data) {

            var q = data.GroupBy(x => x)
            .Select(g => new { Value = g.Key, Count = g.Count() });

            foreach (var x in q) {
                simbolFrequencies[x.Value] = (ulong)x.Count;
                frequenciesSum += (ulong)x.Count;
            }
        }

        private void CompleteInitializationTable() {

            bool firstElement = true;
            int previousIndex = 0;
            for (int i = 0; i < simbolFrequencies.Length; i++) {
                if (simbolFrequencies[i] != 0) {
                    lowerBoundaries[i] = firstElement ? 0 : upperBoundaries[previousIndex];
                    upperBoundaries[i] = (ulong)simbolFrequencies[i] + (firstElement ? 0 : lowerBoundaries[i]);
                    firstElement = false;
                    previousIndex = i;
                }
            }
        }

        private void SetEncodingTable(byte[] data) {

            ulong lowerBoundary = 0;
            var upperBoundary = (ulong)(Math.Pow(2, (encodingBits - 1)) - 1);
            var secondQuater = (upperBoundary + 1) / 2;
            var firstQuater = secondQuater / 2;
            var thirdQuarter = firstQuater * 3;

            ulong step;
            int E3_Counter = 0;
            byte simbol;

            for (int i = 0; i < data.Length; i++) {

                simbol = data[i];

                step = (upperBoundary - lowerBoundary + 1) / frequenciesSum;
                upperBoundary = lowerBoundary + (step * upperBoundaries[simbol]) - 1;
                lowerBoundary = lowerBoundary + (step * lowerBoundaries[simbol]);

                while ((upperBoundary < secondQuater) || (lowerBoundary >= secondQuater)) {
                    if (upperBoundary < secondQuater) {    // E1
                        lowerBoundary = lowerBoundary << 1;
                        upperBoundary = (upperBoundary << 1) + 1;

                        // na izhod se salje bit 0 in E3_Counter krat bit 1
                        writer.WriteSingleBit(false);
                        if (E3_Counter > 0) {
                            writer.WriteBits(true, E3_Counter);
                            E3_Counter = 0;
                        }
                    } else if (lowerBoundary >= secondQuater) {    // E2
                        lowerBoundary = (lowerBoundary - secondQuater) << 1;
                        upperBoundary = ((upperBoundary - secondQuater) << 1) + 1;

                        // na izhod se salje bit 1 in E3_Counter krat bit 0
                        writer.WriteSingleBit(true);
                        if (E3_Counter > 0) {
                            writer.WriteBits(false, E3_Counter);
                            E3_Counter = 0;
                        }
                    }
                }
                // E3
                while ((lowerBoundary >= firstQuater) && (upperBoundary < thirdQuarter)) {
                    lowerBoundary = (lowerBoundary - firstQuater) << 1;
                    upperBoundary = ((upperBoundary - firstQuater) << 1) + 1;
                    ++E3_Counter;
                }
            }

            if (lowerBoundary < firstQuater) {
                writer.WriteSingleBit(false);
                writer.WriteSingleBit(true);
                writer.WriteBits(true, E3_Counter);
            } else {
                writer.WriteSingleBit(true);
                writer.WriteSingleBit(false);
                writer.WriteBits(false, E3_Counter);
            }
        }

    }
}
