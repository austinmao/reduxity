using UnityEngine;
using Redux;

namespace Reduxity.Example.PlayerMovementLook.Movement {
    // actions must have a type and may include a payload
    public class Action {
        public class Move: IAction {
            // using Vector2 as input for 2-axis movements. these will be
            // translated to Vector3 in the reducer.
            public Vector2 inputVelocity { get; set; }
            public float fixedDeltaTime { get; set; }
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

        // TODO: clone state
        public static State Move(State state, Action.Move action) {
            /* calculate distance from velocity and transform */
            var inputVelocity = action.inputVelocity;
            var playerTransform = state.Character.playerTransform;
            var playerVelocity = (inputVelocity.x * playerTransform.right) + (inputVelocity.y * playerTransform.forward);
            var distance = playerVelocity * action.fixedDeltaTime;

            state.Character.isMoving = true;
            state.Character.moveDistance = distance;

            return state;
        }

        public static State Stop(State state, Action.Stop action) {
            state.Character.isMoving = false;
            state.Character.moveDistance = Vector3.zero;
            return state;
        }
    }
}
