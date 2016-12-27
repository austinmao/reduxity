using System;
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


    /// <summary>
    /// Nested State node that governs Character movement.
    /// </summary>
    public class CharacterState : IInitializable {
        readonly CharacterController character_;
        public bool isMoving { get; set; }
        public bool isTurning { get; set; }
        public Vector3 moveDistance { get; set; } // how much to move character
        public Transform transform { get; set; }

        /// <summary>
        /// Character state constructor
        /// </summary>
        /// <param name="character">CharacterController GameObject injected via editor</param>
        public CharacterState(CharacterController character) {
            character_ = character;
        }

        /// <summary>
        /// Initiale state
        /// </summary>
        public void Initialize() {
            isMoving = false;
            isTurning = false;
            moveDistance = Vector3.zero;
            transform = character_.transform;
        }
    }

    /// <summary>
    /// Nested State node that governs Camera transform.
    /// </summary>
    public class CameraState : IInitializable {
        readonly Camera camera_;

        public bool isLooking { get; set; }
        public Quaternion localRotation { get; set; }

        /// <summary>
        /// Camera state constructor
        /// </summary>
        /// <param name="camera">Camera GameObject injected via editor</param>
        public CameraState(Camera camera) {
            camera_ = camera;
        }

        public void Initialize() {
            isLooking = false;
            localRotation = camera_.transform.localRotation;
        }
    }
}
