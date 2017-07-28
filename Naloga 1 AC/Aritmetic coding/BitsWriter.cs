using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritmetic_coding {
    public class BitsWriter {

        public void WriteBits(byte[] data, int bit) {

            for (int i = 0; i < data.Length; i++) {
                //string binary = Convert.ToString(data[i], 2).PadLeft(8, '0');
                string binary = Convert.ToString(data[i], 2);
                Console.WriteLine(binary);
            }
            

        }

    }
}
