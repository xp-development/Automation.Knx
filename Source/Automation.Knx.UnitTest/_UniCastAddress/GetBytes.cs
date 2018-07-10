using FluentAssertions;
using Xunit;

namespace Automation.Knx.UnitTest._UniCastAddress
{
  public class GetBytes
  {
    [Fact]
    public void Usage()
    {
      var uniCastAddress = new UniCastAddress(1, 1, 100);

      var actualBytes = uniCastAddress.GetBytes();

      actualBytes.Should().BeEquivalentTo(new byte[] {0x11, 0x64});
    }
  }
}