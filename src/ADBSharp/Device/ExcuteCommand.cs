using ADBSharp.Device;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

        private void PreCheck()
        {
            if (disposed)
            {
                throw new DeviceUnavailableException(new DeviceDisposedException());
            }
            if (this.Status != "device")
            {
                throw new DeviceUnavailableException(new DeviceStatusNotSupportOperationException("device", Status));
            }
        }

        /// <summary>
        /// Excute a command with output.
        /// </summary>
        /// <param name="cmd"></param>
        public async Task<string> ExeCommand(string cmd)
        {
            PreCheck();

            return await this.ADBClient.ExeCommand(GetADBCMDWithSerial() + cmd);
        }

        /// <summary>
        /// Excute a command via CLI without output.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public async Task<string> ExeCommandViaCLI(string cmd)
        {
            PreCheck();

            return await this.ADBClient.ExeCommandViaCLI(GetADBCMDWithSerial() + cmd);
        }
    }
}
