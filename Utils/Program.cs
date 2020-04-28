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

            Result<bool> result = (Result<bool>)new Result<bool>().AddError("ok");

            // ==================================================
            Console.WriteLine("Finishe Test (" + (DateTime.Now - initDate) + ")");
        }
    }
}
