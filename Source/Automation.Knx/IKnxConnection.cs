using System;
using System.Threading.Tasks;
using Automation.Knx.ReceiveMessages;

namespace Automation.Knx
{
  public interface IKnxConnection
  {
    bool IsConnected { get; }
    event Action<object, ConnectResponse> Connected;
    event Action<object, DisconnectResponse> Disconnected;

    void Connect();
    void Disconnect();
    Task SendAsync(IKnxAddress receivingAddress, IKnxData data);
  }
}