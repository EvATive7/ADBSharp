using ADBSharp;
using ADBSharp.Device;

namespace ADBSharpDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ADBClient aDBClient = new ADBClient(".\\platform-tools", ".\\platform-tools\\adb.exe");

            aDBClient.ExeCommandViaCLI("kill-server");
            aDBClient.ExeCommandViaCLI("start-server");

            var resulta = aDBClient.ExeCommand("connect 127.0.0.1:7555");
            Console.WriteLine(resulta.StdOut);

            aDBClient.DeviceManager.NewDeviceAdded += (s, e) =>
            {
                Console.WriteLine("new device:" + e.Name + ",status:" + e.Status);
            };
            aDBClient.DeviceManager.DeviceStatusChanged += (s, e) =>
            {
                Console.WriteLine("device " + (s as ADBDevice)!.Name + " new status:" + e);
            };
            aDBClient.DeviceManager.DeviceDisconnected += (s, e) =>
            {
                Console.WriteLine("device disconnected:" + e.Name);
            };

            while (true)
            {
                aDBClient.DeviceManager.ScanDevices();
                Thread.Sleep(1000);
            }
            
        }
    }
}