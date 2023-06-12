using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp.Device
{
    public class DeviceStatusNotSupportOperationException : Exception
    {
        private readonly string _need;
        private readonly string _now;
        public DeviceStatusNotSupportOperationException(string need, string now)
        {
            _need = need;
            _now = now;
        }

        public override string Message => 
            "Device status does not support this operation."
            + "("
            + $"Need\"{_need}\""
            + ","
            + $"Actually\"{_now}\""
            + ")";
    }
}
