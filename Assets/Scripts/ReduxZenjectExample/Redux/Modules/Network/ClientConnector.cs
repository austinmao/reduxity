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
        public class ConnectStart : IClientAction, IAction {
            public bool isSwitching { get; set; }
            public PhotonPlayer photonClient { get; set; }
        }
        public class ConnectSuccess : IClientAction, IAction {
            public PhotonPlayer photonClient { get; set; }
        }
        public class ConnectFailure : IClientAction, IAction {}
        public class DisconnectStart : IClientAction, IAction {}
        public class DisconnectSuccess : IClientAction, IAction {}
        public class DisconnectFailure : IClientAction, IAction {}
    }

    public class Reducer : IReducer {
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
            // must not be connecting to client already
            Assert.IsEqual(state.isConnecting, false);
            // must not be connected to client if not switching client
            if (!state.isSwitching) {
                Assert.IsEqual(state.isConnected, false);
            }

            state.isConnecting = true; // action commencing
            state.isConnected = false;
            state.isConnectionFailed = false;
            state.isSwitching = action.isSwitching;
            state.isDisconnecting = false;
            state.isDisconnected = false;
            state.isDisconnectionFailed = false;
            state.photonClient = action.photonClient; // set client. this will be maintained in state.j
            state.feedbackText = "Connecting to client...";
            return state;
        }

        private ClientState ConnectSuccess(ClientState state, Action.ConnectSuccess action) {
            state.isConnecting = false;
            state.isConnected = true; // action succeeded
            state.isConnectionFailed = false;
            state.isSwitching = false;
            state.isDisconnecting = false;
            state.isDisconnected = false;
            state.isDisconnectionFailed = false;
            state.feedbackText = "Connected to client.";
            return state;
        }
        private ClientState ConnectFailure(ClientState state, Action.ConnectFailure action) {
            state.isConnecting = false;
            state.isConnected = false;
            state.isConnectionFailed = true; // action failed
            state.isSwitching = false;
            state.isDisconnecting = false;
            state.isDisconnected = false;
            state.isDisconnectionFailed = false;
            state.feedbackText = "Failed to connect to client.";
            return state;
        }

        private ClientState StartDisconnect(ClientState state, Action.DisconnectStart action) {
            // guarding assertions
            Assert.IsEqual(state.isConnected, true);

            state.isConnecting = false;
            state.isConnected = true; // still connected
            state.isConnectionFailed = false;
            state.isSwitching = false;
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
            state.isSwitching = false;
            state.isDisconnecting = false;
            state.isDisconnected = true; // action succeeded
            state.isDisconnectionFailed = false;
            state.feedbackText = "Disconnected.";
            return state;
        }

        private ClientState DisconnectFailure(ClientState state, Action.DisconnectFailure action) {
            state.isConnecting = false;
            state.isConnected = true; // still disconnected
            state.isConnectionFailed = false;
            state.isSwitching = false;
            state.isDisconnecting = false;
            state.isDisconnected = false;
            state.isDisconnectionFailed = true; // action failed
            state.feedbackText = "Failed to disconnect.";
            return state;
        }
    }
}
