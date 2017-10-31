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

        /// <summary>
        /// Encoding: -E ByteRnd_10M.file izhod.ac
        /// Decoding: -D izhod.ac ByteRnd_10M_drugi.file
        /// </summary>

        static void Main(string[] args) {

            Aritmetic aritmetic;
            Stopwatch time;

            byte[] bytes = ReadFileToBytes(args[1]);
            //GEMMA
            //byte[] bytes = System.Text.Encoding.UTF8.GetBytes("ABCCD");

            switch (args[0]) {
                case "-E":
                    aritmetic = new Aritmetic();

                    time = new Stopwatch();
                    time.Start();
                    
                    WriteBytesToFile(aritmetic.Encode(bytes), args[2]);

                    time.Stop();
                    Console.WriteLine("Encoding done. Time: " + time.Elapsed.TotalSeconds);

                    break;
                case "-D":
                    aritmetic = new Aritmetic(bytes[0]);

                    time = new Stopwatch();
                    time.Start();

                    WriteBytesToFile(aritmetic.Decode(bytes), args[2]);

                    time.Stop();
                    Console.WriteLine("Decoding done. Time: " + time.Elapsed.TotalSeconds);

                    break;
                default:
                    Console.WriteLine("Invalid arguments.");
                    break;
            }

            Console.ReadLine();
        }

        static byte[] ReadFileToBytes(string filePath) {

            byte[] result = File.ReadAllBytes(filePath);
            return result;
        }

        static void WriteBytesToFile(byte[] result, string filePath) {

            File.WriteAllBytes(filePath, result);
        }


    }
}
