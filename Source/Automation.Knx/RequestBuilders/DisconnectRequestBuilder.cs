using System.Collections.Generic;
using System.Net;
using Automation.Knx.ReceiveMessages;

namespace Automation.Knx
{
  public class DisconnectRequestBuilder : IKnxDisconnectRequestBuilder
  {
    public byte[] Build(byte communicationChannel, IPEndPoint source)
    {
      const byte headerLength = 0x06;
      const byte protocolVersion = 0x10;
      byte[] serviceTypeConnectRequest = {0x02, 0x09};

      var sendingBytes = new List<byte> {headerLength, protocolVersion};
      sendingBytes.AddRange(serviceTypeConnectRequest);
      sendingBytes.Add(0x00);
      sendingBytes.Add(0x10);
      sendingBytes.Add(communicationChannel);
      sendingBytes.Add(0x00);
      sendingBytes.AddRange(new HostProtocolAddressInformation(0x01, source).GetBytes());

      return sendingBytes.ToArray();
    }
  }
}