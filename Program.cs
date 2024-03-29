﻿using System;
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
            var dumpFileName = "dump.txt";

            var inputBytes = useSerialPort
                ? Serial.ReadByteStream().Select(Serial.InvertByte)
                : HexDumpFile.ReadByteStream(dumpFileName);

            inputBytes
                .ToTelegrams()
                .ForEachAsync(PrintTelegram)
                .Wait();
        }

        static void PrintTelegram(Telegram telegram)
        {
            //Console.WriteLine(Formatter.ByteArrayToHexString(telegram.Data));
            var str = Formatter.TelegramToString(telegram);
            if (str != null) Console.WriteLine(str);
        }
    }
}
