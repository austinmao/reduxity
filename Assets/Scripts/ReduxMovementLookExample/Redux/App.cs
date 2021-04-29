using Redux;
using UnityEngine;
using UniRx;
using System;
using System.Collections.Generic;

namespace Reduxity.Example.PlayerMovementLook {
	public class App {

		// creating store singleton
		public static IStore<State> Store { get ; private set; }

		public void Initialize () {
			// initialize store with default values
			State initialState = new State {}.Initialize();

			// generate Store
			Store = new Store<State>(
				ReducerCombiner<State>.CombineReducers(
					new Dictionary<string, Func<object, IAction, IState>> {
						{"Character", Movement.Reducer.Reduce},
						{"Camera", Look.Reducer.Reduce}

					}
				),
				initialState
			); 
		}

	}
}
