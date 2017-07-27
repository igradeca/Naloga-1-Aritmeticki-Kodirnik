using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritmetic_coding {
    class EncodingSimbolData {

        public byte simbol { get; set; }
        public ulong step { get; set; }
        public ulong oldLowerBoundary { get; set; }
        public ulong oldUpperBoundary { get; set; }
        public ulong newLowerBoundary { get; set; }
        public ulong newUpperBoundary { get; set; }
        public ulong E1orE2 { get; set; }
        public ulong E3 { get; set; }
        public int E3_Counter { get; set; }

        public void Add(byte simbol, ulong step, ulong oldLowerBoundary, ulong oldUpperBoundary, ulong newLowerBoundary, ulong newUpperBoundary, ulong E1orE2, ulong E3, int E3_Counter) {

            this.simbol = simbol;
            this.step = step;
            this.oldLowerBoundary = oldLowerBoundary;
            this.oldUpperBoundary = oldUpperBoundary;
            this.newLowerBoundary = newLowerBoundary;
            this.newUpperBoundary = newUpperBoundary;
            this.E1orE2 = E1orE2;
            this.E3 = E3;
            this.E3_Counter = E3_Counter;
        }


    }
}
