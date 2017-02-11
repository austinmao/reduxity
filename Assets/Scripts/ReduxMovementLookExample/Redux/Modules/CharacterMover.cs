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

        public class StopMove: IAction {}

        public class Turn: IAction {
            // using Vector2 as input for 2-axis rotations. these will be
            // translated to Quaternions in the reducer.
            public Vector2 inputRotation { get; set; }
            public float fixedDeltaTime { get; set; }
        }

        public class StopTurn: IAction {}
    }

    // reducers handle state changes
    public class Reducer {
        public static CharacterState Reduce(object inState, IAction action) {

            CharacterState previousState = inState as CharacterState;

            if (action is Action.Move) {
                return Move(previousState, (Action.Move)action);
            }

            if (action is Action.StopMove) {
                return StopMove(previousState, (Action.StopMove)action);
            }

            if (action is Action.Turn) {
                return Turn(previousState, (Action.Turn)action);
            }

            if (action is Action.StopTurn) {
                return StopTurn(previousState, (Action.StopTurn)action);
            }

            return previousState;
        }

        // TODO: clone state
        /* calculate distance from velocity and transform */
        public static CharacterState Move(CharacterState state, Action.Move action) {
            var inputVelocity = action.inputVelocity;
            var transform = state.transform;
            var playerVelocity = (inputVelocity.x * transform.right) + (inputVelocity.y * transform.forward);
            var distance = playerVelocity * action.fixedDeltaTime;

            state.isMoving = true;
            state.isTurning = false;
            state.moveDistance = distance;
            Debug.Log($"in Move, returning state: {ObjectDumper.Dump(state)}");
            return state;
        }

        public static CharacterState StopMove(CharacterState state, Action.StopMove action) {
            state.isMoving = false;
            state.moveDistance = Vector3.zero;
            Debug.Log($"in StopMove, returning state: {ObjectDumper.Dump(state)}");
            return state;
        }

        // TODO: get this to work
        public static CharacterState Turn(CharacterState state, Action.Turn action) {
            Vector2 rotation = action.inputRotation;
            float time = action.fixedDeltaTime;
            int lookSpeed = ReduxityInitializer.Instance.lookSpeed; // public setting. TODO: inject

            // inputLook.x rotates the character around the vertical axis (with + being right)
            Vector3 horizontalLook = rotation.x * time * Vector3.up * lookSpeed;
            
            state.transform.localRotation *= Quaternion.Euler(horizontalLook);
            state.isTurning = true;
            state.isMoving = false;
            Debug.Log($"in Turn, returning state: {ObjectDumper.Dump(state)}");
            return state;
        }

        public static CharacterState StopTurn(CharacterState state, Action.StopTurn action) {
            state.isTurning = false;
            Debug.Log($"in StopTurn, returning state: {ObjectDumper.Dump(state)}");
            return state;
        }
    }
}
