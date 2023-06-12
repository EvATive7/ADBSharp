using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp
{
    public partial class ADBDevice:IDisposable
    {
        private bool disposed;

        private readonly ADBClient ADBClient;

        public string Serial { get; set; }
        public string Status { get; set; } = "";

        public ADBDevice(string name,ADBClient client)
        {
            Serial = name;
            ADBClient = client;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Status = "disconnected";
                }
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
