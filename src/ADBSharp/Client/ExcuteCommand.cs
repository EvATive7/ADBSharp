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
#pragma warning disable IDE1006
        private string _execPath => this.ExeFilePath + " ";
        private const int _forceGCTime = 10;
        private int _execTimes_ = 0;
        private int _execTimes
        {
            get
            {
                return _execTimes_;
            }
            set
            {
                if (value >= _forceGCTime)
                {
                    GC.Collect();
                    _execTimes_ = 0;
                }
                else
                {
                    _execTimes_ = value;
                }
            }
        }
#pragma warning restore IDE1006

        /// <summary>
        /// Excute a command with output.
        /// </summary>
        /// <param name="cmd"></param>
        public async Task<string> ExeCommand(string cmd)
        {
            _execTimes++;

            ProcessStartInfo startInfo = new()
            {
                FileName = _execPath,
                Arguments = cmd,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8 // TODO:
            };

            using Process process = new()
            {
                EnableRaisingEvents = true,
                StartInfo = startInfo,
            };

            Logger.Debug("ADB Executing:" + cmd);

            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            Logger.Debug($"ADB Execute result:\n{output}");

            await process.WaitForExitAsync();
            return output;
        }

        /// <summary>
        /// Excute a command via CLI without output.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public async Task<string> ExeCommandViaCLI(string cmd)
        {
            _execTimes++;
            
            return await Util.WindowsCMD.ExecuteCommand(_execPath + cmd);
            
        }
    }
}
