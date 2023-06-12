using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp
{
    public static class Logger
    {
        public delegate void LoggerHandler(string message);

        public static event LoggerHandler? DEBUG;
        public static event LoggerHandler? INFO;
        public static event LoggerHandler? WARN;
        public static event LoggerHandler? ERROR;

        internal static void Debug(string message)
        {
            DEBUG?.Invoke(message);
        }
        internal static void Info(string message)
        {
            INFO?.Invoke(message);
        }
        internal static void Warn(string message)
        {
            WARN?.Invoke(message);
        }
        internal static void Error(string message)
        {
            ERROR?.Invoke(message);
        }
    }
}
