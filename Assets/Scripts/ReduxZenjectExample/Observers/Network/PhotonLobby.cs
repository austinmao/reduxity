using Zenject;
using System;
using System.Collections.Generic;
using Redux;

namespace Reduxity.Example.Zenject {
	public class PhotonLobbyObserver : IComponent {

		readonly App app_;
		readonly Settings settings_;
        readonly LobbyConnectCreator.ActionCreator actionCreator_;
		readonly Dispatcher dispatch_;

        public PhotonLobbyObserver(
			App app,
			Settings settings,
            LobbyConnectCreator.ActionCreator actionCreator

		) {
			app_ = app;
			settings_ = settings;
            actionCreator_ = actionCreator;
			dispatch_ = app_.Store.Dispatch;
		}

		public void Initialize() {
		}

        /// <summary>
        /// Called on entering a lobby on the Master Server. The actual room-list updates will call OnReceivedRoomListUpdate().
        /// </summary>
        /// <remarks>
        /// Note: When PhotonNetwork.autoJoinLobby is false, OnConnectedToMaster() will be called and the room list won't become available.
        ///
        /// While in the lobby, the roomlist is automatically updated in fixed intervals (which you can't modify).
        /// The room list gets available when OnReceivedRoomListUpdate() gets called after OnJoinedLobby().
        /// </remarks>
        public void OnJoinedLobby() {
            var action = new LobbyConnector.Action.JoinSuccess {};
            dispatch_(action);
        }

        /// <summary>
        /// Called after leaving a lobby.
        /// </summary>
        /// <remarks>
        /// When you leave a lobby, [CreateRoom](@ref PhotonNetwork.CreateRoom) and [JoinRandomRoom](@ref PhotonNetwork.JoinRandomRoom)
        /// automatically refer to the default lobby.
        /// </remarks>
        public void OnLeftLobby() {
            var action = new LobbyConnector.Action.LeaveSuccess {};
            dispatch_(action);
        }


        /// <summary>
        /// Called when the Master Server sent an update for the Lobby Statistics, updating PhotonNetwork.LobbyStatistics.
        /// </summary>
        /// <remarks>
        /// This callback has two preconditions:
        /// EnableLobbyStatistics must be set to true, before this client connects.
        /// And the client has to be connected to the Master Server, which is providing the info about lobbies.
        /// </remarks>
        public void OnLobbyStatisticsUpdate() {
        }

		[Serializable]
		/// <summary>
		/// </summary>
		public class Settings {
		}
	}
}