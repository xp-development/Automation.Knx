using System;

namespace Automation.Knx
{
  public class KnxData : IKnxData
  {
    private readonly byte[] _bytes;

    private KnxData(byte[] bytes)
    {
      _bytes = bytes;
    }

    public byte[] GetBytes()
    {
      return _bytes;
    }

    public static KnxData FromBoolean(bool value)
    {
      return new KnxData(new[] {Convert.ToByte(value)});
    }
  }
}