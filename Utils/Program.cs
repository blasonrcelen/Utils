using System;

namespace Utils
{
    class Program
    {
        static void Main()
        {
            DateTime initDate = DateTime.Now;
            Console.WriteLine("Begin Test (" + initDate + ")");
            // ==================================================
            // ==================================================
            Console.WriteLine("Finishe Test (" + (DateTime.Now - initDate) + ")");
        }
    }
}
