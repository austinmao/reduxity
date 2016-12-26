using UnityEngine;

namespace Reduxity.Example.PlayerMovementLook {
    public class State : IState<State> {
        public CharacterState Character { get; set; }
        public CameraState Camera { get; set; }

        /* default state at app start-up. every property needs to be specified. */
        public State Initialize() {
            return new State {
                Character = new CharacterState {
                    isMoving = false,
                    isTurning = false,
                    moveDistance = Vector3.zero,
                    transform = ReduxityInitializer.PlayerTransform // TODO: Inject this
                },
                Camera = new CameraState {
                    isLooking = false,
                    lookRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up),
                    transform = ReduxityInitializer.CameraTransform // TODO: Inject this
                }
            };
        }
    }

    public class CharacterState {
        public bool isMoving { get; set; }
        public bool isTurning { get; set; }
        public Vector3 moveDistance { get; set; } // how much to move character
        public Transform transform { get; set; }
    }

    public class CameraState {
        public bool isLooking { get; set; }
        public Quaternion lookRotation { get; set; }
        public Transform transform { get; set; }
    }
}
