using Zenject;
using System;
using System.Collections.Generic;
using Redux;

namespace Reduxity.Example.Zenject {
	public class PhotonClientObserver : IComponent {

		readonly App app_;
		readonly Settings settings_;
        readonly ClientConnectCreator.ActionCreator actionCreator_;
		readonly Dispatcher dispatch_;

        public PhotonClientObserver(
			App app,
			Settings settings,
            ClientConnectCreator.ActionCreator actionCreator

		) {
			app_ = app;
			settings_ = settings;
            actionCreator_ = actionCreator;
			dispatch_ = app_.Store.Dispatch;
		}

		public void Initialize() {
		}


        /// <summary>
        /// Called after the connection to the master is established and authenticated but only when PhotonNetwork.autoJoinLobby is false.
        /// </summary>
        /// <remarks>
        /// If you set PhotonNetwork.autoJoinLobby to true, OnJoinedLobby() will be called instead of this.
        ///
        /// You can join rooms and create them even without being in a lobby. The default lobby is used in that case.
        /// The list of available rooms won't become available unless you join a lobby via PhotonNetwork.joinLobby.
        /// </remarks>
        public void OnConnectedToMaster() {
			var action = new RoomConnector.Action.JoinStart {
				// shouldJoinRandomRoom = settings_.shouldAutoJoinLobby,
                // roomName = 
			};
			// dispatch_(actionCreator_.JoinRoomStart(action));
        }

        /// <summary>
        /// Called after switching to a new MasterClient when the current one leaves.
        /// </summary>
        /// <remarks>
        /// This is not called when this client enters a room.
        /// The former MasterClient is still in the player list when this method get called.
        /// </remarks>
        public void OnMasterClientSwitched(PhotonPlayer newMasterClient) {
        }

        /// <summary>
        /// Because the concurrent user limit was (temporarily) reached, this client is rejected by the server and disconnecting.
        /// </summary>
        /// <remarks>
        /// When this happens, the user might try again later. You can't create or join rooms in OnPhotonMaxCcuReached(), cause the client will be disconnecting.
        /// You can raise the CCU limits with a new license (when you host yourself) or extended subscription (when using the Photon Cloud).
        /// The Photon Cloud will mail you when the CCU limit was reached. This is also visible in the Dashboard (webpage).
        /// </remarks>
        public void OnPhotonMaxCccuReached() {
        }

		[Serializable]
		/// <summary>
		/// </summary>
		public class Settings {
		}
	}
}
