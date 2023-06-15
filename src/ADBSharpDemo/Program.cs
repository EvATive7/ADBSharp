using ADBSharp;
using ADBSharp.Util;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace ADBSharpDemo
{
    internal class Program
    {
        #region Logger
        static int loglevel = 0;
        static void log(string msg, string level)
        {
            Debug.WriteLine(DateTime.Now.ToString("s") + $" [{level}] " + msg);
        }
        #endregion

        static void Main(string[] args)
        {
            MainAsync().Wait();
        }
        static async Task MainAsync()
        {
            ADBClient myClient = new(".\\platform-tools", ".\\platform-tools\\adb.exe");//declare an adb client

            #region resiger log event
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
            #endregion
            #region resiger event handler
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
            #endregion

            await myClient.ExeCommandViaCLI("kill-server");//kill server (adb cmd)
            await myClient.ExeCommandViaCLI("start-server");//start server (adb cmd)

            await myClient.ExeCommand("connect 127.0.0.1:7555");
            await myClient.ExeCommand("connect 127.0.0.1:5555");

            //start devices auto scanner
            myClient.DeviceManager.AutoController.Start();

            //test
            Thread.Sleep(5000);
            myClient.DeviceManager.Devices.ForEach(async d =>
            {
                /*var _ = await d.ExeCommand("shell pm list users");
                Console.WriteLine(_);*/

                var __ = await d.ExeCommandViaCLI("shell pm list users");
                Console.WriteLine(__);
            });

            #region Interactor
            while (true)
            {
                Console.Write(" > ");
                var cmd = Console.ReadLine()!;
                if (cmd.Replace(" ", "") == string.Empty)
                {
                    Console.WriteLine("space command disabled.\n");
                    continue;
                }
                var exerst = myClient.ExeCommand(cmd);
                Console.WriteLine(exerst);
            }
            #endregion
        }
    }
}