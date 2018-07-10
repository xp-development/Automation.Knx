using System;
using System.Net;
using System.Threading.Tasks;
using Automation.Knx.ReceiveParsers;
using FluentAssertions;
using Moq;
using Xunit;

namespace Automation.Knx.UnitTest._Connection
{
  public class SendAsync : TestBase
  {
    [Fact]
    public void ShouldThrowExceptionIfNotConnected()
    {

      var connection = GetConnection();

      new Func<Task>(async () => await connection.SendAsync(MultiCastAddress.FromString("0/4/0"), KnxData.FromBoolean(false))).Should().Throw<NotConnectedException>();
    }
  }
}