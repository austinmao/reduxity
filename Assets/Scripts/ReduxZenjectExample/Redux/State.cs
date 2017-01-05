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
        public CloudState Network;
        public RoomState Room;

        /// <summary>
        /// Inject nested state nodes into the Redux State that are already initialized.
        /// Thus, the initial State does not need an initializer itself.
        /// </summary>
        public State(
            CharacterState characterState,
            CameraState cameraState,
            ApiState apiState,
            CloudState networkState,
            RoomState roomState
        ) {
            Character = characterState;
            Camera = cameraState;
            Api = apiState;
            Network = networkState;
            Room = roomState;
        }
    }
}
