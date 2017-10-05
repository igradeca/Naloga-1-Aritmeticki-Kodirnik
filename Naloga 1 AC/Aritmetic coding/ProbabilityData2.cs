using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aritmetic_coding {
    public class ProbabilityData2 {

        public ulong frequency { get; set; }
        public ulong lowerBoundary { get; set; }
        public ulong upperBoundary { get; set; }

        public ProbabilityData2(ulong frequency) {

            this.frequency = frequency;
        }


    }
}
