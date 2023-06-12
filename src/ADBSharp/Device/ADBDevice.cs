using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp.Device
{
    public class ADBDevice:IDisposable
    {
        private bool disposedValue;

        public string Name { get; set; }
        public string Status { get; set; } = "";

        public ADBDevice(string name)
        {
            Name = name;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Status = "disconnected";
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
