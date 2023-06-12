using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp
{
    public partial class ADBClient
    {
        readonly DirectoryNotFoundException directoryNotFoundException = new("Work directory do not exists.");
        readonly FileNotFoundException fileNotFoundException = new("Executable ADB file do not exists.");

        private readonly string WorkDir;
        private readonly string ExeFilePath;

        public ADBClient(string workdir, string exefilepath)
        {
            if (!Directory.Exists(workdir))
            {
                throw directoryNotFoundException;
            }

            if (!File.Exists(exefilepath))
            {
                throw fileNotFoundException;
            }

            WorkDir = workdir;
            ExeFilePath = exefilepath;

            string oldValue = Environment.GetEnvironmentVariable("PATH")!;
            Environment.SetEnvironmentVariable("PATH", oldValue + ";" + WorkDir);

            this.DeviceManager = new DeviceManager(this);
        }
    }
}
