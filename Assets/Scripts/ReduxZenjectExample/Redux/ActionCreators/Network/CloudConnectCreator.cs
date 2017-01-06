using Redux;
using Redux.Middleware;
using UnityEngine;
using System;

namespace Reduxity.Example.Zenject.CloudConnectCreator
{

    public class Action {
        /// <summary>
        /// Properties for each type of http request
        /// </summary>
        public class Connect : IAction {
            public string gameVersion;
        }
        public class Disconnect : IAction {}
    }

    public class ActionCreator : IActionCreator {
        /// <summary>
        /// Thunk that connects to Photon Network
        /// </summary>
        /// <returns>ThunkAction for on success or failure handlers</returns>
        public IAction Connect(Action.Connect action) {
            // return thunk to store, which will dispatch new actions upon success or failure
            return new ThunkAction<State> ((dispatch, getState) => {
                // dispatch first action to set state to connecting
                dispatch(new CloudConnector.Action.ConnectStart {});

                // connect to PhotonCloud. Callbacks are handled by Photon.
                Debug.Log($"connect using version: {action.gameVersion}");
				PhotonNetwork.ConnectUsingSettings(action.gameVersion);
            });
        }

        /// <summary>
        /// Thunk that disconnects from Photon Network
        /// </summary>
        /// <returns>ThunkAction for on success or failure handlers</returns>
        public IAction DisconnectStart(Action.Disconnect action) {
            // return thunk to store, which will dispatch new actions upon success or failure
            return new ThunkAction<State> ((dispatch, getState) => {
                // dispatch first action to set state to disconnecting
                dispatch(new CloudConnector.Action.DisconnectStart {});

                // disconnect from PhotonCloud. Callbacks are handled by Photon.
                Debug.Log($"disconnecting");
				PhotonNetwork.Disconnect();
            });
        }
    }

	[Serializable]
	public class Settings {

		/// <summary>
		/// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
		/// </summary>
		public string GameVersion;
        // #NotImportant
        // Force LogLevel
        public PhotonLogLevel logLevel = PhotonLogLevel.Full;
	}
}

