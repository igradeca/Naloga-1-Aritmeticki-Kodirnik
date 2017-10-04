using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritmetic_coding {
    public class Decoding {

        List<byte> output;
        BitsReader reader;

        EncodingInitializationData decodingInit;
        ProbabilityData[] probabilityTable;
        ulong frequenciesSum;
        byte bitsNumber;

        ulong field;
        byte[] simbolValues;
        
        public Decoding(byte byteValue) {

            probabilityTable = new ProbabilityData[256];
            frequenciesSum = 0;
            SetBitsNumber(byteValue);            
        }
        
        private void SetBitsNumber(byte byteValue) {

            bitsNumber = 0;
            switch (byteValue) {
                case 0:
                    bitsNumber = 8;
                    break;
                case 1:
                    bitsNumber = 16;
                    break;
                case 2:
                    bitsNumber = 32;
                    break;
                case 3:
                    bitsNumber = 64;
                    break;
            }
            decodingInit = new EncodingInitializationData(bitsNumber);
        }

        public byte[] Decode(byte[] inputData) {

            int counter = 0;
            for (int i = 1025; i < inputData.Length; i++) {
                if (inputData[i] == 0) {
                    ++counter;
                }
            }

            output = new List<byte>();
            reader = new BitsReader(inputData);

            //byte[] result = new byte[10];

            SetProbabilityTable(inputData);
            CompleteInitializationTable();

            field = reader.GetDataField((byte)(bitsNumber - 1));

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
                    frequenciesSum += k;
                    probabilityTable[simbol] = new ProbabilityData(simbol, k);
                }                
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

        private void SetSimbolValuesArray() {

            simbolValues = new byte[frequenciesSum];

            for (int i = 0, j = 0; i < 255; i++) {
                if (probabilityTable[i] != null) {                     // Ovaj if sluzi samo radi ABCCD primjera jer nema sva slova
                    for (ulong k = probabilityTable[i].lowerBoundary; k < probabilityTable[i].upperBoundary; k++) {
                        simbolValues[j++] = (byte)i;
                    }
                }                
            }
        }

        private void SetDecodingTable() {

            ulong lowerBoundary = decodingInit.minBound;
            ulong upperBoundary = decodingInit.maxBound;
            ulong step, value;
            byte simbol;

            ulong i = 0;
            while (true) {
                step = (upperBoundary - lowerBoundary + 1) / frequenciesSum;
                value = (field - lowerBoundary) / step;

                simbol = simbolValues[value];

                output.Add(probabilityTable[simbol].simbol);

                upperBoundary = lowerBoundary + (step * probabilityTable[simbol].upperBoundary) - 1;
                lowerBoundary = lowerBoundary + (step * probabilityTable[simbol].lowerBoundary);

                if (++i >= frequenciesSum) {
                    break;
                }

                while ((upperBoundary < decodingInit.secondQuater) || (lowerBoundary >= decodingInit.secondQuater)) {
                    if (upperBoundary < decodingInit.secondQuater) {    // E1
                        lowerBoundary = lowerBoundary * 2;
                        upperBoundary = (upperBoundary * 2) + 1;
                        field = 2 * field + (ulong)(reader.ReadBit() ? 1 : 0);

                    } else if (lowerBoundary >= decodingInit.secondQuater) {    // E2
                        lowerBoundary = 2 * (lowerBoundary - decodingInit.secondQuater);
                        upperBoundary = 2 * (upperBoundary - decodingInit.secondQuater) + 1;
                        field = 2 * (field - decodingInit.secondQuater) + (ulong)(reader.ReadBit() ? 1 : 0);
                    }
                }

                while ((lowerBoundary >= decodingInit.firstQuater) && (upperBoundary < decodingInit.thirdQuarter)) {
                    lowerBoundary = 2 * (lowerBoundary - decodingInit.firstQuater); // (lowerBoundary - encodingInit.firstQuater) << 2
                    upperBoundary = 2 * (upperBoundary - decodingInit.firstQuater) + 1;
                    field = 2 * (field - decodingInit.firstQuater) + (ulong)(reader.ReadBit() ? 1 : 0);
                }
            }

            //return 
        }

    }
}
