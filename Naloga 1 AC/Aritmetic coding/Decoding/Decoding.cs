using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritmetic_coding {
    public class Decoding {

        List<byte> output;
        BitsReader reader;

        ulong[] simbolFrequencies;
        ulong[] lowerBoundaries;
        ulong[] upperBoundaries;
        ulong frequenciesSum;
        byte encodingBits;

        ulong field;
        byte[] simbolValues;
        
        public Decoding(byte byteValue) {

            simbolFrequencies = new ulong[256];
            lowerBoundaries = new ulong[256];
            upperBoundaries = new ulong[256];
            frequenciesSum = 0;
            SetBitsNumber(byteValue);            
        }
        
        private void SetBitsNumber(byte byteValue) {

            encodingBits = 0;
            switch (byteValue) {
                case 0:
                    encodingBits = 8;
                    break;
                case 1:
                    encodingBits = 16;
                    break;
                case 2:
                    encodingBits = 32;
                    break;
                case 3:
                    encodingBits = 64;
                    break;
            }
        }

        public byte[] Decode(byte[] inputData) {

            output = new List<byte>();
            reader = new BitsReader(inputData);

            SetProbabilityTable(inputData);
            CompleteInitializationTable();

            field = reader.GetDataField((byte)(encodingBits - 1));

            SetSimbolValuesArray();

            SetDecodingTable();

            return output.ToArray();
        }

        private void SetProbabilityTable(byte[] data) {

            for (int i = 1; i < 1025; i += 4) {
                byte[] intVal = new byte[4];
                ulong k = 0;
                for (int j = i; j < (i + 4); j++, k++) {
                    intVal[k] = data[j];
                }

                k = BitConverter.ToUInt32(intVal, 0);

                if (k != 0) {
                    byte simbol = (byte)((i - 1)/ 4);
                    simbolFrequencies[simbol] = k;
                    frequenciesSum += k;                    
                }                
            }
        }

        private void CompleteInitializationTable() {

            bool firstElement = true;
            int previousIndex = 0;
            for (int i = 0; i < simbolFrequencies.Length; i++) {
                if (simbolFrequencies[i] != 0) {
                    if (firstElement) {
                        lowerBoundaries[i] = 0;
                        upperBoundaries[i] = (ulong)simbolFrequencies[i];
                        firstElement = false;
                    } else {
                        lowerBoundaries[i] = upperBoundaries[previousIndex];
                        upperBoundaries[i] = lowerBoundaries[i] + (ulong)simbolFrequencies[i];
                    }
                    previousIndex = i;
                }
            }
        }

        private void SetSimbolValuesArray() {

            simbolValues = new byte[frequenciesSum];

            for (int i = 0, j = 0; i < 256; i++) {
                if (simbolFrequencies[i] != 0) {
                    for (ulong k = lowerBoundaries[i]; k < upperBoundaries[i]; k++) {
                        simbolValues[j++] = (byte)i;
                    }
                }                
            }
        }

        private void SetDecodingTable() {

            ulong minBound = 0;
            var maxBound = (ulong)Math.Pow(2, (encodingBits - 1)) - 1;
            var secondQuater = (maxBound + 1) / 2;
            var firstQuater = secondQuater / 2;
            var thirdQuarter = firstQuater * 3;

            ulong step, value;
            byte simbol;

            ulong i = 0;
            while (true) {
                step = (maxBound - minBound + 1) / frequenciesSum;
                value = (field - minBound) / step;

                simbol = simbolValues[value];

                output.Add(simbol);

                maxBound = minBound + (step * upperBoundaries[simbol]) - 1;
                minBound = minBound + (step * lowerBoundaries[simbol]);

                if (++i >= frequenciesSum) {
                    break;
                }

                while ((maxBound < secondQuater) || (minBound >= secondQuater)) {
                    if (maxBound < secondQuater) {    // E1
                        minBound = minBound << 1;
                        maxBound = (maxBound << 1) + 1;                        
                        field = (field << 1) + (ulong)(reader.ReadBit() ? 1 : 0);
                    }
                    else if (minBound >= secondQuater) {    // E2
                        minBound = (minBound - secondQuater) << 1;
                        maxBound = ((maxBound - secondQuater) << 1) + 1;                        
                        field = ((field - secondQuater) << 1) + (ulong)(reader.ReadBit() ? 1 : 0);
                    }
                }
                // E3
                while ((minBound >= firstQuater) && (maxBound < thirdQuarter)) {
                    minBound = (minBound - firstQuater) << 1;
                    maxBound = ((maxBound - firstQuater) << 1) + 1;                    
                    field = ((field - firstQuater) << 1) + (ulong)(reader.ReadBit() ? 1 : 0);
                }
            }
        }

    }
}
