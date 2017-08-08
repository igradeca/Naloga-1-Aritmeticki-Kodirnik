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

            string filePath = args[1];
            byte[] bytes = ReadFileToBytes(filePath);
            //Krasna si bistra hci planin
            //GEMMA
            //byte[] bytes = Encoding.UTF8.GetBytes("Krasna si bistra hci planin");

            switch (args[0]) {
                case "-E":
                    Aritmetic_coding.Encoding encode = new Aritmetic_coding.Encoding(32);

                    Stopwatch time = new Stopwatch();
                    time.Start();

                    encode.InitializeWriter(bytes.Length);
                    WriteBytesToFile(encode.Encode(bytes), args[2]);

                    time.Stop();
                    Console.WriteLine("Done. Time: " + time.Elapsed.TotalSeconds);

                    break;
                case "-D":

                    Decoding decode = new Decoding();

                    decode.InitializeDecoder(bytes[0]);

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
