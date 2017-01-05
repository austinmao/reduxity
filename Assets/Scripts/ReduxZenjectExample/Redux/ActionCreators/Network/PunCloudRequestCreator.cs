// using Redux;
// using Redux.Middleware;
// using System.Collections.Generic;
// using UniRx;
// using Hash = System.Collections.Generic.Dictionary<string, string>;
// using Photon;
// using UnityEngine;


// namespace Reduxity.Example.Zenject.PunCloudRequestCreator {
//     public class Action {
//         /// <summary>
//         /// Properties for each type of http request
//         /// </summary>

//         /* Actions for starting an http request */
//         public class ConnectToCloud : IAction {
// 			public string settings;
// 		}
//         public class ConnectToCloudSuccess : IAction {}
//         public class ConnectToCloudFailure : IAction {}
//         public class DisconnectFromCloudStart : IAction {}
//         public class DisconnectFromCloudFailure : IAction {}

//         public class JoinRoom : IAction {
//             public bool shouldJoinRandomRoom;
//             public string roomName;
//         }

//         public class CreateRoom : IAction {
//             public string roomName;
//             public RoomOptions roomOptions;
//             public TypedLobby typedLobby;
//         }
//         public class LeaveRoom : IAction {}
//     }

//     public class ActionCreator : IActionCreator {
//         /// <summary>
//         /// Thunk that connects to Photon Network
//         /// </summary>
//         /// <returns>ThunkAction for on success or failure handlers</returns>
//         public IAction ConnectStart(Action.ConnectToCloud action) {
//             // return thunk to store, which will dispatch new actions upon success or failure
//             return new ThunkAction<State> ((dispatch, getState) => {
//                 // dispatch first action to set state to loading
//                 dispatch(new CloudConnector.Action.ConnectStart {});

//                 // connect to PhotonCloud. Callbacks are handled by Photon.
// 				PhotonNetwork.ConnectUsingSettings(action.settings);
//             });
//         }

//         /// <summary>
//         /// Thunk that connects to Photon Network
//         /// </summary>
//         /// <returns>ThunkAction for on success or failure handlers</returns>
//         public IAction ConnectSuccess(Action.ConnectToCloud action) {
//             // return thunk to store, which will dispatch new actions upon success or failure
//             return new ThunkAction<State> ((dispatch, getState) => {
//                 // dispatch first action to set state to loading
//                 dispatch(new CloudConnector.Action.ConnectStart {});

//                 // connect to PhotonCloud. Callbacks are handled by Photon.
// 				PhotonNetwork.ConnectUsingSettings(action.settings);
//             });
//         }

//         /// <summary>
//         /// Thunk that disconnects from Photon Network
//         /// </summary>
//         /// <returns>ThunkAction for on success or failure handlers</returns>
//         public IAction DisconnectStart(Action.DisconnectFromCloud action) {
//             // return thunk to store, which will dispatch new actions upon success or failure
//             return new ThunkAction<State> ((dispatch, getState) => {
//                 // dispatch first action to set state to loading
//                 dispatch(new RoomConnector.Action.JoinStart {});
//             });
//         }

//         /// <summary>
//         /// Thunk that joins Photon Network room
//         /// </summary>
//         /// <returns>ThunkAction for on success or failure handlers</returns>
//         public IAction JoinRoomStart(Action.JoinRoom action) {
//             // return thunk to store, which will dispatch new actions upon success or failure
//             return new ThunkAction<State> ((dispatch, getState) => {
//                 // dispatch first action to set state to loading
//                 dispatch(new RoomConnector.Action.JoinStart {});

//                 // join by room name is specified
//                 if (action.roomName != null && action.roomName != "") {
//                     PhotonNetwork.JoinRoom(action.roomName);
                
//                 // join random room if specified
//                 } else if (action.shouldJoinRandomRoom) {
//                     PhotonNetwork.JoinRandomRoom();
                
//                 // dispatch join failure
//                 } else {
//                     dispatch(new RoomConnector.Action.JoinFailure {
//                         feedbackText = $"Joining room: {action.roomName} and joining random room failed."
//                     });
//                 }
//             });
//         }

//         /// <summary>
//         /// Thunk that joins Photon Network room
//         /// </summary>
//         /// <returns>ThunkAction for on success or failure handlers</returns>
//         public IAction CreateRoomStart(Action.CreateRoom action) {
//             // return thunk to store, which will dispatch new actions upon success or failure
//             return new ThunkAction<State> ((dispatch, getState) => {
//                 // dispatch first action to set state to starting
//                 dispatch(new RoomConnector.Action.CreateRoomStart {});

//                 // Creates a room but fails if this room is existing already. Can only be called on Master Server.
//                 PhotonNetwork.CreateRoom(action.roomName, action.roomOptions, action.typedLobby);
//             });
//         }

//         /// <summary>
//         /// Thunk that leaves Photon Network room
//         /// </summary>
//         /// <returns>ThunkAction for on success or failure handlers</returns>
//         public IAction LeaveRoom(Action.LeaveRoom action) {
//             // return thunk to store, which will dispatch new actions upon success or failure
//             return new ThunkAction<State> ((dispatch, getState) => {
//                 // dispatch first action to set state to loading
//                 dispatch(new RoomConnector.Action.LeaveStart {});
//             });
//         }
//     }


// }
