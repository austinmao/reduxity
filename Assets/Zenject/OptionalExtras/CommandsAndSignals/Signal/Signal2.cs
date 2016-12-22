using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ModestTree;

namespace Zenject
{
    // Two Parameters
    public abstract class Signal<TDerived, TParam1, TParam2> : ISignal
        where TDerived : Signal<TDerived, TParam1, TParam2>
    {
        readonly List<Action<TParam1, TParam2>> _listeners = new List<Action<TParam1, TParam2>>();

        bool _hasDisposed;

        static string MethodToString(Action<TParam1, TParam2> action)
        {
#if UNITY_WSA && ENABLE_DOTNET && !UNITY_EDITOR
            return action.ToString();
#else
            return "{0}.{1}".Fmt(action.Method.DeclaringType.Name(), action.Method.Name);
#endif
        }

        public void Listen(Action<TParam1, TParam2> listener)
        {
            Assert.That(!_listeners.Contains(listener),
                () => "Tried to add method '{0}' to signal '{1}' but it has already been added"
                .Fmt(MethodToString(listener), this.GetType().Name));
            _listeners.Add(listener);
        }

        public void Unlisten(Action<TParam1, TParam2> listener)
        {
            bool success = _listeners.Remove(listener);
            Assert.That(success,
                () => "Tried to remove method '{0}' from signal '{1}' without adding it first"
                .Fmt(MethodToString(listener), this.GetType().Name));
        }

        void IDisposable.Dispose()
        {
            Assert.That(!_hasDisposed, "Tried to dispose signal '{0}' twice", this.GetType().Name);
            _hasDisposed = true;

            // If you don't want to verify that all event handlers have been removed feel free to comment out this assert or remove
            Assert.That(_listeners.IsEmpty(),
                () => "Found {0} methods still added to signal '{1}'.  Methods: {2}"
                .Fmt(_listeners.Count, this.GetType().Name, _listeners.Select(x => MethodToString(x)).Join(", ")));
        }

        public static TDerived operator + (Signal<TDerived, TParam1, TParam2> signal, Action<TParam1, TParam2> listener)
        {
            signal.Listen(listener);
            return (TDerived)signal;
        }

        public static TDerived operator - (Signal<TDerived, TParam1, TParam2> signal, Action<TParam1, TParam2> listener)
        {
            signal.Unlisten(listener);
            return (TDerived)signal;
        }

        public void Fire(TParam1 p1, TParam2 p2)
        {
            // Use ToArray in case they remove in the handler
            foreach (var listener in _listeners.ToArray())
            {
                listener(p1, p2);
            }
        }
    }
}
