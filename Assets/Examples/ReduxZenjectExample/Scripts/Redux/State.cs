using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {

    /// <summary>
    /// Create Redux state object. Each nested state object needs to be public
    /// to enable modules and App to reference them.
    /// </summary>
    public class State {
        public CharacterState Character;
        public CameraState Camera;
        public ApiState Api;
        
        // network states
        public CloudState Cloud;
        public ClientState Client;
        public LobbyState Lobby;
        public RoomState Room;
        public NetworkPlayerState NetworkPlayer;
        public NetworkPlayersState NetworkPlayers;

        /// <summary>
        /// Inject nested state nodes into the Redux State that are already initialized.
        /// Thus, the initial State does not need an initializer itself.
        /// </summary>
        public State(
            CharacterState characterState,
            CameraState cameraState,
            ApiState apiState,
            CloudState cloudState,
            ClientState clientState,
            LobbyState lobbyState,
            RoomState roomState,
            NetworkPlayerState networkPlayerState,
            NetworkPlayersState networkPlayersState
        ) {
            Character = characterState;
            Camera = cameraState;
            Api = apiState;
            Cloud = cloudState;
            Client = clientState;
            Lobby = lobbyState;
            Room = roomState;
            NetworkPlayer = networkPlayerState;
            NetworkPlayers = networkPlayersState;
        }
    }
}
