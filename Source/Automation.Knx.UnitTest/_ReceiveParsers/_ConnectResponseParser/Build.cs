using System.Net;
using Automation.Knx.ReceiveParsers;
using FluentAssertions;
using Xunit;

namespace Automation.Knx.UnitTest._ReceiveParsers._ConnectResponseParser
{
  public class Build
  {
    [Fact]
    public void Usage()
    {
      var parser = new ConnectResponseParser();

      var response = parser.Build(0x06, 0x10, 0x0014,
        new byte[] {0x1C, 0x00, 0x08, 0x01, 0xC0, 0xA8, 0xB2, 0x1A, 0x1F, 0x77, 0x04, 0x04, 0x22, 0x67});

      response.HeaderLength.Should().Be(0x06);
      response.ProtocolVersion.Should().Be(0x10);
      response.TotalLength.Should().Be(0x0014);
      response.CommunicationChannel.Should().Be(0x1C);
      response.Status.Should().Be(0x00);
      response.DataEndpoint.StructureLength.Should().Be(0x08);
      response.DataEndpoint.HostProtocolCode.Should().Be(0x01);
      response.DataEndpoint.IpEndPoint.Address.Should().Be(IPAddress.Parse("192.168.178.26"));
      response.DataEndpoint.IpEndPoint.Port.Should().Be(8055);
      response.ConnectionResponseDataBlock.StructureLength.Should().Be(4);
      response.ConnectionResponseDataBlock.ConnectionType.Should().Be(4);
      response.ConnectionResponseDataBlock.KnxAddress.Area.Should().Be(2);
      response.ConnectionResponseDataBlock.KnxAddress.Line.Should().Be(2);
      response.ConnectionResponseDataBlock.KnxAddress.DeviceAddress.Should().Be(0x67);
    }
  }
}