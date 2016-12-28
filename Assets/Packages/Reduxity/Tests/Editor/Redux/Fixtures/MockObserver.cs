using System;
using System.Collections.Generic;
using UniRx;

namespace Reduxity.Tests.Redux
{
    public class MockObserver<T> : IObserver<T>
    {
        public bool IsCompleted { get; private set; }

        public Exception Error { get; private set; }

        private readonly List<T> _values = new List<T>();
        // public IEnumerable<T> Values => _values;

        public IEnumerable<T> Values {
            get {
                return _values;
            }
        }

        public void OnCompleted()
        {
            IsCompleted = true;
        }

        public void OnError(Exception error)
        {
            Error = error;
        }

        public void OnNext(T value)
        {
            _values.Add(value);
        }
    }
}
