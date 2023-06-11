using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp.Client
{
    public class ADBClient
    {
        DirectoryNotFoundException directoryNotFoundException = new DirectoryNotFoundException("Work directory do not exists.");
        FileNotFoundException fileNotFoundException = new FileNotFoundException("Executable ADB file do not exists.");

        private string _workdir;
        public string WorkDir
        {
            get
            {
                return _workdir;
            }
            set
            {
                if (!Directory.Exists(value))
                {
                    throw directoryNotFoundException;
                }
                _workdir = value;
            }
        }
        
        private string _exefilepath;
        public string Exefilepath
        {
            get
            {
                return _exefilepath;
            }
            set
            {
                if (!File.Exists(value))
                {
                    throw fileNotFoundException;
                }
                _exefilepath = value;
            }
        }

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

            _workdir = workdir;
            _exefilepath = exefilepath;
        }
    }
}
