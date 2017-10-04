using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritmetic_coding {
    public class ProbabilityData {

        public byte simbol { get; set; }
        public ulong frequency { get; set; }
        public float probability { get; set; }
        public ulong lowerBoundary { get; set; }
        public ulong upperBoundary { get; set; }

        public ProbabilityData(byte simbol, ulong frequency) {

            this.simbol = simbol;
            this.frequency = frequency;
        }


    }
}
