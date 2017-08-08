using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritmetic_coding {
    public class BitsWriter {

        byte[] result;
        int bitLocation;
        int byteLocation;

        public BitsWriter(int arrayLength, int encodingBits) {

            int resultLenght = arrayLength + 1024 + 1;
            result = new byte[resultLenght];
            byteLocation = 1025;
            bitLocation = 7;

            InsertEncodingBitsValue(encodingBits);
        }

        private void InsertEncodingBitsValue(int encodingBits) {

            if (encodingBits == 8) {            // 8 bit
                result[0] = 0;
            } else if (encodingBits == 16) {    // 16 bit
                result[0] = 1;
            } else if (encodingBits == 32) {    // 32 bit
                result[0] = 2;
            } else {                            // 64 bit
                result[0] = 3;
            }            
        }

        public void InsertSimbolsAndFrequencies(ProbabilityData[] probabilityTable) {

            for (int i = 0; i < probabilityTable.Length; i++) {
                if (probabilityTable[i] != null) {
                    byte[] frequency = BitConverter.GetBytes(probabilityTable[i].frequency);
                    frequency.CopyTo(result, ((i * 4) + 1));
                }
            }
        }

        public void WriteBits(bool bit, int counter) {

            for (int i = 0; i < counter; i++) {
                WriteSingleBit(bit);
            }
        }

        public void WriteSingleBit(bool bit) {

            byte currentByte = result[byteLocation];
            byte mask = 0;

            if (bit) {
                mask = (byte)(1 << bitLocation);
                currentByte |= mask;
                --bitLocation;

                result[byteLocation] = currentByte;
            } else {
                --bitLocation;
            }
            CheckBitLocation();

            //Console.Write("byte " + byteLocation + ": ");
            //Console.WriteLine(Convert.ToString(currentByte, 2).PadLeft(8, '0'));
        }

        private void CheckBitLocation() {

            if (bitLocation < 0) {
                bitLocation = 7;
                ++byteLocation;
            }
        }

        public byte[] ReturnResult() {

            return result;
        }

        public void PrintResult() {

            for (int i = 1025; i <= (byteLocation); i++) {
                byte currentByte = result[i];
                if (i == (byteLocation)) {
                    string temp = Convert.ToString(currentByte, 2).PadLeft(8, '0');
                    temp = temp.Substring(0, (7 - bitLocation));
                    Console.Write(temp);
                } else {
                    Console.Write(Convert.ToString(currentByte, 2).PadLeft(8, '0'));
                }
                Console.Write(" ");
            }
            Console.WriteLine("");
        }


    }
}
