using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSSL;
using System.IO;

namespace Lab_5
{
    class Program
    {
        static void Main(string[] args)
        {
            int mode = (args[0] == "e" ? 0 : 1);
            string key_string = args[1];
            if(key_string.Length > 16 || key_string.Length == 0)
            {
                Console.WriteLine("Key length must be in range [1, 16], not " + args[1].Length);
                Environment.Exit(0);
            }
            byte[] key = Encoding.ASCII.GetBytes(key_string);
            key_string = args[2];
            if (key_string.Length > 16 || key_string.Length == 0)
            {
                Console.WriteLine("Vector length must be in range [1, 16]" + args[2].Length);
                Environment.Exit(0);
            }
            byte[] vector = Encoding.ASCII.GetBytes(key_string);
            string path = @"D:\Универ\Захист\Lab_5\Lab_5\Text\1.txt";
            OpenSSL.Crypto.CipherContext ctx = new OpenSSL.Crypto.CipherContext(OpenSSL.Crypto.Cipher.AES_128_CTR);
            string line;
            using (StreamReader sr = new StreamReader(path))
            {
                line = sr.ReadToEnd();
            }
            string answer = "";
            if (mode == 1) // decryption
            {
                byte[] msg = Convert.FromBase64String(line);
                byte[] decrypted = ctx.Decrypt(msg, key, vector);
                answer = new string(System.Text.Encoding.ASCII.GetChars(decrypted));
            }
            else if (mode == 0) // encryption
            {
                byte[] msg = Encoding.ASCII.GetBytes(line);
                byte[] enc = ctx.Encrypt(msg, key, vector);
                answer = Convert.ToBase64String(enc);
            }
            
            Console.WriteLine(answer);
            Console.ReadKey();

        }
        
        
    }
}
