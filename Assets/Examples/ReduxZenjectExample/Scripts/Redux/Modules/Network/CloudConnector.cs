using Redux;
using System;
using ModestTree;

namespace Reduxity.Example.Zenject.CloudConnector {
    public class Action {
        /// <summary>
        /// Properties for each type of http request
        /// </summary>
        public abstract class ICloudAction {
            public string feedbackText { get; set; }
        }
        public class ConnectStart : ICloudAction, IAction {}
        public class ConnectSuccess : ICloudAction, IAction {}
        public class ConnectFailure : ICloudAction, IAction {
            public DisconnectCause photonDisconnectCause;
        }
        public class DisconnectStart : ICloudAction, IAction {}
        public class DisconnectSuccess : ICloudAction, IAction {}
        public class DisconnectFailure : ICloudAction, IAction {}
    }

    public class Reducer : IReducer {
        public CloudState Reduce(CloudState state, IAction action) {
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

        private CloudState StartConnect(CloudState state, Action.ConnectStart action) {
            // guarding assertions
            Assert.IsEqual(state.isConnecting, false);
            Assert.IsEqual(state.isConnected, false);

            state.isConnecting = true; // action commencing
            state.isConnected = false;
            state.isConnectionFailed = false;
            state.isDisconnecting = false;
            state.isDisconnected = true;
            state.isDisconnectionFailed = false;
            state.feedbackText = "Connecting...";
            return state;
        }

        private CloudState ConnectSuccess(CloudState state, Action.ConnectSuccess action) {
            state.isConnecting = false;
            state.isConnected = true; // action succeeded
            state.isConnectionFailed = false;
            state.isDisconnecting = false;
            state.isDisconnected = false;
            state.isDisconnectionFailed = false;
            state.feedbackText = "Joining room...";
            return state;
        }

        private CloudState ConnectFailure(CloudState state, Action.ConnectFailure action) {
            state.isConnecting = false;
            state.isConnected = false;
            state.isConnectionFailed = true; // action failed
            state.isDisconnecting = false;
            state.isDisconnected = true;
            state.isDisconnectionFailed = false;
            state.feedbackText = "Connection failed.";
            return state;
        }

        private CloudState StartDisconnect(CloudState state, Action.DisconnectStart action) {
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

        private CloudState DisconnectSuccess(CloudState state, Action.DisconnectSuccess action) {
            state.isConnecting = false;
            state.isConnected = false; // now disconnected
            state.isConnectionFailed = false;
            state.isDisconnecting = false;
            state.isDisconnected = true; // action succeeded
            state.isDisconnectionFailed = false;
            state.feedbackText = "Disconnection successful.";
            return state;
        }

        private CloudState DisconnectFailure(CloudState state, Action.DisconnectFailure action) {
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
}
