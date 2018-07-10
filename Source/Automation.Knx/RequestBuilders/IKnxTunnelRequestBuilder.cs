namespace Automation.Knx.RequestBuilders
{
  public interface IKnxTunnelRequestBuilder
  {
    byte[] Build(byte communicationChannelId, byte sequenceCounter, IKnxAddress receivingAddress, IKnxData data);
  }
}