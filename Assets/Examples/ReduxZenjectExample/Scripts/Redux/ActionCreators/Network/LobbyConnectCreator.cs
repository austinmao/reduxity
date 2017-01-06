using Redux;
using Redux.Middleware;
using System;

namespace Reduxity.Example.Zenject.LobbyConnectCreator {
    public class Action {
        /// <summary>
        /// Proprties for each type of http request
        /// </summary>
        public class Join : IAction {
            public TypedLobby lobby;
        }
        public class Leave : IAction {}
    }

    public class ActionCreator : IActionCreator {
        /// <summary>
        /// Thunk that connects to Photon Network
        /// </summary>
        /// <returns>ThunkAction for on success or failure handlers</returns>
        public IAction Connect(Action.Join action) {
            // return thunk to store, which will dispatch new actions upon success or failure
            return new ThunkAction<State> ((dispatch, getState) => {
                // dispatch first action to set state to loading
                dispatch(new LobbyConnector.Action.JoinStart {});

                // join default lobby if lobby is not specified
                if (action.lobby == null) {
                    PhotonNetwork.JoinLobby();

                // else join specified lobby
                } else {
                    PhotonNetwork.JoinLobby(action.lobby);
                }
            });
        }

        /// <summary>
        /// Thunk that disconnects from Photon Network
        /// </summary>
        /// <returns>ThunkAction for on success or failure handlers</returns>
        public IAction DisconnectStart(Action.Leave action) {
            // return thunk to store, which will dispatch new actions upon success or failure
            return new ThunkAction<State> ((dispatch, getState) => {
                // dispatch first action to set state to leaving
                dispatch(new LobbyConnector.Action.LeaveStart {});

                // leave PhotonLobby. Callbacks are handled by Photon.
				PhotonNetwork.LeaveLobby();
            });
        }
    }

    [Serializable]
    /// <summary>
    /// Public settings for api loading
    /// </summary>
    public class Settings {

		// #Critical: The first we try to do is to join a potential existing lobby.
		// If there is, good, else, we'll be called back with OnPhotonRandomJoinFailed()
		public bool shouldJoinRandomLobby = true;
    }
}


