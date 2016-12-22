using Redux;

namespace Reduxity.Tests
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
