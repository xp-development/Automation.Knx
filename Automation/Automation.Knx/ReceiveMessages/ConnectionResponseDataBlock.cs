namespace Automation.Knx
{
  public class ConnectionResponseDataBlock
  {
    public ConnectionResponseDataBlock(byte structureLength, byte connectionType, UniCastAddress knxAddress)
    {
      StructureLength = structureLength;
      ConnectionType = connectionType;
      KnxAddress = knxAddress;
    }

    public byte StructureLength { get; }
    public byte ConnectionType { get; }
    public UniCastAddress KnxAddress { get; }
  }
}