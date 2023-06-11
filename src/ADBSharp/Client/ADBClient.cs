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
        DirectoryNotFoundException directoryNotFoundException = new DirectoryNotFoundException("Work directory do not exists.");
        FileNotFoundException fileNotFoundException = new FileNotFoundException("Executable ADB file do not exists.");

        private string WorkDir;
        private string ExeFilePath;

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

            this.DeviceManager = new Device.DeviceManager(this);
        }

        ~ADBClient()
        {

        }
    }
}
