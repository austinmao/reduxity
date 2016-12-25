using UnityEngine;

namespace Reduxity.Example.PlayerMovement {
    public class State {
        public MoveState Movement { get; set; }

        /* default state at app start-up */
        public State Initialize() {
            return new State {
                Movement = new MoveState {
                    isMoving = false,
                    distance = Vector3.zero
                }
            };
        }
    }

    public class MoveState {
        public bool isMoving { get; set; }
        public Vector3 distance { get; set; }
    }
}
