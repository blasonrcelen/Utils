using System;
using Utils.Enums;
using Utils.Validators;

namespace Utils
{
    class Program
    {
        public class teste
        {
            [IsRequired]
            public string Name { get; set; }
        }

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
