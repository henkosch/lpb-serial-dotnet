using System;
using System.Reactive.Linq;

namespace LpbSerialDotnet.Protocol
{
    public static class ReactiveExtensions
    {
        public static IObservable<Telegram> ToTelegrams(this IObservable<byte> input)
        {
            return Observable.Create<Telegram>(observable =>
            {
                var state = new TelegramState();
                state.OnTelegram += telegram => observable.OnNext(telegram);
                return input.Subscribe(state.AddByte, observable.OnCompleted);
            });
        }
    }
}
