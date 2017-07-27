using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritmetic_coding {
    class EncodingInitializationData {

        public int bitsNumber { get; set; }
        public int E3_Counter { get; set; }
        public ulong minBound { get; set; }
        public ulong maxBound { get; set; }
        public ulong secondQuater { get; set; }
        public ulong firstQuater { get; set; }
        public ulong thirdQuarter { get; set; }

        public EncodingInitializationData(int bitsNumber) {

            this.bitsNumber = bitsNumber;
            E3_Counter = 0;
            minBound = 0;
            maxBound = (ulong)Math.Pow(2, (bitsNumber - 1)) - 1;
            secondQuater = (ulong)Math.Floor((decimal)(maxBound + 1) / 2);
            firstQuater = (ulong)Math.Floor((decimal)secondQuater / 2);
            thirdQuarter = (ulong)Math.Floor((decimal)firstQuater * 3);
        }


    }
}
