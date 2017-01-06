using Redux;
using System;

namespace Reduxity.Example.Zenject.NetworkPlayers
{
    public class Action {
        /// <summary>
        /// Properties for each network player action
        /// </summary>
        public abstract class INetworkPlayersAction {
            public PhotonPlayer photonPlayer { get; set; }
        }

        public class ConnectPlayer : INetworkPlayersAction, IAction {}
        public class DisconnectPlayer : INetworkPlayersAction, IAction {}
    }

    public class Reducer : IReducer {
        public NetworkPlayersState Reduce(NetworkPlayersState state, IAction action) {
            if (action is Action.ConnectPlayer) {
                return ConnectPlayer(state, (Action.ConnectPlayer)action);
            }

            if (action is Action.DisconnectPlayer) {
                return DisconnectPlayer(state, (Action.DisconnectPlayer)action);
            }

            return state;
        }

        private NetworkPlayersState ConnectPlayer(NetworkPlayersState state, Action.ConnectPlayer action) {
			// update otherPlayers based on Photon's list
			state.otherPlayers = PhotonNetwork.otherPlayers;
			return state;
		}

        private NetworkPlayersState DisconnectPlayer(NetworkPlayersState state, Action.DisconnectPlayer action) {
			// update otherPlayers based on Photon's list
			state.otherPlayers = PhotonNetwork.otherPlayers;
			return state;
		}
    }
}
