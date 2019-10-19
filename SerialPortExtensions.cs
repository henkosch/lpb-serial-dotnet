using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reactive.Linq;

namespace LpbSerialDotnet
{
    static class SerialPortExtensions
    {
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