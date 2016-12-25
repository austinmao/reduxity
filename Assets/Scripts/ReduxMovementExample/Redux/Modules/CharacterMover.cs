using UnityEngine;
using Redux;

namespace Reduxity.Example.PlayerMovement {
    // actions must have a type and may include a payload
    public class Action {
        public class Move: IAction {
            public Vector2 inputVelocity { get; set; }
            public Transform playerTransform { get; set; } // curent transform
        }

        public class Stop: IAction {}
    }

    // reducers handle state changes
    public static class Reducer {
        public static State Reduce(State previousState, IAction action) {
            // Debug.Log($"reducing with action: {action}");
            if (action is Action.Move) {
                return Move(previousState, (Action.Move)action);
            }

            if (action is Action.Stop) {
                return Stop(previousState, (Action.Stop)action);
            }

            return previousState;
        }

        public static State Move(State previousState, Action.Move action) {
            /* calculate distance from velocity and transform */
            var inputVelocity = action.inputVelocity;
            var playerTransform = action.playerTransform;
            var playerVelocity = (inputVelocity.x * playerTransform.right) + (inputVelocity.y * playerTransform.forward);
            var distance = playerVelocity * Time.fixedDeltaTime;

            // calculate and store distance in state
            MoveState moveState = new MoveState {
                distance = distance,
                isMoving = true
            };

            return new State {
                Movement = moveState
            };
        }

        public static State Stop(State previousState, Action.Stop action) {
            // stop moving
            MoveState moveState = new MoveState {
                isMoving = false
            };

            return new State {
                Movement = moveState
            };
        }
    }
}
