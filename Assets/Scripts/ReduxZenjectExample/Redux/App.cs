using Redux;
using Zenject;

namespace Reduxity.Example.Zenject {

	/// <summary>
    /// Class that initializes the Store and provides a public Store to dispatch actions
	/// and GetState from.
    /// </summary>
	public class App : IInitializable {

		readonly State state_;

		/* Reducers below */
		readonly CharacterMover.Reducer move_;
		readonly CameraLook.Reducer look_;
		// readonly ApiRequestor.Reducer api_;

		/// <summary>
        /// Inject state and reducers
        /// </summary>
        /// <param name="state">Redux State</param>
        /// <param name="move">Character movement reducers</param>
        /// <param name="look">Camera look reducers</param>
        /// <param name="api">Api reducers</param>
		public App(
			State state,
			CharacterMover.Reducer move,
			CameraLook.Reducer look
			// ApiRequestor.Reducer api
		) {
			state_ = state;
			move_ = move;
			look_ = look;
			// api_ = api;
		}

		public IStore<State> Store { get ; private set; }

		public void Initialize () {
			// generate Store with initial state injected in the constructor
			Store = new Store<State>(CombineReducers, state_, Redux.Thunk.Middleware.ThunkMiddleware); 
		}

		// return a new state after respective reducers. note that each reducer returns
		// a nested state, so make sure to associate state objects with the result of the
		// relevant reducer function.
		private State CombineReducers(State previousState, IAction action) {
			// TODO: do not mutate state
			state_.Character = move_.Reduce(previousState.Character, action);
			state_.Camera = look_.Reduce(previousState.Camera, action);
			// state_.Api = api_.Reduce(previousState.Api, action);
			return state_;
		}
	}
}