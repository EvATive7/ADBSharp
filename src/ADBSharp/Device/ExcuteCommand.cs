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
        private string _s_id => "-s " + Name + " ";

        /// <summary>
        /// Excute a command with blocking and output.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="maxWaitTime">unit: milliseconds</param>
        public CommandExcuteResult ExeCommand(string cmd, int maxWaitTime = -1)
        {
            return this.ADBClient.ExeCommand(_s_id + cmd, maxWaitTime);
        }

        /// <summary>
        /// Excute a command via CLI with blocking and without output.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public CommandExcuteResult ExeCommandViaCLI(string cmd)
        {
            return this.ADBClient.ExeCommandViaCLI(_s_id + cmd);
        }

        /// <summary>
        /// Excute a command via CLI without blocking and output.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public CommandExcuteResult ExeCommandViaCLIAsync(string cmd)
        {
            return ADBClient.ExeCommandViaCLIAsync(_s_id + cmd);
        }
    }
}
