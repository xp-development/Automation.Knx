using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Automation.Knx.ReceiveMessages;

namespace Automation.Knx.ReceiveParsers
{
  public class DisconnectResponseParser : IKnxReceiveParser
  {
    public ushort ServiceTypeIdentifier => 0x020A;

    IKnxReceiveMessage IKnxReceiveParser.Build(byte headerLength, byte protocolVersion, ushort totalLength,
      byte[] responseBytes)
    {
      return Build(headerLength, protocolVersion, totalLength, responseBytes);
    }

    public DisconnectResponse Build(byte headerLength, byte protocolVersion, ushort totalLength, byte[] responseBytes)
    {
      var communicationChannel = responseBytes[0];
      var status = responseBytes[1];

      return new DisconnectResponse(headerLength, protocolVersion, totalLength, communicationChannel, status);
    }
  }
}