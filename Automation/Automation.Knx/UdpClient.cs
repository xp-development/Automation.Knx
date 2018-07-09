using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Automation.Knx
{
  public class UdpClient : IUdpClient
  {
    private readonly System.Net.Sockets.UdpClient _udpClient;

    public UdpClient(IPEndPoint receiveEndPoint)
    {
      _udpClient = new System.Net.Sockets.UdpClient(receiveEndPoint);
    }

    public Task<UdpReceiveResult> ReceiveAsync()
    {
      return _udpClient.ReceiveAsync();
    }

    public void SendAsync(byte[] sendMessage, IPEndPoint sendEndPoint)
    {
      _udpClient.SendAsync(sendMessage, sendMessage.Length, sendEndPoint);
    }
  }
}