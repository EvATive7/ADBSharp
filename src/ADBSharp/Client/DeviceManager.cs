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
            AutoController = new DeviceManagerAutoController(this);
        }

        public DeviceManagerAutoController AutoController;
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
                            Logger.Info($"{device_name}:{changedevice.Status}->{device_status}");
                            changedevice.Status = device_status;
                            DeviceStatusChanged?.Invoke(changedevice, changedevice.Status);
                        }
                    }
                    else
                    {
                        // add a new device
                        Logger.Info($"+:{device_name}");
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
                    Logger.Info($"-:{d.Serial}");
                    d.Dispose();
                    Devices.Remove(d);
                    this.DeviceDisconnected?.Invoke(d, d);
                });
            }
        }

        public class DeviceManagerAutoController
        {
            private readonly DeviceManager ParentManager;
            public int FreshInterval = 1000;

            private CancellationTokenSource _cancellationTokenSource;
            private Thread _thread;

            public DeviceManagerAutoController(DeviceManager manager)
            {
                ParentManager = manager;

                _cancellationTokenSource = new CancellationTokenSource();
                _thread = new Thread(ThreadMethod);
            }

            public void Start()
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _thread = new Thread(ThreadMethod);

                _thread.Start();
            }

            public void Stop()
            {
                _cancellationTokenSource.Cancel();
                //_thread.Join();
            }

            private void ThreadMethod()
            {
                CancellationToken cancellationToken = _cancellationTokenSource.Token;

                ParentManager.NewDeviceAdded += OnParentManagerNewDeviceAdded;
                ParentManager.DeviceDisconnected += OnParentManagerDeviceDisconnected;
                ParentManager.DeviceStatusChanged += OnParentManagerDeviceStatusChanged;

                while (!cancellationToken.IsCancellationRequested)
                {
                    ParentManager.ScanDevices();
                    Thread.Sleep(FreshInterval);
                }

                ParentManager.NewDeviceAdded -= OnParentManagerNewDeviceAdded;
                ParentManager.DeviceDisconnected -= OnParentManagerDeviceDisconnected;
                ParentManager.DeviceStatusChanged -= OnParentManagerDeviceStatusChanged;
            }

            private void OnParentManagerDeviceStatusChanged(object? sender, string e)
            {
                
            }

            private void OnParentManagerDeviceDisconnected(object? sender, ADBDevice e)
            {
                
            }

            private void OnParentManagerNewDeviceAdded(object? sender, ADBDevice e)
            {
                
            }
        }
    }
}
