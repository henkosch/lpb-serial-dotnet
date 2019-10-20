using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;

namespace LpbSerialDotnet.Input
{
    public static class HexDumpFile
    {
        public static IObservable<byte> ReadByteStream(string fileName)
        {
            return ReadLines(fileName).ToByteStream();
        }

        public static IObservable<string> ReadLines(string fileName)
        {
            return Observable.Using(
                () => new StreamReader(fileName),
                reader => Observable.FromAsync(reader.ReadLineAsync)
                    .Repeat()
                    .TakeWhile(line => line != null));
        }

        public static IObservable<byte> ToByteStream(this IObservable<string> lines)
        {
            return Observable.Create<byte>(observable => 
            {
                return lines.Subscribe(line => 
                {
                    foreach (var data in StringToByteArray(line))
                    {
                        observable.OnNext(data);
                    }
                }, () => observable.OnCompleted());
            });
        }

        public static IEnumerable<byte> StringToByteArray(string hex) 
        {
            return Enumerable
                .Range(0, hex.Length / 2)
                .Select(x => Convert.ToByte(hex.Substring(x * 2, 2), 16));
        }
    }
}
