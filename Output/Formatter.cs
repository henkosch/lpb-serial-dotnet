using System;
using System.Linq;
using LpbSerialDotnet.Protocol;

namespace LpbSerialDotnet.Output
{
    public static class Formatter
    {
        public static string SerialAddr(byte addr)
        {
            var value = (byte)(addr & 0x7F);
            if (Enum.IsDefined(typeof(Address), value)) return ((Address)value).ToString();
            else return ByteToHex(addr);
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
