using System;
using System.Net;
using System.Threading.Tasks;

namespace Automation.Knx.TestConsole
{
  internal class Program
  {
    public static async Task Main(string[] args)
    {
      Console.WriteLine("Send IP:");
      var sendIp = Console.ReadLine();
      Console.WriteLine("Send port:");
      var sendPort = Convert.ToInt32(Console.ReadLine());
      var connection = new Connection(new IPEndPoint(IPAddress.Any, 45678), new IPEndPoint(IPAddress.Parse(sendIp), sendPort));

      connection.Connected += (sender, response) =>
        Console.WriteLine(
          $"Connected! CommunicationChannel: {response.CommunicationChannel} Status: {response.Status}");
      connection.Disconnected += (sender, response) =>
        Console.WriteLine(
          $"Disconnected! CommunicationChannel: {response.CommunicationChannel} Status: {response.Status}");

      Console.WriteLine("c:\tConnect");
      Console.WriteLine("d:\tDisconnect");
      Console.WriteLine("s:\tSend");
      Console.WriteLine("x:\tExit");

      while (true)
      {
        var key = Console.ReadKey();
        Console.SetCursorPosition(0, Console.CursorTop);
        switch (key.Key)
        {
          case ConsoleKey.C:
            Console.WriteLine("Connect");
            connection.Connect();
            break;
          case ConsoleKey.X:
            Console.WriteLine("Exit");
            if (connection.IsConnected)
            {
              Console.WriteLine("Disconnect");
              connection.Disconnect();
            }
            return;
          case ConsoleKey.S:
            Console.WriteLine("Send");
            await connection.SendAsync(MultiCastAddress.FromString(ReadLine("Address:")), KnxData.FromBoolean(Convert.ToBoolean(ReadLine("Value"))));
            break;
          case ConsoleKey.D:
            Console.WriteLine("Disconnect");
            connection.Disconnect();
            break;
        }
      }
    }

    private static string ReadLine(string message)
    {
      Console.WriteLine();
      Console.WriteLine(message);
      return Console.ReadLine();
    }
  }
}