using System;
using System.IO.Ports;
using System.Linq;
using System.Reactive.Linq;

namespace LpbSerialDotnet
{
    static class Program
    {
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
                .ForEachAsync(PrintBytes)
                .Wait();
        }

        static byte InvertByte(byte input) => (byte)(input^0xff);

        static void PrintBytes(byte[] data)
        {
            Console.WriteLine(ByteArrayToHexString(data));
        }

        static string ByteArrayToHexString(byte[] data) {
            return string.Join("", data.Select(b => string.Format("{0:x2}", b)));
        }
    }
}
