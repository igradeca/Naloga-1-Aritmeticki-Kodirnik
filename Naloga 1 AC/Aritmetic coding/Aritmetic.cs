using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritmetic_coding {
    public class Aritmetic {

        Encoding encoder;
        Decoding decoder;

        public Aritmetic() {    // encoding

            encoder = new Encoding(64);
        }

        public Aritmetic(byte bitsNumber) {     // decoding

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
