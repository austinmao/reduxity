using UnityEngine;
using Redux;

namespace Reduxity.Example.Zenject.Counter {
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
}
