using System.Net;
using Automation.Knx.ReceiveMessages;
using FluentAssertions;
using Xunit;

namespace Automation.Knx.UnitTest._ReceiveMessages._HostProtocolAddressInformation
{
  public class GetBytes
  {
    [Fact]
    public void Usage()
    {
      var hostProtocolAddressInformation =
        new HostProtocolAddressInformation(1, new IPEndPoint(IPAddress.Parse("192.168.178.44"), 5678));

      var bytes = hostProtocolAddressInformation.GetBytes();

      bytes.Should().BeEquivalentTo(new byte[] {0x08, 0x01, 0xC0, 0xA8, 0xB2, 0x2c, 0x16, 0x2E});
    }
  }
}