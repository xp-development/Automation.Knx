using System.Net;

namespace Automation.Knx
{
  public interface IKnxConnectRequestBuilder
  {
    byte[] Build(IPEndPoint source);
  }
}