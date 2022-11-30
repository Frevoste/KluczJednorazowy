//Mateusz Redzimski 266601

using System;
using System.Collections;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KluczJednorazowy
{
    internal class MainProgram
    {
        static void Main(string[] args)
        {
            switch(args[0])
            {
                case "-p":
                    Parser();
                    break;
                case "-e":
                    Encrypter();
                    break;
                case "-k":
                    Cryptoanalizer();
                    break;
                default:
                    Helper();
                    break;
            }
        }

        static async void Parser()
        {
            Console.WriteLine("Parser");
            string text = System.IO.File.ReadAllText(@"orig.txt");
            string key = System.IO.File.ReadAllText(@"key.txt");
            Console.WriteLine("Długość pliku= {0}", text.Length);
            Console.WriteLine("Długość linijki = {0}",key.Length);
            string[] lines = new string[text.Length / key.Length];
            int j = 0;
            for(int i=0; i<text.Length;i++)
            {
                if(i%10==0 && i!=0)
                {
                    
                    j++;
                }
                lines[j] += text[i];
            }
            await File.WriteAllLinesAsync("plain.txt", lines);
        }

        static async void Encrypter()
        {
            byte[] key = System.IO.File.ReadAllBytes(@"key.txt");

            byte[] bytesArray = System.IO.File.ReadAllBytes(@"plain.txt");
            List<byte> crypted = new List<byte>();
            int count = 0;
            foreach(var b in bytesArray)
            {
                var x = b ^ key[count];
                crypted.Add(Convert.ToByte(x));
            }
            byte[] tmp = crypted.ToArray();
            File.WriteAllBytesAsync(@"crypto.txt", tmp);

        }
        static async void Cryptoanalizer()
        {
            byte[] key = System.IO.File.ReadAllBytes(@"key.txt");

            byte[] bytesArray = System.IO.File.ReadAllBytes(@"crypto.txt");
            List<byte> crypted = new List<byte>();
            int count = 0;
            foreach (var b in bytesArray)
            {
                var x = b ^ key[count];
                crypted.Add(Convert.ToByte(x));
            }
            byte[] tmp = crypted.ToArray();
            File.WriteAllBytesAsync(@"decrypt.txt", tmp);

        }

        static void Helper()
        {
            Console.WriteLine("Helper");
        }
    }

}
// END OF PROGRAM ... Written by Mateusz Redzimski 266601