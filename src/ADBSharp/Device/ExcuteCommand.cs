using ADBSharp.Device;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp
{
    public partial class ADBDevice
    {
        private string GetADBCMDWithSerial()
        {
            return "-s " + Serial + " ";
        }

        private CommandExcuteResult PreCheck()
        {
            if (disposed)
            {
                return new CommandExcuteResult(new DeviceUnavailableException(new DeviceDisposedException()));
            }
            if (this.Status != "device")
            {
                return new CommandExcuteResult(new DeviceUnavailableException(new DeviceStatusNotSupportOperationException("device", Status)));
            }
            return new CommandExcuteResult("precheck passed.");
        }

        /// <summary>
        /// Excute a command with blocking and output.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="maxWaitTime">unit: milliseconds</param>
        public CommandExcuteResult ExeCommand(string cmd, int maxWaitTime = -1)
        {
            var rt = PreCheck();
            return rt.Success ? this.ADBClient.ExeCommand(GetADBCMDWithSerial() + cmd, maxWaitTime) : rt;
        }

        /// <summary>
        /// Excute a command via CLI with blocking and without output.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public CommandExcuteResult ExeCommandViaCLI(string cmd)
        {
            var rt = PreCheck();
            return rt.Success ? this.ADBClient.ExeCommandViaCLI(GetADBCMDWithSerial() + cmd) : rt;
        }

        /// <summary>
        /// Excute a command via CLI without blocking and output.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public CommandExcuteResult ExeCommandViaCLIAsync(string cmd)
        {
            var rt = PreCheck();
            return rt.Success ? ADBClient.ExeCommandViaCLIAsync(GetADBCMDWithSerial() + cmd) : rt;
        }
    }
}
