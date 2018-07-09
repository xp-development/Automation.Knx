using System.Net;
using Automation.Knx.ReceiveParsers;
using Moq;

namespace Automation.Knx.UnitTest._Connection
{
  public class TestBase
  {
    protected UdpClientFake UdpClientMock;
    protected IPEndPoint ReceiveEndPoint;
    protected IPEndPoint SendEndPoint;

    protected Connection GetConnection()
    {
      ReceiveEndPoint = new IPEndPoint(IPAddress.Parse("192.168.178.24"), 3671);
      SendEndPoint = new IPEndPoint(IPAddress.Parse("192.168.178.30"), 3671);

      var receiveParserDispatcherMock = new Mock<IKnxReceiveParserDispatcher>();
      UdpClientMock = new UdpClientFake();
      return new Connection(ReceiveEndPoint, SendEndPoint, receiveParserDispatcherMock.Object, UdpClientMock);
    }
  }
}