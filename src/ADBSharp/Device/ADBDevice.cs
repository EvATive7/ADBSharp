using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp
{
    public partial class ADBDevice:IDisposable
    {
        private bool disposedValue;

        private readonly ADBClient ADBClient;

        public string Name { get; set; }
        public string Status { get; set; } = "";

        public ADBDevice(string name,ADBClient client)
        {
            Name = name;
            ADBClient = client;
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
