using Redux;
using UnityEngine;

namespace Reduxity.Example.Zenject {
	public class App : MonoBehaviour {
		public static IStore<State> Store { get ; private set; }

		private void Awake () {
			// initialize store with default values
			State initialState = new State {};

			// generate Store
			Store = new Store<State>(CombineReducers, initialState); 
		}

		// return a new state after respective reducers. note that each reducer returns
		// a nested state, so make sure to associate state objects with the result of the
		// relevant reducer function.
		private State CombineReducers(State previousState, IAction action) {
			return new State {
				// Movement = Reducer.Reduce(previousState, action).Movement,
				// Counter = Reducer.Reduce(previousState, action).Counter
			};
		}
	}
}