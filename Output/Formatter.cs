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
            return ByteToHex(addr);
        }

        public static string TelegramTypeToString(byte value)
        {
            if (Enum.IsDefined(typeof(TelegramType), value)) return ((TelegramType)value).ToString();
            return ByteToHex(value);
        }

        public static string CommandToString(uint commandId)
        {
            var bytes = BitConverter.GetBytes(commandId);
            var bytesStr = BitConverter.IsLittleEndian
                ? ByteArrayToHexString(bytes.Reverse().ToArray())
                : ByteArrayToHexString(bytes);
            if (Command.Commands.TryGetValue(commandId, out var command))
            {
                return string.Format("{0} ({1})", bytesStr, command.Desc);
            }
            else
            {
                return bytesStr;
            }
        }

        public static string TelegramToString(Telegram telegram)
        {
            return string.Format("Dst: {0}, Src: {1}, Type: {2}, Command: {3}",
                AddressToString(telegram.Destination),
                AddressToString(telegram.Source),
                TelegramTypeToString(telegram.Type),
                CommandToString(telegram.Command));
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
