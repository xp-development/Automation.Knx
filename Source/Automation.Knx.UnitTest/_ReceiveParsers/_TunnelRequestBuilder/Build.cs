using Automation.Knx.RequestBuilders;
using FluentAssertions;
using Xunit;

namespace Automation.Knx.UnitTest._ReceiveParsers._TunnelRequestBuilder
{
  public class Build
  {
    [Fact]
    public void SetWithBooleanFalse()
    {
      var builder = new TunnelRequestBuilder();

      var bytes = builder.Build(0x1C, 0, new MultiCastAddress(0, 4, 0), KnxData.FromBoolean(false));

      var expected = new byte[]
      {
        0x06, 0x10, 0x04, 0x20, 0x00, 0x15, 0x04, 0x1C, 0x00, 0x00, 0x11, 0x00, 0xAC, 0xF0, 0x00, 0x00, 0x04, 0x00,
        0x01, 0x00, 0x80
      };
      bytes.Length.Should().Be(expected.Length);
      bytes.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void SetWithBooleanTrue()
    {
      var builder = new TunnelRequestBuilder();

      var bytes = builder.Build(0x1C, 0, new MultiCastAddress(0, 4, 0), KnxData.FromBoolean(true));

      var expected = new byte[]
      {
        0x06, 0x10, 0x04, 0x20, 0x00, 0x15, 0x04, 0x1C, 0x00, 0x00, 0x11, 0x00, 0xAC, 0xF0, 0x00, 0x00, 0x04, 0x00,
        0x01, 0x00, 0x81
      };
      bytes.Length.Should().Be(expected.Length);
      bytes.Should().BeEquivalentTo(expected);
    }
  }
}