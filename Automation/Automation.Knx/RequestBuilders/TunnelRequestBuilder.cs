using System;
using System.Collections.Generic;
using System.Linq;

namespace Automation.Knx.RequestBuilders
{
  public class TunnelRequestBuilder : IKnxTunnelRequestBuilder
  {
    public byte[] Build(byte communicationChannelId, byte sequenceCounter, IKnxAddress receivingAddress, IKnxData data)
    {
      var dataBytes = data.GetBytes();
      var length = GetLength(dataBytes);

      const byte headerLength = 0x06;
      const byte protocolVersion = 0x10;
      byte[] serviceTypeTunnelingRequest = {0x04, 0x20};

      var sendingBytes = new List<byte> {headerLength, protocolVersion};
      sendingBytes.AddRange(serviceTypeTunnelingRequest);

      const byte bodyStructureLength = 0x04;

      sendingBytes.Add(bodyStructureLength);
      sendingBytes.Add(communicationChannelId);
      sendingBytes.Add(sequenceCounter);
      sendingBytes.Add(0x00);

      sendingBytes.Add(0x11);
      sendingBytes.Add(0x00);
      sendingBytes.Add(0xAC);
      sendingBytes.Add(receivingAddress is MultiCastAddress ? (byte) 0xF0 : (byte) 0x50);
      sendingBytes.Add(0x00);
      sendingBytes.Add(0x00);
      sendingBytes.AddRange(receivingAddress.GetBytes());
      sendingBytes.Add((byte) length);
      sendingBytes.Add(0x00);
      sendingBytes.Add(0x80);

      AddSendingData(sendingBytes, dataBytes);

      sendingBytes.InsertRange(4, BitConverter.GetBytes((ushort) (sendingBytes.Count + 2)).Reverse());

      return sendingBytes.ToArray();
    }

    private static void AddSendingData(IList<byte> sendingBytes, IReadOnlyList<byte> dataBytes)
    {
      var lastIndex = sendingBytes.Count - 1;
      if (dataBytes.Count == 1)
      {
        if (dataBytes[0] < 0x3F)
          sendingBytes[lastIndex] = (byte) (sendingBytes[lastIndex] | dataBytes[0]);
        else
          sendingBytes[lastIndex + 1] = dataBytes[0];
      }
      else if (dataBytes.Count > 1)
      {
        if (dataBytes[0] < 0x3F)
        {
          sendingBytes[lastIndex] = (byte) (sendingBytes[lastIndex] | dataBytes[0]);

          for (var i = 1; i < dataBytes.Count; i++)
            sendingBytes[lastIndex + i] = dataBytes[i];
        }
        else
        {
          for (var i = 0; i < dataBytes.Count; i++)
            sendingBytes[lastIndex + 1 + i] = dataBytes[i];
        }
      }
    }

    private static int GetLength(IReadOnlyCollection<byte> dataBytes)
    {
      if (dataBytes.Count == 0)
        return 0;

      if (dataBytes.Count == 1)
        return 1;

      return dataBytes.Count + 1;
    }
  }
}