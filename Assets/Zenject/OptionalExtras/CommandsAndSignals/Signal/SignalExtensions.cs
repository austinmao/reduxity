using System;

namespace Zenject
{
    public static class SignalExtensions
    {
        public static void BindSignal<TSignal>(this DiContainer container)
            where TSignal : ISignal
        {
            container.Bind(typeof(TSignal), typeof(IDisposable)).To<TSignal>().AsSingle();
            // Execute at the very end to ensure that everyone has unsubscribed before
            // we verify that our listener count is empty
            container.BindDisposableExecutionOrder<TSignal>(int.MinValue);
        }

        public static void BindSignal<TSignal>(this DiContainer container, string identifier)
            where TSignal : ISignal
        {
            container.Bind<TSignal>().WithId(identifier).AsSingle(identifier);
            container.Bind<IDisposable>().To<TSignal>().FromResolve(identifier);
            // Execute at the very end to ensure that everyone has unsubscribed before
            // we verify that our listener count is empty
            container.BindDisposableExecutionOrder<TSignal>(int.MinValue);
        }
    }
}
