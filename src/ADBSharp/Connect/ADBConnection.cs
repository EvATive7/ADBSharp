using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp.Connect
{
    public class ADBConnection
    {
        /// <summary>
        /// host+port.
        /// <br></br>
        /// eg.127.0.0.1:7555
        /// </summary>
        public string Address { get; set; }
        public bool Connected { get; set; }

        private ADBClient ADBClient { get; set; }

        public ADBConnection(ADBClient aDBClient,string address)
        {
            ADBClient = aDBClient;
            Address = address;
        }

        // TODO:
        private void Connect()
        {

        }

        public async Task ChangeConnectStatus()
        {
            
        }
    }
}
