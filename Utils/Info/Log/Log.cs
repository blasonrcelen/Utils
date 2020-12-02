using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Utils.Info.Log
{
    public static class Log
    {
        public const string DEFUALT_FILENAME = "log4net.config";

        static Log()
        {
            XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetEntryAssembly()), new FileInfo(DEFUALT_FILENAME));
        }

        public static void SetLogConfigFile(string file)
        {
            XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetEntryAssembly()), new FileInfo(file));
        }

        public static void Debug(object message, [CallerMemberName] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Debug(message);
        }

        public static void Debug(object message, Exception exception, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Debug(message, exception);
        }

        public static void Error(object message, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Error(message);
        }

        public static void Error(object message, Exception exception, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Error(message, exception);
        }

        public static void Fatal(object message, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Fatal(message);
        }

        public static void Fatal(object message, Exception exception, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Fatal(message, exception);
        }

        public static void Info(object message, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Info(message);
        }

        public static void Info(object message, Exception exception, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Info(message, exception);
        }

        public static void Warn(object message, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Debug(message);
        }

        public static void Warn(object message, Exception exception, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Debug(message, exception);
        }
    }
}
