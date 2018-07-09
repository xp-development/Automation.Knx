using FluentAssertions;
using Xunit;

namespace Automation.Knx.UnitTest._UniCastAddress
{
  public class FromByteArray
  {
    [Fact]
    public void Usage()
    {
      var bytes = new byte[] {0x11, 0x64};

      var uniCastAddress = UniCastAddress.FromByteArray(bytes);

      uniCastAddress.Area.Should().Be(1);
      uniCastAddress.Line.Should().Be(1);
      uniCastAddress.DeviceAddress.Should().Be(0x64);
    }
  }
}