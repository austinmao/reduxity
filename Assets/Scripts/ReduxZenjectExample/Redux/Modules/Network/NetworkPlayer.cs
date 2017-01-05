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
        readonly Settings settings_;

        public Reducer(Settings settings) {
            settings_ = settings;
        }

        public NetworkPlayerState Reduce(NetworkPlayerState state, IAction action) {
            return state;
        }

        // private NetworkPlayerState StartConnect(NetworkPlayerState state, Action.ConnectStart action) {
        // }
    }

    [Serializable]
    /// <summary>
    /// </summary>
    public class Settings {
    }
}
