using Automation.Knx.ReceiveParsers;
using FluentAssertions;
using Xunit;

namespace Automation.Knx.UnitTest._ReceiveParsers._TunnelResponseParser
{
  public class Build
  {
    [Fact]
    public void Usage()
    {
      var parser = new TunnelResponseParser();

      var response = parser.Build(0x06, 0x10, 0x000A, new byte[] {0x04, 0x01, 0x03, 0x00});

      response.HeaderLength.Should().Be(0x06);
      response.ProtocolVersion.Should().Be(0x10);
      response.TotalLength.Should().Be(0x000A);
      response.StructureLength.Should().Be(0x04);
      response.CommunicationChannel.Should().Be(0x01);
      response.SequenceCounter.Should().Be(0x03);
      response.Status.Should().Be(0x00);
    }
  }
}