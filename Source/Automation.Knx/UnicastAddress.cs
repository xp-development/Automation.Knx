using System;

namespace Automation.Knx
{
  public class UniCastAddress : IKnxAddress
  {
    public UniCastAddress(byte area, byte line, byte deviceAddress)
    {
      Area = area;
      Line = line;
      DeviceAddress = deviceAddress;
    }

    public byte Area { get; }
    public byte Line { get; }
    public byte DeviceAddress { get; }

    public byte[] GetBytes()
    {
      return new[] {(byte) ((Area << 4) | Line), DeviceAddress};
    }

    public static UniCastAddress FromByteArray(byte[] bytes)
    {
      return new UniCastAddress((byte) (bytes[0] >> 4), (byte) (bytes[0] & 0x0F), bytes[1]);
    }

    public static UniCastAddress FromString(string address)
    {
      var addressParts = address.Split('.');
      if (addressParts.Length != 3)
        throw new Exception("Invalid address string.");

      return new UniCastAddress(Convert.ToByte(addressParts[0]), Convert.ToByte(addressParts[1]),
        Convert.ToByte(addressParts[2]));
    }
  }
}
