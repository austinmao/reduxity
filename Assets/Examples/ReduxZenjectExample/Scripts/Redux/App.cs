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
		readonly CloudConnector.Reducer cloud_;
		readonly ClientConnector.Reducer client_;
		readonly LobbyConnector.Reducer lobby_;
		readonly RoomConnector.Reducer room_;
		readonly NetworkPlayer.Reducer networkPlayer_;
		readonly NetworkPlayers.Reducer networkPlayers_;

		/// <summary>
        /// Inject state and reducers
        /// </summary>
        /// <param name="state">Redux State</param>
        /// <param name="move">Character movement reducers</param>
        /// <param name="look">Camera look reducers</param>
        /// <param name="api">Api reducers</param>
        /// <param name="cloud">Cloud reducers</param>
        /// <param name="room">Room reducers</param>
		public App(
			State state,
			Settings settings,
			Logger logger,
			CharacterMover.Reducer move,
			CameraLook.Reducer look,
			ApiRequestor.Reducer api,

			CloudConnector.Reducer cloud,
			ClientConnector.Reducer client,
			LobbyConnector.Reducer lobby,
			RoomConnector.Reducer room,
			NetworkPlayer.Reducer networkPlayer,
			NetworkPlayers.Reducer networkPlayers
		) {
			state_ = state;
			logger_ = logger;
			settings_ = settings;
			move_ = move;
			look_ = look;
			api_ = api;
			cloud_ = cloud;
			client_ = client;
			lobby_ = lobby;
			room_ = room;
			networkPlayer_ = networkPlayer;
			networkPlayers_ = networkPlayers;
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

			// network reducers
			state_.Cloud = cloud_.Reduce(previousState.Cloud, action);
			state_.Client = client_.Reduce(previousState.Client, action);
			state_.Lobby = lobby_.Reduce(previousState.Lobby, action);
			state_.Room = room_.Reduce(previousState.Room, action);
			state_.NetworkPlayer = networkPlayer_.Reduce(previousState.NetworkPlayer, action);
			state_.NetworkPlayers = networkPlayers_.Reduce(previousState.NetworkPlayers, action);
			return state_;
		}

		[Serializable]
		public class Settings {
		}
	}
}
