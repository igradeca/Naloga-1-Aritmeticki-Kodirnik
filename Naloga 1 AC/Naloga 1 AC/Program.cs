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
        /// Encoding: -E proba.mp3 izhod.ac
        /// Decoding: -D izhod.ac muzik.mp3
        /// </summary>

        static void Main(string[] args) {

            Aritmetic aritmetic;
            Stopwatch time;

            byte[] bytes = ReadFileToBytes(args[1]);
            //Krasna si bistra hci planin
            //GEMMA
            //byte[] bytes = System.Text.Encoding.UTF8.GetBytes("Packo je homo");

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
