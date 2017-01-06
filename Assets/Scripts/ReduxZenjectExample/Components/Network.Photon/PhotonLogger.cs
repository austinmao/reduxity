using Zenject;
using UnityEngine.UI;
using System;

namespace Reduxity.Example.Zenject {

    public class PhotonLoggerComponent : IComponent {

        readonly Text logger_;

		public enum ConnectType {
			cloud,
			client,
			lobby,
			room,
			networkPlayer
		}

		public enum ConnectStatus {
			notConnected,
			connecting,
			connected,
			connectionFailure
		}

        public PhotonLoggerComponent(
            [Inject(Id = "PhotonLogger")]
            Text logger
        ) {
            logger_ = logger;
        }

		public void Render(ConnectStatus status, ConnectType type) {
			// set logger text
			logger_.text = PrepareText_(status, type);
		}

        private string PrepareText_(ConnectStatus status, ConnectType type) {
			string text = "";

			// add action that is being performed to text string
			switch (status) {
				case ConnectStatus.notConnected:
					text += "Click to connect";
					break;
				case ConnectStatus.connecting:
					text += "Connecting";
					break;
				case ConnectStatus.connected:
					text += "Connected";
					break;
				case ConnectStatus.connectionFailure:
					text += "Connection failed";
					break;
			}

			// combine with type
			text += " to";

			// add connection to type
			switch (type) {
				case ConnectType.cloud:
					text += "Photon cloud";
					break;
				case ConnectType.client:
					text += "Photon client";
					break;
				case ConnectType.lobby:
					text += "Photon lobby";
					break;
				case ConnectType.room:
					text += "Photon room";
					break;
				case ConnectType.networkPlayer:
					text += "Photon network player";
					break;
			}

			return text;
        }
    }
}
