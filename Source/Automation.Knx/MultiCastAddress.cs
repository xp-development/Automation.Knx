using System;

namespace Automation.Knx
{
  public class MultiCastAddress : IKnxAddress
  {
    public MultiCastAddress(byte mainGroup, byte middleGroup, byte subGroup)
    {
      MainGroup = mainGroup;
      MiddleGroup = middleGroup;
      SubGroup = subGroup;
    }

    public byte MainGroup { get; }
    public byte MiddleGroup { get; }
    public byte SubGroup { get; }

    public byte[] GetBytes()
    {
      return new[] {(byte) ((MainGroup << 3) | MiddleGroup), SubGroup};
    }

    public static MultiCastAddress FromByteArray(byte[] bytes)
    {
      return new MultiCastAddress((byte) (bytes[0] >> 4), (byte) (bytes[0] & 0x0F), bytes[1]);
    }

    public static MultiCastAddress FromString(string address)
    {
      var addressParts = address.Split('/');
      if (addressParts.Length != 3)
        throw new Exception("Invalid address string.");

      return new MultiCastAddress(Convert.ToByte(addressParts[0]), Convert.ToByte(addressParts[1]), Convert.ToByte(addressParts[2]));
    }
  }
}
