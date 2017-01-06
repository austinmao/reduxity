using Redux;
using System;

namespace Reduxity.Example.Zenject.NetworkPlayer {
    public class Action {
        /// <summary>
        /// Properties for each network player action
        /// </summary>
        public abstract class INetworkPlayerAction {
        }
    }

    public class Reducer : IReducer {
        public NetworkPlayerState Reduce(NetworkPlayerState state, IAction action) {
            return state;
        }

        // private NetworkPlayerState StartConnect(NetworkPlayerState state, Action.ConnectStart action) {
        // }
    }
}
