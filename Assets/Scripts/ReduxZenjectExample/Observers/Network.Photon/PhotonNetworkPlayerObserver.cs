using Zenject;
using System;
using System.Collections.Generic;
using Redux;

namespace Reduxity.Example.Zenject {
	public class PhotonNetworkPlayerObserver : IComponent {

		readonly App app_;
		readonly Settings settings_;
        readonly NetworkPlayerCreator.ActionCreator actionCreator_;
		readonly Dispatcher dispatch_;

        public PhotonNetworkPlayerObserver(
			App app,
			Settings settings,
            NetworkPlayerCreator.ActionCreator actionCreator

		) {
			app_ = app;
			settings_ = settings;
            actionCreator_ = actionCreator;
			dispatch_ = app_.Store.Dispatch;
		}

		public void Initialize() {
		}

        /// <summary>
        /// Called when a remote player entered the room. This PhotonPlayer is already added to the playerlist at this time.
        /// </summary>
        /// <remarks>
        /// If your game starts with a certain number of players, this callback can be useful to check the
        /// Room.playerCount and find out if you can start.
        /// </remarks>
        public void OnPhotonPlayerConnected(PhotonPlayer newPlayer) {
            var action = new NetworkPlayers.Action.ConnectPlayer {};
            dispatch_(action);
        }

        /// <summary>
        /// Called when a remote player left the room. This PhotonPlayer is already removed from the playerlist at this time.
        /// </summary>
        /// <remarks>
        /// When your client calls PhotonNetwork.leaveRoom, PUN will call this method on the remaining clients.
        /// When a remote client drops connection or gets closed, this callback gets executed. after a timeout
        /// of several seconds.
        /// </remarks>
        public void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer) {
            // TODO: decide if we want to use otherPlayer for any info
            var action = new NetworkPlayers.Action.DisconnectPlayer {};
            dispatch_(action);
        }

        /// <summary>
        /// Called when custom player-properties are changed. Player and the changed properties are passed as object[].
        /// </summary>
        /// <remarks>
        /// Since v1.25 this method has one parameter: object[] playerAndUpdatedProps, which contains two entries.<br/>
        /// [0] is the affected PhotonPlayer.<br/>
        /// [1] is the Hashtable of properties that changed.<br/>
        ///
        /// We are using a object[] due to limitations of Unity's GameObject.SendMessage (which has only one optional parameter).
        ///
        /// Changing properties must be done by PhotonPlayer.SetCustomProperties, which causes this callback locally, too.
        ///
        /// Example:<pre>
        /// void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps) {
        ///     PhotonPlayer player = playerAndUpdatedProps[0] as PhotonPlayer;
        ///     Hashtable props = playerAndUpdatedProps[1] as Hashtable;
        ///     //...
        /// }</pre>
        /// </remarks>
        /// <param name="playerAndUpdatedProps">Contains PhotonPlayer and the properties that changed See remarks.</param>
        public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps) {
            // TODO: player state
        }

		[Serializable]
		/// <summary>
		/// </summary>
		public class Settings {
		}
	}
}

