using System;
using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {
    public class State {
        public CharacterState Character;
        public CameraState Camera;

        public State(
            CharacterState characterState,
            CameraState cameraState
        ) {
            Character = characterState;
            Camera = cameraState;
        }
    }

    public class CharacterState : IInitializable {
        readonly Player player_;
        public bool isMoving { get; set; }
        public bool isTurning { get; set; }
        public Vector3 moveDistance { get; set; } // how much to move character
        public Transform transform { get; set; }

        public CharacterState(Player player) {
            player_ = player;
        }

        /// <summary>
        /// Initial state
        /// </summary>
        public void Initialize() {
            isMoving = false;
            isTurning = false;
            moveDistance = Vector3.zero;
            transform = player_.transform; // TODO: Inject this
        }
    }

    public class CameraState : IInitializable {
        readonly Camera camera_;

        public bool isLooking { get; set; }
        public Quaternion localRotation { get; set; }

        public CameraState(Camera camera) {
            camera_ = camera;
        }

        public void Initialize() {
            isLooking = false;
            localRotation = camera_.transform.localRotation; // TODO: Inject this
        }
    }
}
