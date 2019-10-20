using System;
using System.Reactive.Linq;

namespace LpbSerialDotnet.Protocol
{
    public static class ReactiveExtensions
    {
        public static IObservable<byte[]> ToTelegrams(this IObservable<byte> input)
        {
            return Observable.Create<byte[]>(observable =>
            {   
                var state = new Telegram();
                state.OnTelegram += telegram => observable.OnNext(telegram);
                return input.Subscribe(data => state.AddByte(data), () => observable.OnCompleted());
            });
        }
    }
}
