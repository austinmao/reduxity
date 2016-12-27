using Redux;
using UnityEngine;
using UniRx;
using Zenject;

namespace Reduxity.Example.Zenject {
	public class App : IInitializable {

		readonly State state_;

		/* reducers */
		readonly Movement.Reducer move_;
		readonly Look.Reducer look_;

		/// <summary>
        /// Inject state and reducers
        /// </summary>
        /// <param name="state"></param>
        /// <param name="move"></param>
        /// <param name="look"></param>
		public App(
			State state,
			Movement.Reducer move,
			Look.Reducer look
		) {
			state_ = state;
			move_ = move;
			look_ = look;
		}

		// creating store singleton
		public IStore<State> Store { get ; private set; }

		public void Initialize () {
			// generate Store with initial state
			Store = new Store<State>(CombineReducers, state_); 
		}

		// return a new state after respective reducers. note that each reducer returns
		// a nested state, so make sure to associate state objects with the result of the
		// relevant reducer function.
		private State CombineReducers(State previousState, IAction action) {
			// TODO: do not mutate state
			state_.Character = move_.Reduce(previousState.Character, action);
			state_.Camera = look_.Reduce(previousState.Camera, action);
			return state_;
		}
	}
}