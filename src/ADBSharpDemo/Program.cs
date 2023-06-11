using ADBSharp;

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

            var resultb = aDBClient.ExeCommand("devices");
            Console.WriteLine(resultb.StdOut);

        }
    }
}