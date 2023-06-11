using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp
{
    public partial class ADBClient
    {
        /// <summary>
        /// Excute a command with blocking and output.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="maxWaitTime">unit: milliseconds</param>
        public CommandExcuteResult ExeCommand(string cmd, int maxWaitTime = -1)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "adb.exe",
                Arguments = cmd,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                StandardOutputEncoding = Encoding.GetEncoding(Util.vars.defaultCodePage),
                StandardErrorEncoding = Encoding.GetEncoding(Util.vars.defaultCodePage)
            };

            Process process = new Process()
            {
                EnableRaisingEvents = true,
                StartInfo = startInfo,
            };

            process.Start();

            if (maxWaitTime < 0)
            {
                maxWaitTime = int.MaxValue;
            }

            if (process.WaitForExit(maxWaitTime))
            {
                string output = process.StandardOutput.ReadToEnd();
                return new CommandExcuteResult(output);
            }
            else
            {
                process.Kill();
                return new CommandExcuteResult(new CommandExcuteTimeoutException());
            }
        }
    }
}
