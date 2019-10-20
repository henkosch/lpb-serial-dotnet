using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reactive.Linq;

namespace LpbSerialDotnet.Input
{
    public static class Serial
    {
        public static IObservable<byte> ReadByteStream()
        {
            return Observable.Using(() => new SerialPort()
            {
                PortName = "/dev/serial0",
                BaudRate = 4800,
                Parity = Parity.Odd,
                DataBits = 8,
                StopBits = StopBits.One,
                Handshake = Handshake.RequestToSend,
                ReadTimeout = 500,
                WriteTimeout = 500
            }, serialPort => {
                serialPort.Open();
                return serialPort
                    .ToByteStream()
                    .Select(InvertByte);
            });
        }

        static byte InvertByte(byte input) => (byte)(input^0xff);

        public static IObservable<byte> ToByteStream(this SerialPort serialPort)
        {
            return Observable.FromEventPattern<
                SerialDataReceivedEventHandler,
                SerialDataReceivedEventArgs>
            (
                handler => serialPort.DataReceived += handler,
                handler => serialPort.DataReceived -= handler
            ).SelectMany(_ =>
            {
                var buffer = new byte[1024];
                var ret = new List<byte>();
                int bytesRead = 0;
                do
                {
                    bytesRead = serialPort.Read(buffer, 0, buffer.Length);
                    ret.AddRange(buffer.Take(bytesRead));
                } while (bytesRead >= buffer.Length);
                return ret;
            });
        }
    }
}
