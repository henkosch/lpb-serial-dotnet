using System;
using System.Linq;
using System.Reactive.Linq;
using LpbSerialDotnet.Input;
using LpbSerialDotnet.Output;
using LpbSerialDotnet.Protocol;

namespace LpbSerialDotnet
{
    static class Program
    {
        static void Main(string[] args)
        {
            var useSerialPort = false;

            var input = useSerialPort 
                ? Serial.ReadByteStream()
                : HexDumpFile.ReadByteStream("dump.txt");

            input
                .ToTelegrams()
                .Select(PrintBytes)
                .Select(ToMessage)
                .Select(PrintMessage)
                .ForEachAsync(NoOp)
                .Wait();
        }
        
        static byte[] PrintBytes(byte[] data)
        {
            Console.WriteLine(Formatter.ByteArrayToHexString(data));
            return data;
        }

        static Message ToMessage(byte[] telegram)
        {
            return new Message(telegram);
        }

        static Message PrintMessage(Message message)
        {
            Console.WriteLine(message);
            return message;
        }

        static void NoOp<T>(T input)
        {
        }
    }
}
