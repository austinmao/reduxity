using UnityEngine;
using Redux;

namespace Counter {
	public class Action {
    // actions must have a type and may include a payloaMoveon {
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

        public static State Increment(State previousState, Action.Increment action) {
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
