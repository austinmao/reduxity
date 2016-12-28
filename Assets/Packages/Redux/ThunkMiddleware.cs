using System;

// from https://github.com/taiste/redux.NET-thunk
namespace Redux.Thunk
{
    public static class Middleware
    {
        public static Func<Dispatcher, Dispatcher> ThunkMiddleware<TState>(IStore<TState> store)
        {
            return (Dispatcher next) => (IAction action) =>
            {
                var thunkAction = action as ThunkAction<TState>;
                if (thunkAction != null) {
                    thunkAction.Action(store.Dispatch, store.GetState);
                    return thunkAction;
                }
                return next(action);                
            };
        }
    }

    public class ThunkAction<TState>: IAction
    {
        public Action<Dispatcher, Func<TState>> Action { get; }

        public ThunkAction(Action<Dispatcher, Func<TState>> action)
        {
            this.Action = action;
        }
    }
}

