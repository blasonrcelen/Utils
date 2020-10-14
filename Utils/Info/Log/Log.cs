using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Utils.Info.Log
{
    public class Log
    {
        public Log(string fileName)
        {
            XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetEntryAssembly()), new FileInfo(fileName));
        }

        public void Debug(object message, [CallerMemberName] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Debug(message);
        }

        public void Debug(object message, Exception exception, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Debug(message, exception);
        }

        public void Error(object message, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Error(message);
        }

        public void Error(object message, Exception exception, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Error(message, exception);
        }

        public void Fatal(object message, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Fatal(message);
        }

        public void Fatal(object message, Exception exception, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Fatal(message, exception);
        }

        public void Info(object message, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Info(message);
        }

        public void Info(object message, Exception exception, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Info(message, exception);
        }

        public void Warn(object message, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Debug(message);
        }

        public void Warn(object message, Exception exception, [CallerFilePath] string fileName = "")
        {
            LogManager.GetLogger(Assembly.GetEntryAssembly(), fileName).Debug(message, exception);
        }
    }
}
