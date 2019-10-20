using System;
namespace LpbSerialDotnet.Protocol
{
    public enum TelegramType : byte
    {
        QINF = 0x01,
        INF  = 0x02,
        SET  = 0x03,
        ACK  = 0x04,
        NACK = 0x05,
        QUR  = 0x06,
        ANS  = 0x07,
        ERR  = 0x08,
        QRV  = 0x0F,
        ARV  = 0x10
    } 
}
 