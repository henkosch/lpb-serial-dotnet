using System;
using System.Linq;
using LpbSerialDotnet.Protocol;

namespace LpbSerialDotnet.Output
{
    public static class Formatter
    {
        public static string AddressToString(byte addr)
        {
            var value = (byte)(addr & 0x7F);
            if (Enum.IsDefined(typeof(Address), value)) return ((Address)value).ToString();
            else return ByteToHex(addr);
        }

        public static string TelegramTypeToString(byte value)
        {
            if (Enum.IsDefined(typeof(TelegramType), value)) return ((TelegramType)value).ToString();
            else return ByteToHex(value);
        }

        public static string TelegramToString(Telegram telegram)
        {
            return string.Format("Dst: {0}, Src: {1}, Type: {2}",
                Formatter.AddressToString(telegram.Destination),
                Formatter.AddressToString(telegram.Source),
                Formatter.TelegramTypeToString(telegram.Type));
        }

        public static string ByteToHex(byte data) 
        {
            return string.Format("{0:x2}", data);
        }

        public static string ByteArrayToHexString(byte[] data) {
            return string.Join("", data.Select(ByteToHex));
        }
    }
}
