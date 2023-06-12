using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSharp
{
    public class DeviceManager
    {
        private readonly ADBClient ADBClient;
        public DeviceManager(ADBClient client)
        {
            ADBClient = client;
        }

        public List<ADBDevice> Devices = new();

        public event EventHandler<ADBDevice>? NewDeviceAdded;
        public event EventHandler<ADBDevice>? DeviceDisconnected;
        public event EventHandler<string>? DeviceStatusChanged;

        private static string _oldScanOutput = "";
        public void ScanDevices()
        {
            string output = ADBClient.ExeCommand("devices").StdOut!;

            if (output != _oldScanOutput)
            {
                _oldScanOutput = output;
                List<Tuple<string, string>> _tempdevicels = new();

                foreach (string str in output.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (str.StartsWith("List")/* || !str.Contains("device")*/)
                    {
                        continue;
                    }

                    string[] parts = str.Split('\t');

                    string device_name = parts[0];
                    string device_status = parts[1];

                    var changedevice = Devices.Find(deviceC => deviceC.Serial == device_name);
                    if (changedevice != null)
                    {
                        if (changedevice.Status != device_status)
                        {
                            // change device status
                            changedevice.Status = device_status;
                            DeviceStatusChanged?.Invoke(changedevice, changedevice.Status);
                        }
                    }
                    else
                    {
                        // add a new device
                        var device = new ADBDevice(device_name, this.ADBClient) { Status = device_status };
                        Devices.Add(device);
                        NewDeviceAdded?.Invoke(device, device);
                    }
                    _tempdevicels.Add(new Tuple<string, string>(device_name, device_status));
                }

                // remove disconnect device
                var dcdev = Devices.FindAll(d => !_tempdevicels.Exists(td => td.Item1 == d.Serial));
                dcdev.ForEach(d =>
                {
                    d.Dispose();
                    Devices.Remove(d);
                    this.DeviceDisconnected?.Invoke(d, d);
                });
            }
        }
    }
}
