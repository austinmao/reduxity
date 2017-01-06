# reduxity

React-redux via UniRx + Redux.NET for Unity3D += Zenject

## Table of Contents

* [Motivations](#motivations)
  + [Why React](#why-react)
  + [Why Redux](#why-redux)
* [Installation](#installation)
* [Structure](#structure)
* [How to Use with Zenject](#how-to-use-with-zenject)
  + [A Few Gotchas](#a-few-gotchas)
* [How to Use without Zenject](#how-to-use-without-zenject)
* [Examples](#examples)
  + [CounterButton](#counterbutton)
  + [PlayerMovement](#playermovement)
  + [PlayerMovementLook](#playermovementlook)
  + [ZenjectPlayerMovementLook](#zenjectplayermovementlook)
  + [Async](#async)
* [Middleware](#middleware)
  + [Thunk](#thunk)
  + [Logger](#logger)
* [Plans](#plans)
* [FAQ](#faq)
  + [Should I include the entire state object in the reducer?](#should-i-include-the-entire-state-object-in-the-reducer-)
  + [Why are you mutating the state?](#why-are-you-mutating-the-state-)
  + [Will there be DevTools that allow Time Travel?](#will-there-be-devtools-that-allow-time-travel-)
  + [Why use Zenject?](#why-use-zenject-)
* [Resources](#resources)
* [Acknowledgements](#acknowledgements)

## Motivations

React-redux is a modern design pattern employed on websites and mobile apps, including Facebook.com. This project seeks to port its principles and practices to Unity3D. To understand how Reactive Programming and Redux can be incorporated into your project, follow these steps:

### Why React

First, read [this well written blog post](https://ornithoptergames.com/reactiverx-in-unity3d-part-1/) on reactive programming in Unity3D using UniRx. This takes a classic character movement example and decouples reading keyboard/mouse inputs -> converting to velocity -> applying movement to a character.

Now, you should be convinced that reactive programming will lead to more efficient and maintanable code when compared with classical, imperative coding.

### Why Redux

Note that there is still coupling between inputs and movement. What if you wanted to move the character regardless of input (e.g., on a timer or because of an event trigger)? You would have to create a reactive situation for both observing the event and handling the movement for each variation.

Enter [Redux](http://redux.js.org/). Instead of components observing events and then rendering changes, they observe said events and then mutate a global state object (via actions and pure functions called "reducers"). Other components who's sole purpose is to render changes will then observe changes to the state and then render.

In the aforementioned example, CharacterController.Move would only care that a Vector3 state change occurreded using Redux. It would not care where that change came from.This can be done by:

1. Observe input from any source
2. Dispatch input to redux store
3. Reducers convert input to Vector3 velocity
4. State is updated with new velocity (!)
5. Renderer observe state.velocity changes
6. Renderer moves character

Now, the CharacterController only knows that state.velocity has changed and responds accordingly. This has a few benefits:

1. You can unit test the observer (step 1), reducers (step 3), and renderers (step 5) separately. Hooray for decoupling! See [Unit Testing](#unit-testing) for more info.
2. You can use any input to dispatch to the redux store. This includes third party assets that are not written reactively. All you need to do is dispatch to the store.
3. You can debug how additions to the state affected your app one state change at a time.

For a standard CounterButton example, take a look at the Example below. Example `_Scenes` also includes the aforementioned CharacterMover and CounterButton examples.


## Installation

Download or clone this repo. While Reduxity has dependencies on [UniRx](https://github.com/neuecc/UniRx) and [redux.NET](https://github.com/GuillaumeSalles/redux.NET), these are included in this repo. You can also use Reduxity with Zenject for Direct Injection.

*Using Zenject is highly recommended and encouraged*. A full tutorial on how to use Zenject is forthcoming since it is a bit heady at first.

## Structure

This project follows Redux conventions a la common React-redux practices. The data flow is uni-directional from:

Observer => (Action Creator => Action || Action) => Reducer => State => Container => Component

In slightly greater detail:

1. An Observer subscribes to an event, then dispatches an Action Creator or Action to the Redux Store
2. An Action Creator (which is used for async actions) can dispatch an Action
3. The Redux Store routes Action to the proper Reducer
4. The Reducer performs calculations using the previous state and new data, then returns a new State
5. A Container subscribes to a state change, then passes data down to components
6. Each component renders its view based on data passed to it

The objects that will interact with GameObjects or Inputs are:

### Observers
Observers dispatch actions to the store. This is a slight deviation from Redux conventions in that Redux projects often have components themselves observe events (like OnClick). However, I have found that separating observers into their own classes promotes further decoupling.

### Components
"Dumb" views that are passed data and then render based on the data. Components do not actually read from the Store or State object. In the web world, this may be a button that says "Increment" or "Processing..." based on current state.

NOTE: you will not always need a script for this if you are using Zenject to bind GameObjects (like UI.Button). These will then be the components if no view logic needs to be applied other than to just render data.

### Containers
"Smart" view controllers that connect to the Redux store, query for data, and then pass this to one or many components, which then render. Thus, you can think of containers as a collection of components where the container is the manager divvying up tasks. In the web world, this may be a form, button, and corresponding response text. In a game, this could be an avatar that would include components for his/her hair, eyes, etc.

Information on Action Creators, Actions, and Reducers can be found at [Redux Docs](http://redux.js.org/docs).

## How to Use with Zenject

We recommend you use Reduxity with Zenject to encourage more modular, easier-to-test code. Once you get the hang of it, it will lead to less code that is easier to reason and less dependent on MonoBehaviours.

***Example for this is in `Reduxity.ZenjectPlayerMovementExamply.unity`***

First, let us set up Zenject. This is boilerplate for every Zenject project.

1. Create MonoInstaller in Project > Create > Zenject > Mono Installer. This is where you will bind classes to the Direct Injection container via [Installers](https://github.com/modesttree/Zenject#installers)
2. Create ScriptableObjectInstaller in Project > Create > Zenject > Scriptable Object Installer. This is where you will have Settings that can be changed during runtime and persist between sessions.
3. Create Zenject `SceneContext` in Hierarchy > Create > Zenject > SceneContext. This is the entryway into your scene.
4. In the SceneContext, click the `+` under Installers and ScriptableObjectInstallers
5. Drag the MonoInstaller to SceneContext > Installers
6. Drag the ScriptableObjectInstaller to SceneContext > ScriptableObjectInstallers

Next, let's set up the State.

1. Create a `State` script. This will house your nested state object and initialize a default state.
2. Create an `IInitializable` Zenject class like `CharacterState : IInitializable`. Specify state properties.
3. In `void Initialize()`, set up the default state for this object.
4. Repeat this for each node of the state tree. Finally, set up the `State` class to include each of the nodes.

Next, set up Actions and Reducers. Actions take input parameters and are dispatched to the Redux store. They feed those parameters as a payload to reducers that will modify a new state object.

Next, set up the App, which will initialize the store with a default state and reducers. Here, you will inject each reducer and the state. Note that the App contains a public `Store`, which will be your method to dispatch Actions.

Finally, create Components that listen to changes in the State.

### A Few Gotchas

[Zenject](https://github.com/modesttree/Zenject) can take some time to decipher. Here are a few gotchas:
* Need a reference to a GameObject? On the GameObject, go to Add Component > ZenjectBindingScript. Then, change the Component > Size to `1` and then drag the GameObject into `Element 0`. Then, inject and use the bound instance without needing to do `GetComponent` or `public GameObject`.
* Make sure you are dragging the actual GameObject you want a reference to. If it is the `CharacterController`, do not drag in the `Transform`.
* You can also bind a GameObject directly to a script by dragging the script into the `Element 0`.
* If you have multiple injected GameObjects of the same type, you should give an `ID` in the `ZenjectBindingScript`. Then, you can reference this injected GameObject by ID via an [Identifier](https://github.com/modesttree/Zenject#identifiers).


## How to Use without Zenject

Note: Direct injection via Zenject is the preferred method for using this library.

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
    // clean up subscribable when game object is destroyed in order to not leak memory
    .AddTo(this);
```

6) Render state changes!

## Examples
### CounterButton
A standard example that demonstrate simple actions without a payload

### PlayerMovement
An example of player movement via PC input observables. This implements a React-redux version of player movement from [this awesome article](https://ornithoptergames.com/reactiverx-in-unity3d-part-1/)

### PlayerMovementLook
An example of player movement with keyboard inputs and camera looking with mouse inputs. This implements a React-redux version of player movement and mouse looking from [the aforementioned article](https://ornithoptergames.com/reactiverx-in-unity3d-part-1/). Specifically, it demonstrates:
* an observer dispatching two actions
* GameObjects (i.e., Transform) in state
* separation of responsibilities of reducers

### ZenjectPlayerMovementLook
All of the above but using Zenject instead of static functions and `new Class()` intializers.

### Async
An async example of using UniRx's WWWObservable with [Redux Thunk](https://github.com/gaearon/redux-thunk) via [redux.NET-thunk](https://github.com/taiste/redux.NET-thunk) is provided.
tl;dr instead of dispatching an Action, you dispatch an [ActionCreator](http://redux.js.org/docs/basics/Actions.html#action-creators) that returns an Action (after the async process is done), which is then dispatched to the store.

## Middleware

### Thunk
A port of [redux.NET-thunk](https://github.com/taiste/redux.NET-thunk) is included for dispatching async actions. This allows you to dispatch an [Action Creator](http://redux.js.org/docs/basics/Actions.html#action-creators) that returns an [Action](http://redux.js.org/docs/basics/Actions.html). Within the Action Creator, we recommend you dispatch an action to store in state that an async process is happening. Then, update state upon success or failure with another dispatched action. See the Async example for reference on how to use this with UniRx.

### Logger
A port of [redux-logger](https://github.com/evgenyrodionov/redux-logger) is included for automatically logging dispatched actions and the current state before applying the action. Currently, this is only tested with Zenject and adjustable within Zenject's [ScriptableObjectInstaller](https://github.com/modesttree/Zenject#scriptable-object-installer) that is adjustable in runtime Settings. Note that using a `verbose` LogLevel setting will result in dumping each action and state object, which could adversely affect performance in development mode.

## Unit Testing
One of the key benefits of redux is making unit testing much easier. Earlier, we stated: "You can unit test the observer (step 1), reducers (step 3), and renderers (step 5) separately. Hooray for decoupling!"

Let's take a look at some code to test an asynchronous web request, which would normally be impossible without a web server.

Given this reducer:

```csharp
public class Action {
    /* Actions for succeeding with an http request */
    public abstract class IQuerySuccess {
        public string text { get; set; }
    }
    public class GetSuccess : IQuerySuccess, IAction {}
}

public class Reducer : IReducer {
    private ApiState RequestSuccess(ApiState state, Action.IQuerySuccess action) {
        state.isLoaded = true;
        state.isLoading = false;
        state.isError = false;
        state.text = action.text;
        state.error = null;
        return state;
    }
}
```

You could write a test to make sure that the state returns:

```csharp
/* NOTE: testing using Zenject DIContainer below makes testing easier, but is not necessary */
[TestFixture]
public class Test : ZenjectUnitTestFixture {

    DiContainer container_;
    private ApiState mockApiState_;
    private Reducer reducer_;

    [SetUp]
    public void Setup() {
        // create private container
        container_ = new DiContainer();
        container_.Bind<Reducer>().AsSingle();

        // set up mock settings
        var mockSettings = new Settings {
        };
        container_.Bind<Settings>().FromInstance(mockSettings);

        // set default state
        mockApiState_ = new ApiState {};
        reducer_ = container_.Resolve<Reducer>();
    }

    [Test]
    public void Should_set_api_state_when_request_succeeds() {
        // arrange
        var mockApiAction = new Action.GetSuccess {
            text = "foo"
        };

        // act
        var result = reducer_.Reduce(mockApiState_, mockApiAction);

        // assert
        Assert.IsTrue(result.isLoaded);
        Assert.IsFalse(result.isLoading);
        Assert.IsNull(result.error);
        Assert.AreEqual(result.text, "foo");
    }
}
```

There, now you have confirmed that dispatching the `GetSuccess()` will result in the desired state.

You can then test the Component responsible for reacting to this state change.


## Plans
- [x] provide multiple reducer example
- [x] provide GameObject in State example
- [ ] clone state so state does not get mutated
- [x] add Zenject example
- [x] add async example
- [ ] write a Zenject tutorial
- [ ] write a Reduxity + Zenject tutorial
- [x] add logger middleware with different levels settings
- [ ] create DevTools to visualize current state
- [ ] create TimeMachineStore to visualize past states

## FAQ
### Should I include the entire state object in the reducer?
While [Redux docs](http://redux.js.org/docs/faq/Reducers.html#how-do-i-share-state-between-two-reducers-do-i-have-to-use-combinereducers) suggest this is an anti-pattern, it also provides situations where this could be desired. Ultimately, it's up to you. Just make sure you must return the entire `State` object in `CombineReducers()` on `App.cs`.

### Why are you mutating the state?
Because I need to figure out how to efficiently deep clone state objects before they hit the reducer. Until then, be careful that you do not mutate the whole state.

### Will there be DevTools that allow Time Travel?
Working on it, but this will likely be integrated with Zenject. If you don't know what this is, check out [Redux DevTools](https://github.com/gaearon/redux-devtools).

### Why use Zenject?
There is plenty of reading material you can find on the benefits of direct injection at the [Zenject Repo](https://github.com/modesttree/Zenject). With that aside, I found that Zenject's separation + initialization logic, runtime serialization of fields, and its easy-to-use Settings Installer make it easier to reason about a more complex Redux project.

The downside is that Direct Injection can take some time to wrap your head around *and* Zenject does not have any good tutorials. Don't worry, this is on the roadmap.


## Resources
Read more about [Redux](http://redux.js.org/)
Read more about [Reactive Programming](https://gist.github.com/staltz/868e7e9bc2a7b8c1f754)


## Acknowledgements

* [redux.NET](https://github.com/GuillaumeSalles/redux.NET)
* [UniRx](https://github.com/neuecc/UniRx)
* [redux.NET thunk](https://github.com/taiste/redux.NET-thunk)
* [Ducks: Redux Reducer Bundles](https://github.com/erikras/ducks-modular-redux)
* [Redux](http://redux.js.org/)
* [Zenject](https://github.com/modesttree/Zenject)
* [Unidux](https://github.com/mattak/Unidux) for the inspiration!
* [Takuma](https://github.com/mattak) for the help
* [Moq for Unity](https://github.com/tenpn/moq-unitypackage/releases) for Zenject's [Automocking](https://github.com/modesttree/Zenject/blob/master/Documentation/AutoMocking.md) compatibility
