using ADBSharp;
using ADBSharp.Client;

namespace ADBSharpDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ADBClient aDBClient = new ADBClient(".\\platform-tools", ".\\platform-tools\\adb.exe");
        }
    }
}