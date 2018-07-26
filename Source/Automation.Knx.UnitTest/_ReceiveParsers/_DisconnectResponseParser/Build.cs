using System.Net;
using Automation.Knx.ReceiveParsers;
using FluentAssertions;
using Xunit;

namespace Automation.Knx.UnitTest._ReceiveParsers._DisconnectResponseParser
{
  public class Build
  {
    [Fact]
    public void Usage()
    {
      var parser = new DisconnectResponseParser();

      var response = parser.Build(0x06, 0x10, 0x0008, new byte[] {0x06, 0x00});

      response.HeaderLength.Should().Be(0x06);
      response.ProtocolVersion.Should().Be(0x10);
      response.TotalLength.Should().Be(0x0008);
      response.CommunicationChannel.Should().Be(0x06);
      response.Status.Should().Be(0x00);
    }
  }
}