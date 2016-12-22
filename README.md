# reduxity

React-redux via UniRx + Redux.NET for Unity3D

Redux is a modern design pattern that can help with decoupling Unity projects. To understand how Reactive Programming and Redux can be incorporated into your project, follow these steps:

First, read [this well written blog post](https://ornithoptergames.com/reactiverx-in-unity3d-part-1/) on reactive programming in Unity3D. This already improves on the classical Unity3D approach of reading keyboard inputs -> converting to velocity -> applying movement to a character.

However, note that there is still coupling between inputs and movement. In fact, this reactive approach still passes around `IObservable`s which can make it difficult to reason, test, expand, and even use 3rd party assets that do not use observables.

Using Redux, this example can be abstracted further. CharacterController.Move should only care that a Vector3 was provided to it. It should not care where that Vector3 came from or whether it is an Observable or not. This can be done by:

1. Observe input from any source
2. Dispatch input to redux store
3. Reducers convert input to Vector3 velocity
4. State is updated with new velocity (!)
5. Renderer observe state.velocity changes
6. Renderer moves character

Now, the CharacterController only knows that state.velocity has changed and responds accordingly. This has a few benefits:

1. You can unit test the observer (step 1), reducers (step 3), and renderers (step 5) separately. Hooray for decoupling!
2. You can use any input to dispatch to the redux store. This includes third party assets that are not written reactively. All you need to do is dispatch to the store.
3. You can debug how additions to the state affected your app one state change at a time.

For a standard CounterButton example, take a look at the Example below. Example `_Scenes` also includes the aforementioned CharacterMover and CounterButton examples.


## Installation

Download or clone this repo. While Reduxity has dependencies on [UniRx](https://github.com/neuecc/UniRx) and [redux.NET](https://github.com/GuillaumeSalles/redux.NET), these are included in this repo.


## Process

1) Set up `State.cs`. Don't forget you need to create a function to initialize state with default values.

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

2) Set up your [Actions](http://redux.js.org/docs/basics/Actions.html) and [Reducers](http://redux.js.org/docs/basics/Reducers.html). Note that Reduxity follows the [Ducks Module Proposal](https://github.com/erikras/ducks-modular-redux) for bundling.

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

3) Set up the [Store](http://redux.js.org/docs/basics/Store.html):

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

4) Observe events and dispatch actions to the store:
```csharp
App.Store.Dispatch(new Counter.Action.Increment {});
```

5) Subscribe to state changes. At this point, you can use Selectors to filter for a specific node in the state tree.
```csharp
App.Store.Subscribe(store => {
        Debug.Log($"going to change count to: {store.Counter.count}");
    })
    .AddTo(this);
```

6) Render state changes!


## Resources
Read more about [Redux](http://redux.js.org/)
Read more about [Reactive Programming](https://gist.github.com/staltz/868e7e9bc2a7b8c1f754)


## Acknowledgements

* [redux.NET](https://github.com/GuillaumeSalles/redux.NET)
* [UniRx](https://github.com/neuecc/UniRx)
* [Ducks: Redux Reducer Bundles](https://github.com/erikras/ducks-modular-redux)
* [Redux](http://redux.js.org/)
* [Unidux](https://github.com/mattak/Unidux) for the inspiration!
* [Takuma](https://github.com/mattak) for the help
