using System;
using System.Text;
using Utils.Info;
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

            // ==================================================
            Console.WriteLine("Finishe Test (" + (DateTime.Now - initDate) + ")");
        }
    }
}
