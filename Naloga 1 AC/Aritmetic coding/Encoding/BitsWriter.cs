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

        public BitsWriter(int arrayLength, int encodingBits) {      // Za enkodiranje

            int resultLenght = arrayLength + (1024 + 1);
            result = new byte[resultLenght];
            byteLocation = 1025;
            bitLocation = 7;

            InsertEncodingBitsValue(encodingBits);
        }

        private void InsertEncodingBitsValue(int encodingBits) {

            switch (encodingBits) {
                case 8:
                    result[0] = 0;
                    break;
                case 16:
                    result[0] = 1;
                    break;
                case 32:
                    result[0] = 2;
                    break;
                case 64:
                    result[0] = 3;
                    break;
            }          
        }

        public void InsertSimbolsAndFrequencies(ulong[] simbolFrequencies) {

            for (int i = 0; i < simbolFrequencies.Length; i++) {
                if (simbolFrequencies[i] != 0) {
                    byte[] frequency = BitConverter.GetBytes(simbolFrequencies[i]);
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

    }
}
