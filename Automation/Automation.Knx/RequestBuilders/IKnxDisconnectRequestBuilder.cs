using System.Net;

namespace Automation.Knx
{
  public interface IKnxDisconnectRequestBuilder
  {
    byte[] Build(byte communicationChannel, IPEndPoint source);
  }
}