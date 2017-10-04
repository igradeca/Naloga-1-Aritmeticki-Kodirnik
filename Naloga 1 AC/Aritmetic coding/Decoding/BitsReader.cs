using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritmetic_coding {
    class BitsReader {

        byte[] data;
        int bitLocation;
        int byteLocation;

        ulong dataField;

        public BitsReader(byte [] inputData) {

            data = inputData;
            byteLocation = 1025;
            bitLocation = 0;
        }

        public ulong GetDataField(byte byteValue) {

            dataField = 0;
            for (int i = 0; i < byteValue; i++) {
                dataField <<= 1;
                if (ReadBit()) {
                    dataField += 1;
                }
            }
            return dataField;
        }

        public bool ReadBit() {

            bool bit = false;
            if (byteLocation < data.LongLength) {
                bit = (data[byteLocation] & 0x80) != 0;
                data[byteLocation] <<= 1;

                ++bitLocation;
                CheckBitLocation();
            }

            return bit;
        }

        private void CheckBitLocation() {

            if (bitLocation >= 8) {
                bitLocation = 0;
                ++byteLocation;
            }
        }
    }
}
