using System;
using System.Collections.Concurrent;
using System.Net;
using System.Threading.Tasks;
using Automation.Knx.ReceiveMessages;
using Automation.Knx.ReceiveParsers;
using Automation.Knx.RequestBuilders;

namespace Automation.Knx
{
  public class Connection : IKnxConnection
  {
    private readonly IPEndPoint _receiveEndPoint;
    private readonly IKnxReceiveParserDispatcher _receiveParserDispatcher;
    private readonly IPEndPoint _sendEndPoint;
    private readonly IUdpClient _udpClient;
    private readonly BlockingCollection<byte[]> _sendMessages = new BlockingCollection<byte[]>();
    private byte _communicationChannel;
    private byte _sequenceCounter;

    public Connection(IPEndPoint receiveEndPoint, IPEndPoint sendEndPoint)
      : this(receiveEndPoint, sendEndPoint, new ReceiveParserDispatcher(new IKnxReceiveParser[] { new ConnectResponseParser(), new TunnelResponseParser(), new TunnelRequestParser(), new DisconnectResponseParser() }), new UdpClient(receiveEndPoint))
    {
    }

    public Connection(IPEndPoint receiveEndPoint, IPEndPoint sendEndPoint, IKnxReceiveParserDispatcher receiveParserDispatcher, IUdpClient udpClient)
    {
      _receiveEndPoint = receiveEndPoint;
      _receiveParserDispatcher = receiveParserDispatcher;
      _sendEndPoint = sendEndPoint;
      _udpClient = udpClient;
      ProcessSendMessages();
    }

    public bool IsConnected { get; private set; }

    public event Action<object, ConnectResponse> Connected;
    public event Action<object, DisconnectResponse> Disconnected;

    public void Connect()
    {
      _sendMessages.Add(new ConnectRequestBuilder().Build(_receiveEndPoint));
    }

    public void Disconnect()
    {
      if (!IsConnected)
        return;

      _sendMessages.Add(new DisconnectRequestBuilder().Build(_communicationChannel, _receiveEndPoint));
    }

    public Task SendAsync(IKnxAddress receivingAddress, IKnxData data)
    {
      if (!IsConnected)
        throw new NotConnectedException();

      _sendMessages.Add(new TunnelRequestBuilder().Build(_communicationChannel, _sequenceCounter++, receivingAddress, data));

      return Task.CompletedTask;
    }

    private void ProcessSendMessages()
    {
      Task.Run(async () =>
      {
        while (true)
        {
          var result = await _udpClient.ReceiveAsync();
          var knxResponse = _receiveParserDispatcher.Build(result.Buffer);
          switch (knxResponse)
          {
            case ConnectResponse connectResponse:
              if (connectResponse.Status == 0x00)
              {
                _sequenceCounter = 0;
                _communicationChannel = connectResponse.CommunicationChannel;
                IsConnected = true;
                Connected?.Invoke(this, connectResponse);
              }

              break;
            case TunnelRequest tunnelRequest:
              _sendMessages.Add(new TunnelResponse(0x06, 0x10, 0x0A, 0x04, _communicationChannel, tunnelRequest.SequenceCounter, 0x00).GetBytes());
              break;

            case DisconnectResponse disconnectResponse:
              IsConnected = false;
              _communicationChannel = 0;
              Disconnected?.Invoke(this, disconnectResponse);
              break;
          }
        }
      });

      Task.Run(() =>
      {
        foreach (var sendMessage in _sendMessages.GetConsumingEnumerable())
          _udpClient.SendAsync(sendMessage, _sendEndPoint);
      });
    }
  }
}