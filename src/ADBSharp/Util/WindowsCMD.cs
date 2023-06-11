using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp.Util
{
    internal class WindowsCMD
    {
        /// <summary>
        /// Excute a command via CLI with blocking.
        /// </summary>
        /// <param name="cmd"></param>
        public static void ExecuteCommand(string cmd)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = 
                    "cmd /k " 
                    + "\"" + cmd + "\""
                    + "&exit",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                StandardOutputEncoding = Encoding.GetEncoding(vars.defaultCodePage),
                StandardErrorEncoding = Encoding.GetEncoding(vars.defaultCodePage)
            };

            Process prss = new Process()
            {
                EnableRaisingEvents = true,
                StartInfo = startInfo
            };

            Debug.WriteLine("WindowsCMD Executing:" + cmd);

            prss.Start();
            prss.WaitForExit();
        }

        private static Process? CMD;
        public static void ExecuteCommandAsync(string cmd)
        {
            if (CMD == null)
            {
                CMD = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = "cmd",
                    Arguments = "/K prompt $g ",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    StandardOutputEncoding = Encoding.GetEncoding(vars.defaultCodePage),
                    StandardErrorEncoding = Encoding.GetEncoding(vars.defaultCodePage)
                };
                CMD.EnableRaisingEvents = true;
                CMD.StartInfo = startInfo;
                CMD.Start();
            }

            Debug.WriteLine("WindowsCMD Async Executing:" + cmd);
            CMD.StandardInput.WriteLine(cmd);
        }
    }
}
