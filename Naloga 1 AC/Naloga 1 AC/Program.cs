using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aritmetic_coding;
using System.Diagnostics;

namespace Naloga_1_AC {
    class Program {
        static void Main(string[] args) {

            AritmeticMain aritmetic = new AritmeticMain(32);

            string filePath = "proba.mp3";
            byte[] bytes = ReadFileToBytes(filePath);

            //Krasna si bistra hci planin
            //byte[] bytes = Encoding.UTF8.GetBytes("Krasna si bistra hci planin");

            Stopwatch time = new Stopwatch();
            time.Start();

            aritmetic.InitializeWriter(bytes.Length);

            WriteBytesToFile(aritmetic.Encode(bytes));            

            time.Stop();
            Console.WriteLine("Done. Time: " + time.Elapsed.TotalSeconds);

            //BitsWriter writer = new BitsWriter(bytes.Length);
            //writer.WriteBits(bytes, 2);
            //writer.WriteSingleBit(true);

            Console.ReadLine();
        }

        static byte[] ReadFileToBytes(string filePath) {

            byte[] result = File.ReadAllBytes(filePath);
            return result;
        }

        static void WriteBytesToFile(byte[] result) {

            File.WriteAllBytes("izhod.ac", result);
        }


    }
}
