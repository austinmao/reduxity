using UnityEngine;
using Redux;

namespace Reduxity.Example.PlayerMovementLook.Look {
    // actions must have a type and may include a payload
    public class Action {
        public class Look: IAction {
            // using Vector2 as input for 2-axis movements. these will be
            // translated to Vector3 in the reducer.
            public Vector2 inputRotation { get; set; }
            public Transform cameraTransform { get; set; }
            public float fixedDeltaTime { get; set; }
        }

        public class Stop: IAction {}
    }

    // reducers handle state changes
    public static class Reducer {
        public static State Reduce(State previousState, IAction action) {
            // Debug.Log($"reducing with action: {action}");
            if (action is Action.Look) {
                return Look(previousState, (Action.Look)action);
            }

            if (action is Action.Stop) {
                return Stop(previousState, (Action.Stop)action);
            }

            return previousState;
        }

        // Translate 2D mouse input into euler angle rotations.
        public static State Look(State state, Action.Look action) {
            Transform playerTransform = state.Character.playerTransform;
            Transform cameraTransform = state.Character.cameraTransform;

            Vector3 rotation = action.inputRotation;
            float time = action.fixedDeltaTime;

            // inputLook.x rotates the character around the vertical axis (with + being right)
            Vector3 horzLook = rotation.x * time * Vector3.up;
            Quaternion playerLocalRotation = playerTransform.localRotation * Quaternion.Euler(horzLook);

            // inputLook.y rotates the camera around the horizontal axis (with + being up)
            var vertLook = rotation.y * time * Vector3.left;
            Quaternion cameraLookRotation = cameraTransform.localRotation * Quaternion.Euler(vertLook);

            state.Character.isLooking = true;
            state.Character.lookRotation = cameraLookRotation;
            state.Character.playerTransform.localRotation = playerLocalRotation;
            return state;
        }

        public static State Stop(State state, Action.Stop action) {
            state.Character.isLooking = false;
            return state;
        }
    }
}
