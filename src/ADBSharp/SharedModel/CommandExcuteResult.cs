using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp
{
    public class CommandExcuteResult
    {
        public bool Success { get; } 

        public Exception? Exception { get; } = null;
        public string? StdOut { get; } = null;

        internal CommandExcuteResult(Exception exception)
        {
            Success = false;
            Exception = exception;
        }
        internal CommandExcuteResult(string stdout)
        {
            Success = true;
            StdOut = stdout;
        }
    }
}
