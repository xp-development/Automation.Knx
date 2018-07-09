using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Automation.Knx.UnitTest._Connection
{
  public class UdpClientFake : IUdpClient
  {
    private readonly Dictionary<byte[], byte[]> _messages = new Dictionary<byte[], byte[]>();
    private byte[] _nextReceiveMessage;
    private readonly AutoResetEvent _resetEvent = new AutoResetEvent(false);

    public Task<UdpReceiveResult> ReceiveAsync()
    {
      _resetEvent.WaitOne();
      return Task.FromResult(new UdpReceiveResult(_nextReceiveMessage, new IPEndPoint(IPAddress.Any, Int32.MaxValue)));
    }

    public void SendAsync(byte[] sendMessage, IPEndPoint sendEndPoint)
    {
      _nextReceiveMessage = _messages[sendMessage];
      _resetEvent.Set();
    }

    public void AddReceiveMessageForSend(byte[] sendMessage, byte[] receiveMessage)
    {
      _messages[sendMessage] = receiveMessage;
    }
  }
}