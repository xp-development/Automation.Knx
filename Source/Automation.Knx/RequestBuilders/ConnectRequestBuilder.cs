using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Automation.Knx
{
  public class ConnectRequestBuilder : IKnxConnectRequestBuilder
  {
    public byte[] Build(IPEndPoint source)
    {
      const byte headerLength = 0x06;
      const byte protocolVersion = 0x10;
      byte[] serviceTypeConnectRequest = {0x02, 0x05};

      var sendingBytes = new List<byte> {headerLength, protocolVersion};
      sendingBytes.AddRange(serviceTypeConnectRequest);

      const byte bodyStructureLength = 0x08;
      const byte ipv4 = 0x01;

      sendingBytes.Add(bodyStructureLength);
      sendingBytes.Add(ipv4);
      sendingBytes.AddRange(source.Address.GetAddressBytes());
      sendingBytes.AddRange(BitConverter.GetBytes((ushort) source.Port).Reverse());

      sendingBytes.Add(bodyStructureLength);
      sendingBytes.Add(ipv4);
      sendingBytes.AddRange(source.Address.GetAddressBytes());
      sendingBytes.AddRange(BitConverter.GetBytes((ushort) source.Port).Reverse());

      const byte connectionRequestStructureLength = 0x04;
      const byte connectionTypeTunnelConnection = 0x04;
      const byte connectionTypeTunnelLinkLayer = 0x02;
      sendingBytes.Add(connectionRequestStructureLength);
      sendingBytes.Add(connectionTypeTunnelConnection);
      sendingBytes.Add(connectionTypeTunnelLinkLayer);
      sendingBytes.Add(0x00);

      sendingBytes.InsertRange(4, BitConverter.GetBytes((ushort) (sendingBytes.Count + 2)).Reverse());

      return sendingBytes.ToArray();
    }
  }
}