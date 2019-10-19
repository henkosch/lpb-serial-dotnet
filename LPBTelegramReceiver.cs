using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace LpbSerialDotnet
{   
    static class LPBTelegramReceiver
    {
        private const byte magicByte = 0x78;

        public static IObservable<byte[]> ToTelegrams(this IObservable<byte> input)
        {
            return Observable.Create<byte[]>(observable =>
            {   
                var state = new LPBTelegramState();
                state.OnTelegram += telegram => observable.OnNext(telegram);
                return input.Subscribe(data => state.AddByte(data));
            });
        }

        class LPBTelegramState
        {
            public enum States
            {
                WaitingForMagicByte,
                WaitingForLength,
                WaitingForRemaining
            }

            private States state;
            
            private int remainingBytes;
            private List<byte> telegram;

            public event Action<byte[]> OnTelegram;

            public LPBTelegramState()
            {
                reset();
            }

            private void reset()
            {
                state = States.WaitingForMagicByte;
                remainingBytes = 0;
                telegram = new List<byte>();
            }

            public void AddByte(byte data)
            {
                switch (state) 
                {
                    case States.WaitingForMagicByte:
                        if (data == magicByte) 
                        {
                            telegram.Add(data);
                            state = States.WaitingForLength;
                        }
                    break;
                    case States.WaitingForLength:
                        remainingBytes = data - 1;
                        telegram.Add(data);
                        state = States.WaitingForRemaining;
                    break;
                    case States.WaitingForRemaining:
                        telegram.Add(data);
                        remainingBytes -= 1;
                        if (remainingBytes <= 0)
                        {
                            OnTelegram?.Invoke(telegram.ToArray());
                            reset();
                        }
                    break;
                }
            }
        }
    }
}