using System.Collections.Generic;
using Automation.Knx.ReceiveParsers;
using Moq;
using Xunit;

namespace Automation.Knx.UnitTest._ReceiveParsers._ReceiveParserDispatcher
{
  public class Build
  {
    [Fact]
    public void Usage()
    {
      var parserMock = new Mock<IKnxReceiveParser>();
      parserMock.Setup(x => x.ServiceTypeIdentifier).Returns(0x0277);
      var dispatcher = new ReceiveParserDispatcher(new List<IKnxReceiveParser> {parserMock.Object});

      dispatcher.Build(new byte[] {0x06, 0x10, 0x02, 0x77, 0x00, 0x07, 0x01});

      parserMock.Verify(x => x.Build(0x06, 0x10, 0x07, new byte[] {0x01}));
    }
  }
}