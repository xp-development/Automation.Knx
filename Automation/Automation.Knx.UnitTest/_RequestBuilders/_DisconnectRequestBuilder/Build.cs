using System.Net;
using FluentAssertions;
using Xunit;

namespace Automation.Knx.UnitTest._RequestBuilders._DisconnectRequestBuilder
{
  public class Build
  {
    [Fact]
    public void Usage()
    {
      var builder = new DisconnectRequestBuilder();

      var bytes = builder.Build(0x34, new IPEndPoint(IPAddress.Parse("192.168.22.33"), 7890));

      bytes.Should().BeEquivalentTo(new byte[]
        {0x06, 0x10, 0x02, 0x09, 0x00, 0x10, 0x34, 0x00, 0x08, 0x01, 0xc0, 0xa8, 0x16, 0x21, 0x1e, 0xD2});
    }
  }
}