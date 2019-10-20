using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reactive.Linq;

namespace LpbSerialDotnet
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

            using (var stream = new MemoryStream(telegram))
            using (var reader = new BinaryReader(stream))
            {
                MagicByte = reader.ReadByte();
                Length = reader.ReadByte();
                Destination = reader.ReadByte();
                Source = reader.ReadByte();
                Unknown1 = reader.ReadBytes(4);
                Type = reader.ReadByte();
                // TODO: continue
            }
        }

        public override string ToString()
        {
            return "Dst: " + Program.SerialAddr(Destination) + 
            ", Src: " + Program.SerialAddr(Source) +
            ", Type: " + Type;
        }
    }

    enum Address : byte
    {
        HEIZ = 0x00,
        EM1 = 0x03,
        EM2 = 0x04,
        RGT1 = 0x06,
        RGT2 = 0x07,
        CNTR = 0x08,
        DISP = 0x0A,
        SRVC = 0x0B,
        OZW = 0x31,
        FE  = 0x32,
        LAN = 0x42,
        ALL = 0x7F,
    }


    static class Program
    {
        public static string SerialAddr(byte addr)
        {
            var value = (byte)(addr & 0x7F);
            if (Enum.IsDefined(typeof(Address), value)) return ((Address)value).ToString();
            else return ByteToHex(addr);
        }

        static Message ToMessage(byte[] telegram)
        {
            return new Message(telegram);
        }

        static void Main(string[] args)
        {
            var serialPort = new SerialPort()
            {
                PortName = "/dev/serial0",
                BaudRate = 4800,
                Parity = Parity.Odd,
                DataBits = 8,
                StopBits = StopBits.One,
                Handshake = Handshake.RequestToSend,
                ReadTimeout = 500,
                WriteTimeout = 500
            };

            serialPort.Open();

            serialPort
                .ToByteStream()
                .Select(InvertByte)
                .ToTelegrams()
                .Select(PrintBytes)
                .Select(ToMessage)
                .ForEachAsync(PrintMessage)
                .Wait();
        }

        static byte InvertByte(byte input) => (byte)(input^0xff);

        static void PrintMessage(Message message)
        {
            Console.WriteLine(message);
        }

        static byte[] PrintBytes(byte[] data)
        {
            Console.WriteLine(ByteArrayToHexString(data));
            return data;
        }

        static string ByteToHex(byte data) 
        {
            return string.Format("{0:x2}", data);
        }

        static string ByteArrayToHexString(byte[] data) {
            return string.Join("", data.Select(ByteToHex));
        }
    }
}
