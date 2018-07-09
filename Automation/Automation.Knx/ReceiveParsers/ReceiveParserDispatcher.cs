using System;
using System.Collections.Generic;
using System.Linq;
using Automation.Knx.ReceiveMessages;

namespace Automation.Knx.ReceiveParsers
{
  public class ReceiveParserDispatcher : IKnxReceiveParserDispatcher
  {
    private readonly IEnumerable<IKnxReceiveParser> _responseParsers;

    public ReceiveParserDispatcher(IEnumerable<IKnxReceiveParser> responseParsers)
    {
      _responseParsers = responseParsers;
    }

    public IKnxReceiveMessage Build(byte[] responseBytes)
    {
      var headerLength = ParseHeaderLength(responseBytes[0]);
      var protocolVersion = ParseProtocolVersion(responseBytes[1]);
      var serviceTypeIdentifier = ParseServiceTypeIdentifier(responseBytes[2], responseBytes[3]);
      var totalLength = ParseTotalLength(responseBytes[4], responseBytes[5]);

      Console.WriteLine($"ServiceType: {serviceTypeIdentifier} {responseBytes[2]:X}-{responseBytes[3]:X}");

      return _responseParsers.SingleOrDefault(x => x.ServiceTypeIdentifier == serviceTypeIdentifier)
        ?.Build(headerLength, protocolVersion, totalLength, responseBytes.Skip(6).ToArray());
    }

    private static ushort ParseTotalLength(byte data, byte data1)
    {
      return BitConverter.ToUInt16(new[] {data1, data}, 0);
    }

    private static ushort ParseServiceTypeIdentifier(byte data, byte data1)
    {
      return BitConverter.ToUInt16(new[] {data1, data}, 0);
    }

    private static byte ParseProtocolVersion(byte data)
    {
      return data;
    }

    private static byte ParseHeaderLength(byte data)
    {
      return data;
    }
  }
}