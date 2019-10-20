using System;

namespace LpbSerialDotnet.Protocol
{
    enum Address : byte
    {
        HEIZ = 0x00,
        EM1 = 0x03,
        EM2 = 0x04,
        RGT1 = 0x06,
        RGT2 = 0x07,
        CNTR = 0x08,
        DISP = 0x0A,
        SRVC = 0x0B,
        OZW = 0x31,
        FE = 0x32,
        LAN = 0x42,
        ALL = 0x7F,
    }
}
