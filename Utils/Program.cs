using System;
using System.Text;
using Utils.Security.Encryption;

namespace Utils
{
    class Program
    {
        static void Main()
        {
            string teste = "{ola:\"mundo\"}";
            byte[] testeB = Encoding.UTF8.GetBytes(teste);

            AES256 aes = new AES256();
            Console.WriteLine(Encoding.UTF8.GetString(aes.Decrypt(aes.Encrypt(testeB))));
        }
    }
}
