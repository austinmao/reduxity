using Redux;
using System;
using ModestTree;

namespace Reduxity.Example.Zenject.NetworkRequestor {
    public class Action {
        /// <summary>
        /// Properties for each type of http request
        /// </summary>
        public abstract class INetworkAction {
            public string feedbackText { get; set; }
        }
        public class ConnectStart : INetworkAction, IAction {}
        public class ConnectSuccess : INetworkAction, IAction {}
        public class ConnectFailure : INetworkAction, IAction {}
        public class DisconnectStart : INetworkAction, IAction {}
        public class DisconnectSuccess : INetworkAction, IAction {}
    }

    public class Reducer : IReducer {
        readonly Settings settings_;

        public Reducer(Settings settings) {
            settings_ = settings;
        }

        public NetworkState Reduce(NetworkState state, IAction action) {
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

            return state;
        }

        private NetworkState StartConnect(NetworkState state, Action.ConnectStart action) {
            // guarding assertions
            Assert.IsEqual(state.isConnecting, false);
            Assert.IsEqual(state.isConnected, false);

            state.isConnecting = true;
            state.isConnected = false;
            state.isConnectionFailed = false;
            state.isDisconnecting = false;
            state.feedbackText = "Connecting...";
            return state;
        }

        private NetworkState ConnectSuccess(NetworkState state, Action.ConnectSuccess action) {
            state.isConnecting = false;
            state.isConnected = true;
            state.isConnectionFailed = false;
            state.isDisconnecting = false;
            state.feedbackText = "Joining room...";
            return state;
        }

        private NetworkState ConnectFailure(NetworkState state, Action.ConnectFailure action) {
            state.isConnecting = false;
            state.isConnected = false;
            state.isConnectionFailed = true;
            state.isDisconnecting = false;
            state.feedbackText = "Connection failed.";
            return state;
        }

        private NetworkState StartDisconnect(NetworkState state, Action.DisconnectStart action) {
            // guarding assertions
            Assert.IsEqual(state.isConnected, true);

            state.isConnecting = false;
            state.isConnected = true;
            state.isConnectionFailed = false;
            state.isDisconnecting = true;
            state.feedbackText = "Disconnecting...";
            return state;
        }

        private NetworkState DisconnectSuccess(NetworkState state, Action.DisconnectSuccess action) {
            state.isConnecting = false;
            state.isConnected = false;
            state.isConnectionFailed = false;
            state.isDisconnecting = false;
            state.feedbackText = "Disconnection successful.";
            return state;
        }
    }

    [Serializable]
    /// <summary>
    /// Public settings for api loading
    /// </summary>
    public class Settings {

        // The maximum number of players per room
        public byte maxPlayersPerRoom = 4;

        // #NotImportant
        // Force LogLevel
        public PhotonLogLevel logLevel = PhotonLogLevel.Full;

        // #Critical
        // we don't join the lobby. There is no need to join a lobby to get the list of rooms.
        public bool shouldAutoJoinLobby = false;

        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        public bool shouldAutomaticallySyncScene = true;
    }
}

