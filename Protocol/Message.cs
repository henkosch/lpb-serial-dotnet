using System.IO;

namespace LpbSerialDotnet.Protocol
{
    class Message
    {
        private byte[] telegram;

        public byte MagicByte { get; set; }
        public byte Length { get; set; }
        public byte Destination { get; set; }
        public byte Source { get; set; }
        public byte[] Unknown1 { get; set; }
        public byte Type { get; set; }

        public Message(byte[] telegram)
        {
            this.telegram = telegram;

            using var stream = new MemoryStream(telegram);
            using var reader = new BinaryReader(stream);

            MagicByte = reader.ReadByte();
            Length = reader.ReadByte();
            Destination = reader.ReadByte();
            Source = reader.ReadByte();
            Unknown1 = reader.ReadBytes(4);
            Type = reader.ReadByte();
        }

        public override string ToString()
        {
            return "Dst: " + Output.Formatter.SerialAddr(Destination) +
            ", Src: " + Output.Formatter.SerialAddr(Source) +
            ", Type: " + Type;
        }
    }
}
