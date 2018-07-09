using Automation.Knx.ReceiveMessages;

namespace Automation.Knx.ReceiveParsers
{
  public interface IKnxReceiveParserDispatcher
  {
    IKnxReceiveMessage Build(byte[] responseBytes);
  }
}