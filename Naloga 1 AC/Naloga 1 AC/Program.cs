using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aritmetic_coding;

namespace Naloga_1_AC {
    class Program {
        static void Main(string[] args) {

            AritmeticMain aritmetic = new AritmeticMain(8);
            string filePath = "historical_data.txt";

            //string author = aritmetic.Author();
            //Console.WriteLine(author);

            //byte[] bytes = ReadFileToBytes(filePath);
            byte[] bytes = Encoding.UTF8.GetBytes("GEMMAG");

            aritmetic.Encode(bytes);

            Console.ReadLine();
        }

        static byte[] ReadFileToBytes(string filePath) {

            byte[] result = File.ReadAllBytes(filePath);
            return result;
        }


    }
}
