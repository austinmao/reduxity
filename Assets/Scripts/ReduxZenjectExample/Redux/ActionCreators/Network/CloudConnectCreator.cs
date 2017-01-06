using Redux;
using Redux.Middleware;
using UniRx;
using PhotonRx;

namespace Reduxity.Example.Zenject.CloudConnectCreator {
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
                // dispatch first action to set state to loading
                dispatch(new CloudConnector.Action.ConnectStart {});

                // connect to PhotonCloud. Callbacks are handled by Photon.
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
                // dispatch first action to set state to loading
                // dispatch(new RoomConnector.Action.JoinStart {});
            });
        }

        // private IConnectableObservable<bool> ConnectToCloudObservable() {
        //     return this.OnJoinedLobbyAsObservable().Cast(default(object))
        //         .Merge(this.OnFailedToConnectToPhotonAsObservable().Cast(default(object)))
        //         .FirstOrDefault() //OnCompletedを発火させるため
        //         .PublishLast();   //PhotonNetwork.Connectを呼び出すより前にストリームを稼働させるため

        // }

    }
}

