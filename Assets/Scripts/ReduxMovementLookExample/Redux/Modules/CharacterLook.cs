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

        public class StopLook: IAction {}
    }

    // reducers handle state changes
    public static class Reducer {
        public static CameraState Reduce(CameraState previousState, IAction action) {
            // Debug.Log($"reducing with action: {action}");
            if (action is Action.Look) {
                return Look(previousState, (Action.Look)action);
            }

            if (action is Action.StopLook) {
                return StopLook(previousState, (Action.StopLook)action);
            }

            return previousState;
        }

        // Translate 2D mouse input into euler angle rotations.
        public static CameraState Look(CameraState state, Action.Look action) {
            Transform transform = state.transform;
            Vector2 rotation = action.inputRotation;
            float time = action.fixedDeltaTime;

            // inputLook.y rotates the camera around the horizontal axis (with + being up)
            Vector3 vertLook = rotation.y * time * Vector3.left;
            Quaternion cameraLookRotation = transform.localRotation * Quaternion.Euler(vertLook);

            state.isLooking = true;
            state.lookRotation = cameraLookRotation;
            Debug.Log($"in Look, returning state: {ObjectDumper.Dump(state)}");
            return state;
        }

        public static CameraState StopLook(CameraState state, Action.StopLook action) {
            state.isLooking = false;
            Debug.Log($"in StopLook, returning state: {ObjectDumper.Dump(state)}");
            return state;
        }
    }
}
