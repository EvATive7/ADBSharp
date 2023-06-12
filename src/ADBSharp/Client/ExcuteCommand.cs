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
        private string _execPath => this.ExeFilePath + " ";

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
                StandardOutputEncoding = Encoding.GetEncoding(Util.Vars.defaultCodePage),
                StandardErrorEncoding = Encoding.GetEncoding(Util.Vars.defaultCodePage)
            };

            Process process = new Process()
            {
                EnableRaisingEvents = true,
                StartInfo = startInfo,
            };

            Debug.WriteLine("ADB Executing:" + cmd);

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

        /// <summary>
        /// Excute a command via CLI with blocking and without output.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public CommandExcuteResult ExeCommandViaCLI(string cmd)
        {
            try
            {
                Util.WindowsCMD.ExecuteCommand(_execPath + cmd);
            }
            catch (Exception e)
            {
                return new CommandExcuteResult(e);
            }
            return new CommandExcuteResult("");
        }

        /// <summary>
        /// Excute a command via CLI without blocking and output.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public CommandExcuteResult ExeCommandViaCLIAsync(string cmd)
        {
            try
            {
                Util.WindowsCMD.ExecuteCommandAsync(_execPath + cmd);
            }
            catch (Exception e)
            {
                return new CommandExcuteResult(e);
            }
            return new CommandExcuteResult("");
        }
    
        
    }
}
