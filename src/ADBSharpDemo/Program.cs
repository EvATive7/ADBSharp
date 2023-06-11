using ADBSharp;

namespace ADBSharpDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ADBClient aDBClient = new ADBClient(".\\platform-tools", ".\\platform-tools\\adb.exe");

            var result = aDBClient.ExeCommand("devices");
            Console.WriteLine(result.StdOut);
        }
    }
}