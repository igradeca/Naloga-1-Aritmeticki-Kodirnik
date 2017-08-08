using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritmetic_coding {
    public class Decoding {

        EncodingInitializationData decodingInit;
        ProbabilityData[] probabilityTable;

        public Decoding() {

            probabilityTable = new ProbabilityData[256];
        }

        public void InitializeDecoder(byte byteValue) {

            int bitsNumber = 0;
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




    }
}
