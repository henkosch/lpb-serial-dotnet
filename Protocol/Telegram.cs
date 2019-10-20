using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace LpbSerialDotnet.Protocol
{
    public class Telegram
    {
        public byte[] Data { get; private set; }

        public byte MagicByte { get; set; }
        public byte Length { get; set; }
        public byte Destination { get; set; }
        public byte Source { get; set; }
        public byte[] Unknown1 { get; set; }
        public byte Type { get; set; }
        public uint Command { get; set; }

        public Telegram(IEnumerable<byte> dataBytes)
        {
            Data = dataBytes.ToArray();

            using var stream = new MemoryStream(Data);
            using var reader = new BinaryReader(stream);

            MagicByte = reader.ReadByte();
            Length = reader.ReadByte();
            Destination = reader.ReadByte();
            Source = reader.ReadByte();
            Unknown1 = reader.ReadBytes(4);
            Type = reader.ReadByte();
            Command = (uint)IPAddress.NetworkToHostOrder(reader.ReadInt32());
        }
    }
}
