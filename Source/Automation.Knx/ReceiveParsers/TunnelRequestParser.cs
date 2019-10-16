using Automation.Knx.ReceiveMessages;

namespace Automation.Knx.ReceiveParsers
{
  public class TunnelRequestParser : IKnxReceiveParser
  {
    public ushort ServiceTypeIdentifier => 0x0420;

    IKnxReceiveMessage IKnxReceiveParser.Build(byte headerLength, byte protocolVersion, ushort totalLength,
      byte[] responseBytes)
    {
      return Build(headerLength, protocolVersion, totalLength, responseBytes);
    }

    public TunnelRequest Build(byte headerLength, byte protocolVersion, ushort totalLength, byte[] responseBytes)
    {
      var structureLength = responseBytes[0];
      var communicationChannel = responseBytes[1];
      var sequenceCounter = responseBytes[2];
      var messageCode = responseBytes[4];
      var addInformationLength = responseBytes[5];
      var controlField = responseBytes[6];
      var controlField2 = responseBytes[7];
      var npduLength = responseBytes[12];


      return new TunnelRequest(headerLength, protocolVersion, totalLength, structureLength, communicationChannel,
        sequenceCounter, messageCode, addInformationLength, controlField, controlField2,
        UniCastAddress.FromByteArray(new[] {responseBytes[8], responseBytes[9]}),
        MultiCastAddress.FromByteArray(new[] {responseBytes[10], responseBytes[11]}), npduLength,
        new[] {responseBytes[12], responseBytes[13]});
    }
  }

  public class TunnelRequest : IKnxReceiveMessage
  {
    public TunnelRequest(byte headerLength, byte protocolVersion, ushort totalLength, byte structureLength,
      byte communicationChannel, byte sequenceCounter, byte messageCode, byte addInformationLength, byte controlField,
      byte controlField2, UniCastAddress sourceAddress, MultiCastAddress destinationAddress, byte npduLength,
      byte[] data)
    {
      HeaderLength = headerLength;
      ProtocolVersion = protocolVersion;
      TotalLength = totalLength;
      StructureLength = structureLength;
      CommunicationChannel = communicationChannel;
      SequenceCounter = sequenceCounter;
      MessageCode = messageCode;
      AddInformationLength = addInformationLength;
      ControlField = controlField;
      ControlField2 = controlField2;
      SourceAddress = sourceAddress;
      DestinationAddress = destinationAddress;
      NpduLength = npduLength;
      Data = data;
    }

    public byte HeaderLength { get; }
    public byte ProtocolVersion { get; }
    public ushort TotalLength { get; }
    public byte StructureLength { get; }
    public byte CommunicationChannel { get; }
    public byte SequenceCounter { get; }
    public byte MessageCode { get; }
    public byte AddInformationLength { get; }
    public byte ControlField { get; }
    public byte ControlField2 { get; }
    public UniCastAddress SourceAddress { get; }
    public object DestinationAddress { get; }
    public byte NpduLength { get; }
    public byte[] Data { get; }
  }
}
