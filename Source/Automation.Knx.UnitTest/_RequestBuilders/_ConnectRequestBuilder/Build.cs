using System.Net;
using FluentAssertions;
using Xunit;

namespace Automation.Knx.UnitTest._RequestBuilders._ConnectRequestBuilder
{
  public class Build
  {
    [Fact]
    public void Usage()
    {
      var builder = new ConnectRequestBuilder();

      var bytes = builder.Build(new IPEndPoint(IPAddress.Parse("192.168.22.33"), 7890));

      bytes.Should().BeEquivalentTo(new byte[]
      {
        0x06, 0x10, 0x02, 0x05, 0x00, 0x1a, 0x08, 0x01, 0xc0, 0xa8, 0x16, 0x21, 0x1e, 0xD2, 0x08, 0x01, 0xc0, 0xa8,
        0x16, 0x21, 0x1E, 0xD2, 0x04, 0x04, 0x02, 0x00
      });
    }
  }
}