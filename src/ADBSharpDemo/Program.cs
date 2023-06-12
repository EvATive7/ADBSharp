using ADBSharp;

namespace ADBSharpDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ADBClient myClient = new(".\\platform-tools", ".\\platform-tools\\adb.exe");//declare an adb client

            myClient.ExeCommandViaCLI("kill-server");//kill server (adb cmd)
            myClient.ExeCommandViaCLI("start-server");//start server (adb cmd)

            myClient.ExeCommand("connect 127.0.0.1:7555");
            myClient.ExeCommand("connect 127.0.0.1:5555");

            //resiger event handler
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

            //start devices auto scanner
            myClient.DeviceManager.AutoController.Start();

            //test
            Thread.Sleep(5000);
            myClient.DeviceManager.Devices.ForEach(d =>
            {
                var _ = d.ExeCommand("shell pm list users");
                Console.WriteLine(_.StdOut);
            });
        }
    }
}