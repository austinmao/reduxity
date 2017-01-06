using Redux;
using UnityEngine;
using UniRx;

namespace Reduxity.Example.Counter {
	public class App {
		public static IStore<State> Store { get ; private set; }

		public void Initialize () {
			// initialize store with default values
			var initialState = new State {}.Initialize();

			// generate Store
			Store = new Store<State>(CombineReducers, initialState); 
		}

		// return a new state after respective reducers. note that each reducer returns
		// a nested state, so make sure to associate state objects with the result of the
		// relevant reducer function.
		private State CombineReducers(State previousState, IAction action) {
			return new State {
				Counter = Reducer.Reduce(previousState, action).Counter
			};
		}
	}
}