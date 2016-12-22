using Redux;
using UnityEngine;
using UniRx;

namespace Reduxity {
	public class App : MonoBehaviour {
		public static IStore<State> Store { get ; private set; }

		private void Awake () {
			State initialState = new State {}.initialize();

			// Generate Store
			Store = new Store<State>(CombineReducers, initialState); 

			// // Called when the state changes
			// Store.Subscribe (state => {
			// 	Debug.Log($"state is {state}");
			// });
		}

		private State CombineReducers(State previousState, IAction action) {
			// return a new state after respective reducers. note that each reducer returns
			// a nested state, so make sure to associate state objects with the result of the
			// relevant reducer function.
			return new State {
				Movement = CharacterMover.Reducer.Reduce(previousState, action).Movement,
				Counter = Counter.Reducer.Reduce(previousState, action).Counter
			};
		}
	}
}