using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Automation.Knx
{
  public interface IUdpClient
  {
    Task<UdpReceiveResult> ReceiveAsync();
    void SendAsync(byte[] sendMessage, IPEndPoint sendEndPoint);
  }
}