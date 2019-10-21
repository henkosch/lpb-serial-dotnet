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
        public uint CommandId { get; set; }
        public CommandType CommandType { get; set; }
        public float Temperature { get; set; }
        public ushort Crc { get; set; }

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
            CommandId = (uint)IPAddress.NetworkToHostOrder(reader.ReadInt32());

            CommandType = CommandType.FromId(CommandId);
            if (CommandType != null)
            {
                switch (CommandType.ValueType)
                {
                    case ValueType.VT_TEMP:
                        var enable = reader.ReadByte();
                        var value = (ushort)IPAddress.NetworkToHostOrder(reader.ReadInt16());
                        Temperature = value / 64.0f;
                    break;
                    default:
                    break;
                }
            }

            reader.BaseStream.Seek(-2, SeekOrigin.End);
            Crc = reader.ReadUInt16();
        }
    }
}
