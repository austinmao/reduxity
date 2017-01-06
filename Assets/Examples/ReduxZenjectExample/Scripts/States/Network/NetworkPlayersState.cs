using Zenject;

namespace Reduxity.Example.Zenject {

    public class NetworkPlayersState : IState {

		/// <summary>The list of players in the current room, excluding the local player.</summary>
		/// <remarks>
		/// This list is only valid, while the client is in a room.
		/// It automatically gets updated when someone joins or leaves.
		///
		/// This can be used to list all other players in a room.
		/// Each player's PhotonPlayer.customProperties are accessible (set and synchronized via
		/// PhotonPlayer.SetCustomProperties).
		///
		/// You can use a PhotonPlayer.TagObject to store an arbitrary object for reference.
		/// That is not synchronized via the network.
		/// </remarks>
		public PhotonPlayer[] otherPlayers { get; set; }
    }

    public class NetworkPlayersStateInitializer : IStateInitializer, IInitializable {
        readonly NetworkPlayersState state_;

        public NetworkPlayersStateInitializer(NetworkPlayersState networkPlayersState) {
            state_ = networkPlayersState;
        }

        public void Initialize() {
        }
    }
}

