using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp
{
    public class DeviceDisposedException : Exception
    {
        public override string Message => "The device is disposed.";
    }
}
