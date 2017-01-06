using Redux;
using Redux.Middleware;

namespace Reduxity.Example.Zenject.ClientConnectCreator {
    public class Action {
        /// <summary>
        /// Properties for each type of http request
        /// </summary>
        public class Connect : IAction {}
        public class Switch : IAction {}
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
                dispatch(new ClientConnector.Action.ConnectStart {});

                // TODO: add this if ever used
                // connect to client. Callbacks are handled by Photon.
                /// <param name="masterServerAddress">The server's address (either your own or Photon Cloud address).</param>
                /// <param name="port">The server's port to connect to.</param>
                /// <param name="appID">Your application ID (Photon Cloud provides you with a GUID for your game).</param>
                /// <param name="gameVersion">This client's version number. Users are separated by gameversion (which allows you to make breaking changes).</param>
                // PhotonNetwork.ConnectToMaster(masterServerAddress, port, appID, gameVersion);
            });
        }

        /// <summary>
        /// Thunk that disconnects from Photon Network
        /// </summary>
        /// <returns>ThunkAction for on success or failure handlers</returns>
        public IAction DisconnectStart(Action.Disconnect action) {
            // return thunk to store, which will dispatch new actions upon success or failure
            return new ThunkAction<State> ((dispatch, getState) => {
                dispatch(new ClientConnector.Action.DisconnectStart {});

                // TODO: add this if ever used
                // PhotonNetwork.Disconnect();
            });
        }
    }
}
