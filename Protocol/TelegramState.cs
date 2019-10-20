using System;
using System.Collections.Generic;

namespace LpbSerialDotnet.Protocol
{
    public class TelegramState
    {
        private const byte magicByte = 0x78;

        public enum States
        {
            WaitingForMagicByte,
            WaitingForLength,
            WaitingForRemaining
        }

        private States state;

        private int remainingBytes;
        private List<byte> dataBytes;

        public event Action<Telegram> OnTelegram;

        public TelegramState()
        {
            Reset();
        }

        private void Reset()
        {
            state = States.WaitingForMagicByte;
            remainingBytes = 0;
            dataBytes = new List<byte>();
        }

        public void AddByte(byte data)
        {
            switch (state)
            {
                case States.WaitingForMagicByte:
                    if (data == magicByte)
                    {
                        dataBytes.Add(data);
                        state = States.WaitingForLength;
                    }
                    break;
                case States.WaitingForLength:
                    remainingBytes = data - 1;
                    dataBytes.Add(data);
                    state = States.WaitingForRemaining;
                    break;
                case States.WaitingForRemaining:
                    dataBytes.Add(data);
                    remainingBytes -= 1;
                    if (remainingBytes <= 0)
                    {
                        var telegram = new Telegram(dataBytes);
                        OnTelegram?.Invoke(telegram);
                        Reset();
                    }
                    break;
            }
        }
    }
}