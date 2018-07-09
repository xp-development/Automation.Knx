using Automation.Knx.ReceiveMessages;

namespace Automation.Knx.ReceiveParsers
{
  public class TunnelResponseParser : IKnxReceiveParser
  {
    public ushort ServiceTypeIdentifier => 0x0421;

    IKnxReceiveMessage IKnxReceiveParser.Build(byte headerLength, byte protocolVersion, ushort totalLength,
      byte[] responseBytes)
    {
      return Build(headerLength, protocolVersion, totalLength, responseBytes);
    }

    public TunnelResponse Build(byte headerLength, byte protocolVersion, ushort totalLength, byte[] responseBytes)
    {
      var structureLength = responseBytes[0];
      var communicationChannel = responseBytes[1];
      var sequenceCounter = responseBytes[2];
      var status = responseBytes[3];

      return new TunnelResponse(headerLength, protocolVersion, totalLength, structureLength, communicationChannel,
        sequenceCounter, status);
    }
  }
}