using System;
using Utils.Info.Log;

namespace TestUtils
{
    class Program
    {
        private static readonly Log log = new Log("log4net.config");

        static void Main()
        {
            // Log some things
            log.Debug("Debug", new Exception("OLA MUNDo"));
            log.Info("Hello logging world!");
            log.Error("Error!");
            log.Warn("Warn!");
        }
    }
}
