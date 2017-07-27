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
        public ulong E1E2Lower { get; set; }
        public ulong E1E2Upper { get; set; }
        public ulong E3Lower { get; set; }
        public ulong E3Upper { get; set; }
        public int E3_Counter { get; set; }

        public void Add(byte simbol, ulong step, ulong oldLowerBoundary, ulong oldUpperBoundary, ulong newLowerBoundary, ulong newUpperBoundary, 
            ulong E1E2Lower, ulong E1E2Upper, ulong E3Lower, ulong E3Upper, int E3_Counter) {

            this.simbol = simbol;
            this.step = step;
            this.oldLowerBoundary = oldLowerBoundary;
            this.oldUpperBoundary = oldUpperBoundary;
            this.newLowerBoundary = newLowerBoundary;
            this.newUpperBoundary = newUpperBoundary;
            this.E1E2Lower = E1E2Lower;
            this.E1E2Upper = E1E2Upper;
            this.E3Lower = E3Lower;
            this.E3Upper = E3Upper;
            this.E3_Counter = E3_Counter;
        }


    }
}
