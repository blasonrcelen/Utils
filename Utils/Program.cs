using System;
using System.Text;
using Utils.Security.Hashing;

namespace Utils
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime initDate = DateTime.Now;
            Console.WriteLine("Begin Test (" + initDate + ")");
            // ==================================================
            SHA512Hash ok = new SHA512Hash(Encoding.UTF8.GetBytes("okok"), 20);
            SHA512Hash ok2 = new SHA512Hash(ok.GetHashSalt());
            SHA512Hash ok3 = new SHA512Hash(Encoding.UTF8.GetBytes("okok"), 20, ok2.Salt);
            Console.WriteLine(ok == ok3);
            // ==================================================
            Console.WriteLine("Finishe Test (" + (DateTime.Now - initDate) + ")");
        }
    }
}
