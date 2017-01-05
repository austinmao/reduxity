using System;
using Redux;
using ExitGames.Client.Photon;

namespace Reduxity.Example.Zenject {
	public class PhotonRoomObserver : IComponent {

		readonly App app_;
		readonly Settings settings_;
        readonly RoomConnectCreator.ActionCreator actionCreator_;
		readonly Dispatcher dispatch_;

        public PhotonRoomObserver(
			App app,
			Settings settings,
            RoomConnectCreator.ActionCreator actionCreator
		) {
			app_ = app;
			settings_ = settings;
            actionCreator_ = actionCreator;
			dispatch_ = app_.Store.Dispatch;
		}

		public void Initialize() {
		}

        /// <summary>
        /// Called when entering a room (by creating or joining it). Called on all clients (including the Master Client).
        /// </summary>
        /// <remarks>
        /// This method is commonly used to instantiate player characters.
        /// If a match has to be started "actively", you can call an [PunRPC](@ref PhotonView.RPC) triggered by a user's button-press or a timer.
        ///
        /// When this is called, you can usually already access the existing players in the room via PhotonNetwork.playerList.
        /// Also, all custom properties should be already available as Room.customProperties. Check Room.playerCount to find out if
        /// enough players are in the room to start playing.
        /// </remarks>
        public void OnJoinedRoom() {
            var action = new RoomConnector.Action.JoinSuccess {};
            dispatch_(action);
        }

        /// <summary>
        /// Called when a JoinRoom() call failed. The parameter provides ErrorCode and message (as array).
        /// </summary>
        /// <remarks>
        /// Most likely error is that the room does not exist or the room is full (some other client was faster than you).
        /// PUN logs some info if the PhotonNetwork.logLevel is >= PhotonLogLevel.Informational.
        /// </remarks>
        /// <param name="codeAndMsg">codeAndMsg[0] is short ErrorCode. codeAndMsg[1] is string debug msg.</param>
        public void OnPhotonJoinRoomFailed(object[] codeAndMsg) {
            var action = new RoomConnector.Action.JoinFailure {};
            dispatch_(action);
        }

        /// <summary>
        /// Called when a JoinRandom() call failed. The parameter provides ErrorCode and message.
        /// </summary>
        /// <remarks>
        /// Most likely all rooms are full or no rooms are available. <br/>
        /// When using multiple lobbies (via JoinLobby or TypedLobby), another lobby might have more/fitting rooms.<br/>
        /// PUN logs some info if the PhotonNetwork.logLevel is >= PhotonLogLevel.Informational.
        /// </remarks>
        /// <param name="codeAndMsg">codeAndMsg[0] is short ErrorCode. codeAndMsg[1] is string debug msg.</param>
        public void OnPhotonRandomJoinFailed(object[] codeAndMsg) {
            var action = new RoomConnector.Action.JoinFailure {
                feedbackText = codeAndMsg.ToString() // TODO: check this
            };
            dispatch_(action);
        }

        /// <summary>
        /// Called when this client created a room and entered it. OnJoinedRoom() will be called as well.
        /// </summary>
        /// <remarks>
        /// This callback is only called on the client which created a room (see PhotonNetwork.CreateRoom).
        ///
        /// As any client might close (or drop connection) anytime, there is a chance that the
        /// creator of a room does not execute OnCreatedRoom.
        ///
        /// If you need specific room properties or a "start signal", it is safer to implement
        /// OnMasterClientSwitched() and to make the new MasterClient check the room's state.
        /// </remarks>
        public void OnCreatedRoom() {
            var action = new RoomConnector.Action.CreateSuccess {};
            dispatch_(action);
        }

        /// <summary>
        /// Called when a CreateRoom() call failed. The parameter provides ErrorCode and message (as array).
        /// </summary>
        /// <remarks>
        /// Most likely because the room name is already in use (some other client was faster than you).
        /// PUN logs some info if the PhotonNetwork.logLevel is >= PhotonLogLevel.Informational.
        /// </remarks>
        /// <param name="codeAndMsg">codeAndMsg[0] is a short ErrorCode and codeAndMsg[1] is a string debug msg.</param>
        public void OnPhotonCreateRoomFailed(object[] codeAndMsg) {
            var action = new RoomConnector.Action.CreateFailure {};
            dispatch_(action);
        }

        /// <summary>
        /// Called when the local user/client left a room.
        /// </summary>
        /// <remarks>
        /// When leaving a room, PUN brings you back to the Master Server.
        /// Before you can use lobbies and join or create rooms, OnJoinedLobby() or OnConnectedToMaster() will get called again.
        /// </remarks>
        public void OnLeftRoom() {
			var action = new RoomConnector.Action.LeaveSuccess {};
			dispatch_(action);
        }

        /// <summary>
        /// Called for any update of the room-listing while in a lobby (PhotonNetwork.insideLobby) on the Master Server.
        /// </summary>
        /// <remarks>
        /// PUN provides the list of rooms by PhotonNetwork.GetRoomList().<br/>
        /// Each item is a RoomInfo which might include custom properties (provided you defined those as lobby-listed when creating a room).
        ///
        /// Not all types of lobbies provide a listing of rooms to the client. Some are silent and specialized for server-side matchmaking.
        /// </remarks>
        public void OnReceivedRoomListUpdate() {
        }

        /// <summary>
        /// Called when a room's custom properties changed. The propertiesThatChanged contains all that was set via Room.SetCustomProperties.
        /// </summary>
        /// <remarks>
        /// Since v1.25 this method has one parameter: Hashtable propertiesThatChanged.<br/>
        /// Changing properties must be done by Room.SetCustomProperties, which causes this callback locally, too.
        /// </remarks>
        /// <param name="propertiesThatChanged"></param>
        public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged) {
        }

		[Serializable]
		/// <summary>
		/// </summary>
		public class Settings {
		}
	}
}
