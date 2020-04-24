using System;
using System.Text;
using Utils.Security.Encryption;

namespace Utils
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime initDate = DateTime.Now;
            Console.WriteLine("Begin Test (" + initDate + ")");
            // ==================================================

            AES256 aes = new AES256();
            Console.WriteLine(Convert.ToBase64String(aes.GetKeyIv()));
            String encrypted_data = Convert.ToBase64String(aes.Encrypt(Encoding.UTF8.GetBytes("OLA MUNDO")));
            AES256 aes2 = new AES256(aes.GetKeyIv());
            String original_data = Encoding.UTF8.GetString(aes2.Decrypt(Convert.FromBase64String(encrypted_data)));
            Console.WriteLine(encrypted_data);
            Console.WriteLine(original_data);

            // ==================================================
            Console.WriteLine("Finishe Test (" + (DateTime.Now - initDate) + ")");
        }
    }
}
