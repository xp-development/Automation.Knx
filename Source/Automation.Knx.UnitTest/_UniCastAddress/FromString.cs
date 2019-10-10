using FluentAssertions;
using Xunit;

namespace Automation.Knx.UnitTest._UniCastAddress
{
  public class FromString
  {
    [Fact]
    public void Usage()
    {
      const string address = "1.1.40";

      var uniCastAddress = UniCastAddress.FromString(address);

      uniCastAddress.Area.Should().Be(1);
      uniCastAddress.Line.Should().Be(1);
      uniCastAddress.DeviceAddress.Should().Be(0x28);
    }
  }
}