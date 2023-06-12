using ADBSharp;

namespace ADBSharpDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ADBClient myClient = new(".\\platform-tools", ".\\platform-tools\\adb.exe");

            myClient.ExeCommandViaCLI("kill-server");
            myClient.ExeCommandViaCLI("start-server");

            myClient.ExeCommand("connect 127.0.0.1:7555");
            myClient.ExeCommand("connect 127.0.0.1:5555");

            myClient.DeviceManager.NewDeviceAdded += (s, e) =>
            {
                Console.WriteLine("new device:" + e.Serial + ",status:" + e.Status);
            };
            myClient.DeviceManager.DeviceStatusChanged += (s, e) =>
            {
                Console.WriteLine("device " + (s as ADBDevice)!.Serial + " new status:" + e);
            };
            myClient.DeviceManager.DeviceDisconnected += (s, e) =>
            {
                Console.WriteLine("device disconnected:" + e.Serial);
            };

            myClient.DeviceManager.AutoController.Start();

            Thread.Sleep(5000);
            myClient.DeviceManager.Devices.ForEach(d =>
            {
                var _ = d.ExeCommand("shell pm list users");
                Console.WriteLine(_.StdOut);
            });
        }
    }
}