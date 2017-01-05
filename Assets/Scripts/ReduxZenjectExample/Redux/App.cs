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
		readonly Settings settings_;

		/* Reducers below */
		readonly CharacterMover.Reducer move_;
		readonly CameraLook.Reducer look_;
		readonly ApiRequestor.Reducer api_;
		readonly CloudConnector.Reducer network_;
		readonly RoomConnector.Reducer room_;

		/// <summary>
        /// Inject state and reducers
        /// </summary>
        /// <param name="state">Redux State</param>
        /// <param name="move">Character movement reducers</param>
        /// <param name="look">Camera look reducers</param>
        /// <param name="api">Api reducers</param>
        /// <param name="network">Network reducers</param>
        /// <param name="room">Room reducers</param>
		public App(
			State state,
			Settings settings,
			Logger logger,
			CharacterMover.Reducer move,
			CameraLook.Reducer look,
			ApiRequestor.Reducer api,
			CloudConnector.Reducer network,
			RoomConnector.Reducer room
		) {
			state_ = state;
			logger_ = logger;
			settings_ = settings;
			move_ = move;
			look_ = look;
			api_ = api;
			network_ = network;
			room_ = room;
		}

		public IStore<State> Store { get ; private set; }

		public void Initialize () {
			// Init with middlewares. Order will determine order of execution.
			Middleware<State>[] middlewares = {
				// automatically log depending on LogLevel, which can be adjusted in the inspector
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
			state_.Network = network_.Reduce(previousState.Network, action);
			state_.Room = room_.Reduce(previousState.Room, action);
			return state_;
		}

		[Serializable]
		public class Settings {
		}
	}
}
