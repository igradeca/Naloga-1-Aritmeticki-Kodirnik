using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritmetic_coding {
    class EncodingSimbolData {

        public byte simbol { get; set; }
        public int step { get; set; }
        public int oldLowerBoundary { get; set; }
        public int oldUpperBoundary { get; set; }
        public int newLowerBoundary { get; set; }
        public int newUpperBoundary { get; set; }
        public int E1orE2 { get; set; }
        public int E3 { get; set; }
        public int E3_Counter { get; set; }

        public void Add(byte simbol, int step, int oldLowerBoundary, int oldUpperBoundary, int newLowerBoundary, int newUpperBoundary, int E1orE2, int E3, int E3_Counter) {

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
