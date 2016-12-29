using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {
    public class State {
        public CharacterState Character;
        public CameraState Camera;

        /// <summary>
        /// Inject nested state nodes into the Redux State that are already initialized.
        /// Thus, the initial State does not need an initializer itself.
        /// </summary>
        /// <param name="characterState"></param>
        /// <param name="cameraState"></param>
        public State(
            CharacterState characterState,
            CameraState cameraState
        ) {
            Character = characterState;
            Camera = cameraState;
        }
    }
}
