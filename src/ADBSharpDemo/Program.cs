using ADBSharp;
using System.Diagnostics;

namespace ADBSharpDemo
{
    internal class Program
    {
        static void log(string msg, string level)
        {
            Debug.WriteLine(DateTime.Now.ToString("s") + $" [{level}] " + msg);
        }
        static void Main(string[] args)
        {
            ADBClient myClient = new(".\\platform-tools", ".\\platform-tools\\adb.exe");//declare an adb client

            myClient.ExeCommandViaCLI("kill-server");//kill server (adb cmd)
            myClient.ExeCommandViaCLI("start-server");//start server (adb cmd)

            myClient.ExeCommand("connect 127.0.0.1:7555");
            myClient.ExeCommand("connect 127.0.0.1:5555");

            //resiger log event
            Logger.DEBUG += (msg) =>
            {
                log(msg, "DEBU");
            };
            Logger.INFO += (msg) =>
            {
                log(msg, "INFO");
            };
            Logger.WARN += (msg) =>
            {
                log(msg, "WARN");
            };
            Logger.ERROR += (msg) =>
            {
                log(msg, "ERRO");
            };

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