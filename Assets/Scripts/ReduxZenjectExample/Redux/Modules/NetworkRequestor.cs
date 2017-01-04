using Redux;
using System;
using Photon;
using UnityEngine.UI;

namespace Reduxity.Example.Zenject.NetworkRequestor {
    public class Action {
        /// <summary>
        /// Properties for each type of http request
        /// </summary>
        public abstract class INetworkResponse {
            public string feedbackText { get; set; }
        }

        public class Connect : INetworkResponse, IAction {}
        public class ConnectSuccess : INetworkResponse, IAction {}
        public class ConnectFailure : INetworkResponse, IAction {}
        public class Disconnect : INetworkResponse, IAction {}
        public class DisconnectSuccess : INetworkResponse, IAction {}
    }

    public class Reducer : IReducer {
        readonly Settings settings_;

        public Reducer(Settings settings) {
            settings_ = settings;
        }

        public NetworkState Reduce(NetworkState state, IAction action) {
            if (action is Action.Connect) {
                return StartConnect(state, (Action.Connect)action);
            }

            if (action is Action.ConnectSuccess) {
                return ConnectSuccess(state, (Action.ConnectSuccess)action);
            }

            if (action is Action.ConnectFailure) {
                return ConnectFailure(state, (Action.ConnectFailure)action);
            }

            if (action is Action.Disconnect) {
                return StartDisconnect(state, (Action.Disconnect)action);
            }

            if (action is Action.DisconnectSuccess) {
                return DisconnectSuccess(state, (Action.DisconnectSuccess)action);
            }

            return state;
        }

        private NetworkState StartConnect(NetworkState state, Action.Connect action) {
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

        private NetworkState StartDisconnect(NetworkState state, Action.Disconnect action) {
            state.isConnecting = false;
            state.isConnected = false;
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

        // // The maximum number of players per room
        // byte maxPlayersPerRoom = 4;

        // // #NotImportant
        // // Force LogLevel
        // PhotonLogLevel logLevel = PhotonLogLevel.Full;

        // // #Critical
        // // we don't join the lobby. There is no need to join a lobby to get the list of rooms.
        // bool shouldAutoJoinLobby = false;

        // // #Critical
        // // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        // bool shouldAutomaticallySyncScene = true;
    }
}

