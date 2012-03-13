using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Ex1HashFicheiro
{
    class Program
    {
        static void Metodo1HighLevelCopyPasted() // http://msdn.microsoft.com/en-us/library/system.security.cryptography.sha256.aspx
        {
            //var fldr = "";
            //var patt = "*";
            //var fname = Directory.EnumerateFiles(fldr, patt, SearchOption.AllDirectories).First();
            Console.WriteLine("Introduza Path do Ficheiro:");
            var fname = Console.ReadLine();
            //String content = File.ReadAllText(fname);

            byte[] data = File.ReadAllBytes(fname); //new byte[DATA_SIZE];
            byte[] result;

            SHA256 shaM = new SHA256Managed();
            result = shaM.ComputeHash(data);

            Console.WriteLine("Ficheiro: {0}", fname);
            Console.WriteLine("Resultado Hash: {0}", result);
        }

        static void Main(string[] args)
        {
            //Metodo1HighLevelCopyPasted();
        }
    }
}
