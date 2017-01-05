using Redux;
using System;
using ModestTree;

namespace Reduxity.Example.Zenject.ClientConnector {
    public class Action {
        /// <summary>
        /// Properties for each client connector action
        /// </summary>
        public abstract class IClientAction {
            public string feedbackText { get; set; }
        }
        public class ConnectStart : IClientAction, IAction {}
        public class ConnectSuccess : IClientAction, IAction {}
        public class ConnectFailure : IClientAction, IAction {}
        public class DisconnectStart : IClientAction, IAction {}
        public class DisconnectSuccess : IClientAction, IAction {}
        public class DisconnectFailure : IClientAction, IAction {}
    }

    public class Reducer : IReducer {
        readonly Settings settings_;

        public Reducer(Settings settings) {
            settings_ = settings;
        }

        public ClientState Reduce(ClientState state, IAction action) {
            if (action is Action.ConnectStart) {
                return StartConnect(state, (Action.ConnectStart)action);
            }

            if (action is Action.ConnectSuccess) {
                return ConnectSuccess(state, (Action.ConnectSuccess)action);
            }

            if (action is Action.ConnectFailure) {
                return ConnectFailure(state, (Action.ConnectFailure)action);
            }

            if (action is Action.DisconnectStart) {
                return StartDisconnect(state, (Action.DisconnectStart)action);
            }

            if (action is Action.DisconnectSuccess) {
                return DisconnectSuccess(state, (Action.DisconnectSuccess)action);
            }

            if (action is Action.DisconnectFailure) {
                return DisconnectFailure(state, (Action.DisconnectFailure)action);
            }

            return state;
        }

        private ClientState StartConnect(ClientState state, Action.ConnectStart action) {
            // guarding assertions
            Assert.IsEqual(state.isConnecting, false);
            Assert.IsEqual(state.isConnected, false);

            state.isConnecting = true; // action commencing
            state.isConnected = false;
            state.isConnectionFailed = false;
            state.isDisconnecting = false;
            state.isDisconnected = false;
            state.isDisconnectionFailed = false;
            state.feedbackText = "Connecting...";
            return state;
        }

        private ClientState ConnectSuccess(ClientState state, Action.ConnectSuccess action) {
            state.isConnecting = false;
            state.isConnected = true; // action succeeded
            state.isConnectionFailed = false;
            state.isDisconnecting = false;
            state.isDisconnected = false;
            state.isDisconnectionFailed = false;
            state.feedbackText = "Joining room...";
            return state;
        }

        private ClientState ConnectFailure(ClientState state, Action.ConnectFailure action) {
            state.isConnecting = false;
            state.isConnected = false;
            state.isConnectionFailed = true; // action failed
            state.isDisconnecting = false;
            state.isDisconnected = false;
            state.isDisconnectionFailed = false;
            state.feedbackText = "Connection failed.";
            return state;
        }

        private ClientState StartDisconnect(ClientState state, Action.DisconnectStart action) {
            // guarding assertions
            Assert.IsEqual(state.isConnected, true);

            state.isConnecting = false;
            state.isConnected = true; // still connected
            state.isConnectionFailed = false;
            state.isDisconnecting = true; // action commencing
            state.isDisconnected = false;
            state.isDisconnectionFailed = false;
            state.feedbackText = "Disconnecting...";
            return state;
        }

        private ClientState DisconnectSuccess(ClientState state, Action.DisconnectSuccess action) {
            state.isConnecting = false;
            state.isConnected = false; // now disconnected
            state.isConnectionFailed = false;
            state.isDisconnecting = false;
            state.isDisconnected = true; // action succeeded
            state.isDisconnectionFailed = false;
            state.feedbackText = "Disconnection successful.";
            return state;
        }

        private ClientState DisconnectFailure(ClientState state, Action.DisconnectFailure action) {
            state.isConnecting = false;
            state.isConnected = true; // still disconnected
            state.isConnectionFailed = false;
            state.isDisconnecting = false;
            state.isDisconnected = false;
            state.isDisconnectionFailed = true; // action failed
            state.feedbackText = "Disconnection failed.";
            return state;
        }
    }

    [Serializable]
    /// <summary>
    /// Public settings for api loading
    /// </summary>
    public class Settings {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        public bool shouldAutomaticallySyncScene = true;

        // #NotImportant
        // Force LogLevel
        public PhotonLogLevel logLevel = PhotonLogLevel.Full;
    }
}
