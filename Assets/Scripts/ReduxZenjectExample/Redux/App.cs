using Redux;
using Thunk = Redux.Middleware.Thunk;
using Logger = Reduxity.Middleware.Logger;
using Zenject;
using System;

namespace Reduxity.Example.Zenject {

	/// <summary>
    /// Class that initializes the Store and provides a public Store to dispatch actions
	/// and GetState from.
    /// </summary>
	public class App : IInitializable {

		readonly State state_;
		readonly Logger logger_;
		// readonly Settings settings_;

		/* Reducers below */
		readonly CharacterMover.Reducer move_;
		readonly CameraLook.Reducer look_;
		readonly ApiRequestor.Reducer api_;

		/// <summary>
        /// Inject state and reducers
        /// </summary>
        /// <param name="state">Redux State</param>
        /// <param name="move">Character movement reducers</param>
        /// <param name="look">Camera look reducers</param>
        /// <param name="api">Api reducers</param>
		public App(
			State state,
			// Settings settings,
			Logger logger,
			CharacterMover.Reducer move,
			CameraLook.Reducer look,
			ApiRequestor.Reducer api
		) {
			state_ = state;
			logger_ = logger;
			// settings_ = settings;
			move_ = move;
			look_ = look;
			api_ = api;
		}

		public IStore<State> Store { get ; private set; }

		public void Initialize () {
			// Init with middlewares. Order will determine order of execution.
			Middleware<State>[] middlewares = {
				logger_.Middleware,
				Thunk.Middleware
			};

			// generate Store with initial state injected in the constructor
			Store = new Store<State>(CombineReducers, state_, middlewares); 
		}

		// return a new state after respective reducers. note that each reducer returns
		// a nested state, so make sure to associate state objects with the result of the
		// relevant reducer function.
		private State CombineReducers(State previousState, IAction action) {
			// TODO: do not mutate state
			state_.Character = move_.Reduce(previousState.Character, action);
			state_.Camera = look_.Reduce(previousState.Camera, action);
			state_.Api = api_.Reduce(previousState.Api, action);
			return state_;
		}

		// [Serializable]
		// public class Settings {
		// }
	}
}