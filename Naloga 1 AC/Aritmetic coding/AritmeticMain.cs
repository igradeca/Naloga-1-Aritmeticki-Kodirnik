using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritmetic_coding {
    public class AritmeticMain {

        Encoding encoder;
        Decoding decoder;

        public AritmeticMain() {    // encoding

            encoder = new Encoding(8);            
        }

        public AritmeticMain(byte bitsNumber) {     // decoding

            decoder = new Decoding(bitsNumber);
        }

        public string Author() {

            string result = "Ivan" + " " + "Gradecak" + " " + "E5031111";
            return result;
        }

        public byte[] Encode(byte[] data) {
            
            encoder.InitializeWriter(data.Length);
            return encoder.Encode(data);
        }

        public byte[] Decode(byte[] data) {

            return decoder.Decode(data);
        }

    }
}
