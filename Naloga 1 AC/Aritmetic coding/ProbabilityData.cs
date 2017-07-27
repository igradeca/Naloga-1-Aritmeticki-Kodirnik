using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritmetic_coding {
    class ProbabilityData {

        public byte simbol { get; set; }
        public int frequency { get; set; }
        public float probability { get; set; }
        public int lowerBoundary { get; set; }
        public int upperBoundary { get; set; }

        public ProbabilityData(byte simbol, int frequency) {

            this.simbol = simbol;
            this.frequency = frequency;
        }


    }
}
