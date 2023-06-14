﻿using System;
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
        /// Excute a command with blocking and output.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="maxWaitTime">unit: milliseconds</param>
        public CommandExcuteResult ExeCommand(string cmd, int maxWaitTime = -1)
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
                StandardErrorEncoding = Encoding.UTF8
            };

            using Process process = new()
            {
                EnableRaisingEvents = true,
                StartInfo = startInfo,
            };

            Logger.Debug("ADB Executing:" + cmd);

            process.Start();

            if (maxWaitTime < 0)
            {
                maxWaitTime = int.MaxValue;
            }

            string output = process.StandardOutput.ReadToEnd();
            if (process.WaitForExit(maxWaitTime))
            {
                Logger.Debug("ADB Executing result:\n" +  output);
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
            _execTimes++;

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
            _execTimes++;

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
