using Redux;
using Redux.Middleware;
using System;

namespace Reduxity.Example.Zenject.RoomConnectCreator {
    public class Action {
        /// <summary>
        /// Properties for each type of http request
        /// </summary>
        public class Join : RoomConnector.Action.IRoomAction, IAction {
            public bool shouldJoinRandomRoom;
        }

        public class Create : RoomConnector.Action.IRoomAction, IAction {
            public RoomOptions roomOptions;
            public TypedLobby typedLobby;
        }

        public class Leave : RoomConnector.Action.IRoomAction, IAction {}
    }

    public class ActionCreator : IActionCreator {
        /// <summary>
        /// Thunk that joins Photon Network room
        /// </summary>
        /// <returns>ThunkAction for on success or failure handlers</returns>
        public IAction JoinRoomStart(Action.Join action) {
            // return thunk to store, which will dispatch new actions upon success or failure
            return new ThunkAction<State> ((dispatch, getState) => {
                // dispatch first action to set state to loading
                dispatch(new RoomConnector.Action.JoinStart {});

                // join by room name is specified
                if (action.roomName != null && action.roomName != "") {
                    PhotonNetwork.JoinRoom(action.roomName);
                
                // join random room if specified
                } else if (action.shouldJoinRandomRoom) {
                    // TODO: add joining random room with filters
                    PhotonNetwork.JoinRandomRoom();
                
                // dispatch join failure
                } else {
                    dispatch(new RoomConnector.Action.JoinFailure {
                        feedbackText = $"Joining room: {action.roomName} and joining random room failed."
                    });
                }
            });
        }

        /// <summary>
        /// Thunk that joins Photon Network room
        /// </summary>
        /// <returns>ThunkAction for on success or failure handlers</returns>
        public IAction CreateRoomStart(Action.Create action) {
            // return thunk to store, which will dispatch new actions upon success or failure
            return new ThunkAction<State> ((dispatch, getState) => {
                // dispatch first action to set state to starting
                dispatch(new RoomConnector.Action.CreateStart {});

                // Creates a room with options if specified
                if (action.roomOptions != null || action.typedLobby != null) {
                    PhotonNetwork.CreateRoom(action.roomName, action.roomOptions, action.typedLobby);

                // Else creates a room but fails if this room is existing already. Can only be called on Master Server.
                } else {
                    // will use random room name if roomName is null
                    PhotonNetwork.CreateRoom(action.roomName);
                }
            });
        }

        /// <summary>
        /// Thunk that leaves Photon Network room
        /// </summary>
        /// <returns>ThunkAction for on success or failure handlers</returns>
        public IAction LeaveRoom(Action.Leave action) {
            // return thunk to store, which will dispatch new actions upon success or failure
            return new ThunkAction<State> ((dispatch, getState) => {
                // dispatch first action to set state to loading
                dispatch(new RoomConnector.Action.LeaveStart {});

                PhotonNetwork.LeaveRoom();
            });
        }
    }

    [Serializable]
    /// <summary>
    /// Public settings for api loading
    /// </summary>
    public class Settings {

        // The maximum number of players per room
        public byte maxPlayersPerRoom = 4;

		// #Critical: The first we try to do is to join a potential existing room.
		// If there is, good, else, we'll be called back with OnPhotonRandomJoinFailed()
		public bool shouldJoinRandomRoom = true;
    }
}
