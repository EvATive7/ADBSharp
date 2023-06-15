using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static ADBSharp.Logger;

namespace ADBSharp.Util
{
    public class WindowsCMD
    {
        /// <summary>
        /// Excute a command via CLI with blocking.
        /// </summary>
        /// <param name="cmd"></param>
        public static async Task<string> ExecuteCommand(string cmd)
        {
            ProcessStartInfo startInfo = new()
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
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8
            };

            using Process prss = new()
            {
                EnableRaisingEvents = true,
                StartInfo = startInfo
            };

            Debug("WindowsCMD Executing:" + cmd);

            prss.Start();
            await prss.WaitForExitAsync();
            string output = prss.StandardOutput.ReadToEnd();
            prss.Kill();

            Debug("WindowsCMD Execute result:\n" + output);

            return output;
        }
    }
}
