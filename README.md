# reduxity
React-redux via UniRx + Redux.NET for Unity3D

## Installation

Download or clone this repo. While Reduxity has dependencies on [UniRx](https://github.com/neuecc/UniRx) and [redux.NET](https://github.com/GuillaumeSalles/redux.NET), these are included in this repo.

## Process

1. Set up `State.cs`. Don't forget you need to create a function to initialize state with default values.

```csharp
    public class State {
        public CounterState Counter { get; set; }

        /* default state at app start-up */
        public State initialize() {
            return new State {
                Counter = new CounterState {
                    count = 0
                }
            };
        }
    }
    public class CounterState {
        public int count { get; set; }
    }
```

2. Set up your [Actions](http://redux.js.org/docs/basics/Actions.html) and [Reducers](http://redux.js.org/docs/basics/Reducers.html). Note that Reduxity follows the [Ducks Module Proposal](https://github.com/erikras/ducks-modular-redux) for bundling.

```csharp
public class Action {
    // actions must have a type and may include a payload. The payload will
    // need to be specified in properties of the Action. See the CharacterMover
    // module for an example of this.
    public class Increment: IAction {}
    public class Decrement: IAction {}
}

// reducers handle state changes by taking the previousState, applying an action,
// and then returning a new State without mutating the previousState. In theory,
// this can create a record of states that DevTools can reveal for easy debugging.
public static class Reducer {
    public static State Reduce(State previousState, IAction action) {
        // Debug.Log($"reducing with action: {action}");
        if (action is Action.Increment) {
            // always pass previousState and action cast as the action type
            // this will lead to more easily replicable reducers
            return Increment(previousState, (Action.Increment)action);
        }

        if (action is Action.Decrement) {
            return Decrement(previousState, (Action.Decrement)action);
        }

        return previousState;
    }

    // include previousState and action in the constructor to make it faster and
    // easier to replicate reducers later
    public static State Increment(State previousState, Action.Increment action) {
        // always return a new State in order to note mutate the previousState
        return new State {
            Counter = new CounterState {
                count = previousState.Counter.count + 1
            }
        };
    }

    public static State Decrement(State previousState, Action.Decrement action) {
        return new State {
            Counter = new CounterState {
                count = previousState.Counter.count - 1
            }
        };
    }
}
```

3. Set up the [Store](http://redux.js.org/docs/basics/Store.html):

```csharp
public class App : MonoBehaviour {
    public static IStore<State> Store { get ; private set; }

    private void Awake () {
        // initialize store with default values
        State initialState = new State {}.initialize();

        // generate Store
        Store = new Store<State>(CombineReducers, initialState); 
    }

    // return a new state after respective reducers. note that each reducer returns
    // a nested state, so make sure to associate state objects with the result of the
    // relevant reducer function.
    private State CombineReducers(State previousState, IAction action) {
        return new State {
            Movement = CharacterMover.Reducer.Reduce(previousState, action).Movement,
            Counter = Counter.Reducer.Reduce(previousState, action).Counter
        };
    }
}
```

4. Observe events and dispatch actions to the store:
```csharp
App.Store.Dispatch(new Counter.Action.Increment {});
```

5. Subscribe to state changes. At this point, you can use Selectors to filter for a specific node in the state tree.
```csharp
App.Store.Subscribe(store => {
        Debug.Log($"going to change count to: {store.Counter.count}");
    })
    .AddTo(this);
```

6. Render state changes!


## Acknowledgements

* [redux.NET](https://github.com/GuillaumeSalles/redux.NET)
* [UniRx](https://github.com/neuecc/UniRx)
* [Ducks: Redux Reducer Bundles](https://github.com/erikras/ducks-modular-redux)
* [Redux](http://redux.js.org/)
* [Unidux](https://github.com/mattak/Unidux) for the inspiration!
* [Takuma](https://github.com/mattak) for the help
