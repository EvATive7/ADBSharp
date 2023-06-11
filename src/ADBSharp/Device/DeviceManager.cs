using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp.Device
{
    public class DeviceManager
    {
        private ADBClient ADBClient;
        public DeviceManager(ADBClient client)
        {
            ADBClient = client;
        }

        public List<ADBDevice> Devices = new List<ADBDevice>();

        public event EventHandler<ADBDevice>? NewDeviceAdded;
        public event EventHandler<string>? DeviceStatusChanged;

        private static string oldScanOutput = "";
        public void ScanDevices()
        {
            string output = ADBClient.ExeCommand("devices").StdOut!;

            if (output != oldScanOutput)
            {
                oldScanOutput = output;

                foreach (string str in output.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (str.StartsWith("List")/* || !str.Contains("device")*/)
                    {
                        continue;
                    }

                    string[] parts = str.Split('\t');

                    string device_name = parts[0];
                    string device_status = parts[1];

                    // TODO:fix remove disconnect device

                    var changedevice = Devices.Find(deviceC => deviceC.Name == device_name);
                    if (changedevice != null)
                    {
                        if (changedevice.Status != device_status)
                        {
                            changedevice.Status = device_status;
                            DeviceStatusChanged?.Invoke(changedevice,changedevice.Status);
                        }
                    }
                    else
                    {
                        var device = new ADBDevice(device_name) { Status = device_status };
                        Devices.Add(device);
                        NewDeviceAdded?.Invoke(device, device);
                    }
                }
            }
        }
    }
}
