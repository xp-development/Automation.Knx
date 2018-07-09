using Automation.Knx.ReceiveMessages;

namespace Automation.Knx
{
  public interface IKnxReceiveParser
  {
    ushort ServiceTypeIdentifier { get; }
    IKnxReceiveMessage Build(byte headerLength, byte protocolVersion, ushort totalLength, byte[] responseBytes);
  }
}