using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp.Device
{
    public class ADBDevice
    {
        public string Name { get; set; }
        public string Status { get; set; } = "";

        public ADBDevice(string name)
        {
            Name = name;
        }
    }
}
