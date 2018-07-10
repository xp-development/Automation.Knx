using FluentAssertions;
using Xunit;

namespace Automation.Knx.UnitTest._Connection
{
  public class IsConnected : TestBase
  {
    [Fact]
    public void ShouldBeFalseIfNotConnected()
    {
      var connection = GetConnection();

      connection.IsConnected.Should().BeFalse();
    }
  }
}