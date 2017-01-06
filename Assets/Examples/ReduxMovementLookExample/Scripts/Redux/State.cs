using UnityEngine;

namespace Reduxity.Example.PlayerMovementLook {
    public class State {
        public CharacterState Character { get; set; }
        public CameraState Camera { get; set; }

        /* default state at app start-up. every property needs to be specified. */
        public State Initialize() {
            return new State {
                Character = new CharacterState {
                    isMoving = false,
                    isTurning = false,
                    moveDistance = Vector3.zero,
                    transform = ReduxityInitializer.Instance.Player.transform // TODO: Inject this
                },
                Camera = new CameraState {
                    isLooking = false,
                    localRotation = ReduxityInitializer.Instance.Camera.transform.localRotation // TODO: Inject this
                }
            };
        }
    }

    public class CharacterState : IState {
        public bool isMoving { get; set; }
        public bool isTurning { get; set; }
        public Vector3 moveDistance { get; set; } // how much to move character
        public Transform transform { get; set; }
    }

    public class CameraState : IState {
        public bool isLooking { get; set; }
        public Quaternion localRotation { get; set; }
    }
}
