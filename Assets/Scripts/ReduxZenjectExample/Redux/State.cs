using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {
    public class State {
        public CharacterState Character;
        public CameraState Camera;
        public ApiState Api;

        /// <summary>
        /// Inject nested state nodes into the Redux State that are already initialized.
        /// Thus, the initial State does not need an initializer itself.
        /// </summary>
        public State(
            CharacterState characterState,
            CameraState cameraState,
            ApiState apiState
        ) {
            Character = characterState;
            Camera = cameraState;
            Api = apiState;
        }
    }
}
