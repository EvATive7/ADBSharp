using ADBSharp;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace ADBSharpDemo
{
    internal class Program
    {
        static int loglevel = 0;
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
            if (loglevel <= 0)
                Logger.DEBUG += (msg) =>
                {
                    log(msg, "DEBU");
                };
            if (loglevel <= 1)
                Logger.INFO += (msg) =>
                {
                    log(msg, "INFO");
                };
            if (loglevel <= 2)
                Logger.WARN += (msg) =>
                {
                    log(msg, "WARN");
                };
            if (loglevel <= 3)
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

            while(true)
            {
                Console.Write(" > ");
                var cmd = Console.ReadLine()!;
                if (cmd.Replace(" ","") == string.Empty)
                {
                    Console.WriteLine("space command disabled.\n");
                    continue;
                }
                var exerst = myClient.ExeCommand(cmd);
                Console.WriteLine(exerst.StdOut);
            }
        }
    }
}