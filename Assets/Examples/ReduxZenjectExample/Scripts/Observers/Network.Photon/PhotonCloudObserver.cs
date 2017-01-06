using Zenject;
using System;
using System.Collections.Generic;
using Redux;
using UniRx;
using UnityEngine;
using PhotonRx;

namespace Reduxity.Example.Zenject {
	public class PhotonCloudObserver : IInitializable {

		readonly App app_;
        readonly CloudConnectCreator.ActionCreator actionCreator_;
        readonly Component component_;

        public PhotonCloudObserver(
			App app,
            CloudConnectCreator.ActionCreator actionCreator
		) {
			app_ = app;
            actionCreator_ = actionCreator;
		}

		public void Initialize() {
		}

		/// <summary>
        /// Called when the initial connection got established but before you can use the server. OnJoinedLobby() or OnConnectedToMaster() are called when PUN is ready.
        /// </summary>
        /// <remarks>
        /// This callback is only useful to detect if the server can be reached at all (technically).
        /// Most often, it's enough to implement OnFailedToConnectToPhoton() and OnDisconnectedFromPhoton().
        ///
        /// <i>OnJoinedLobby() or OnConnectedToMaster() are called when PUN is ready.</i>
        ///
        /// When this is called, the low level connection is established and PUN will send your AppId, the user, etc in the background.
        /// This is not called for transitions from the masterserver to game servers.
        /// </remarks>
        public void OnConnectedToPhoton() {
            var action = new CloudConnector.Action.ConnectSuccess {};
            app_.Store.Dispatch(action);
        }

        /// <summary>
        /// Called if a connect call to the Photon server failed before the connection was established, followed by a call to OnDisconnectedFromPhoton().
        /// </summary>
        /// <remarks>
        /// This is called when no connection could be established at all.
        /// It differs from OnConnectionFail, which is called when an existing connection fails.
        /// </remarks>
        public void OnFailedToConnectToPhoton(DisconnectCause cause) {
            var action = new CloudConnector.Action.ConnectFailure {
                photonDisconnectCause = cause
            };
            app_.Store.Dispatch(action);
        }

        /// <summary>
        /// Called when something causes the connection to fail (after it was established), followed by a call to OnDisconnectedFromPhoton().
        /// </summary>
        /// <remarks>
        /// If the server could not be reached in the first place, OnFailedToConnectToPhoton is called instead.
        /// The reason for the error is provided as DisconnectCause.
        /// </remarks>
        public void OnConnectionFail(DisconnectCause cause) {
            var action = new CloudConnector.Action.ConnectFailure {
                photonDisconnectCause = cause
            };
            app_.Store.Dispatch(action);
        }

        /// <summary>
        /// Called after disconnecting from the Photon server.
        /// </summary>
        /// <remarks>
        /// In some cases, other callbacks are called before OnDisconnectedFromPhoton is called.
        /// Examples: OnConnectionFail() and OnFailedToConnectToPhoton().
        /// </remarks>
        public void OnDisconnectedFromPhoton() {
            var action = new CloudConnector.Action.DisconnectSuccess {};
            app_.Store.Dispatch(action);
        }

        /// <summary>
        /// Called when the custom authentication failed. Followed by disconnect!
        /// </summary>
        /// <remarks>
        /// Custom Authentication can fail due to user-input, bad tokens/secrets.
        /// If authentication is successful, this method is not called. Implement OnJoinedLobby() or OnConnectedToMaster() (as usual).
        ///
        /// During development of a game, it might also fail due to wrong configuration on the server side.
        /// In those cases, logging the debugMessage is very important.
        ///
        /// Unless you setup a custom authentication service for your app (in the [Dashboard](https://www.photonengine.com/dashboard)),
        /// this won't be called!
        /// </remarks>
        /// <param name="debugMessage">Contains a debug message why authentication failed. This has to be fixed during development time.</param>
        public void OnCustomAuthenticationFailed(string debugMessage) {
            var action = new CloudConnector.Action.ConnectFailure {
                feedbackText = debugMessage
            };
            app_.Store.Dispatch(action);
        }


        /// <summary>
        /// Called when your Custom Authentication service responds with additional data.
        /// </summary>
        /// <remarks>
        /// Custom Authentication services can include some custom data in their response.
        /// When present, that data is made available in this callback as Dictionary.
        /// While the keys of your data have to be strings, the values can be either string or a number (in Json).
        /// You need to make extra sure, that the value type is the one you expect. Numbers become (currently) int64.
        ///
        /// Example: void OnCustomAuthenticationResponse(Dictionary&lt;string, object&gt; data) { ... }
        /// </remarks>
        /// <see cref="https://doc.photonengine.com/en/realtime/current/reference/custom-authentication"/>
        public void OnCustomAuthenticationResponse(Dictionary<string, object> data) {
        }
	}
}