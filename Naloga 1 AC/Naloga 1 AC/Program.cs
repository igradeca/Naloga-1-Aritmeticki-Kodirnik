﻿using System;
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
            
            //string filePath = "historical_data.txt";
            //byte[] bytes = ReadFileToBytes(filePath);
            byte[] bytes = Encoding.UTF8.GetBytes("GEMMA");

            aritmetic.Encode(bytes);

            //BitsWriter writer = new BitsWriter();
            //writer.WriteBits(bytes, 2);


            Console.ReadLine();
        }

        static byte[] ReadFileToBytes(string filePath) {

            byte[] result = File.ReadAllBytes(filePath);
            return result;
        }


    }
}
