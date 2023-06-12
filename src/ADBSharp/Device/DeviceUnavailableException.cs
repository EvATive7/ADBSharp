using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp
{
    public class DeviceUnavailableException : Exception
    {
        public override string Message => "The specified device is not available.";
        public Exception InlineException;

        public DeviceUnavailableException(Exception exception)
        {
            InlineException = exception;
        }
    }
}
