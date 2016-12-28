using Redux;

namespace Reduxity.Tests.Redux
{
    public class FakeAction<T> : IAction
    {
        public T Value { get; set; }

        public FakeAction(T value)
        {
            Value = value;
        }
    }
}
