﻿using UnityEngine;
using Redux;

namespace Reduxity.Example.PlayerMovementLook.Look {
    // actions must have a type and may include a payload
    public class Action {
        public class Look: IAction {
            // using Vector2 as input for 2-axis movements. these will be
            // translated to Vector3 in the reducer.
            public Vector2 inputRotation { get; set; }
            // // cameraTransform is initialized. in a proper redux application, this
            // // can only be mutated form a reducer. therefore, it should never need
            // // to be passed in as a payload of an action
            // public Transform cameraTransform { get; set; }
            public float fixedDeltaTime { get; set; }
        }

        public class StopLook: IAction {}
    }

    // reducers handle state changes
    public class Reducer {
        public static CameraState Reduce(object inState, IAction action) {

            CameraState previousState = inState as CameraState;

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
            Quaternion localRotation = state.localRotation;
            Vector2 rotation = action.inputRotation;
            float time = action.fixedDeltaTime;
            int lookSpeed = ReduxityInitializer.Instance.lookSpeed; // editor setting

            // inputLook.y rotates the camera around the horizontal axis (with + being up)
            Vector3 vertLook = rotation.y * time * Vector3.left * lookSpeed;
            Quaternion cameraLookRotation = localRotation * Quaternion.Euler(vertLook);

            // We have to flip the signs and positions of min/max view angle here because the math
            // uses the contradictory interpretation of our angles (+/- is down/up).
            float maxViewAngle = ReduxityInitializer.Instance.maxViewAngle;
            float minViewAngle = ReduxityInitializer.Instance.minViewAngle;
            Quaternion clampedRotation = ClampRotationAroundXAxis_(cameraLookRotation, -maxViewAngle, -minViewAngle);

            state.isLooking = true;
            state.localRotation = clampedRotation;
            Debug.Log($"in Look, returning state: {ObjectDumper.Dump(state)}");
            return state;
        }

        // stop look
        public static CameraState StopLook(CameraState state, Action.StopLook action) {
            state.isLooking = false;
            Debug.Log($"in StopLook, returning state: {ObjectDumper.Dump(state)}");
            return state;
        }

        // Ripped straight out of the Standard Assets MouseLook script
		public static Quaternion ClampRotationAroundXAxis_(Quaternion q, float minAngle, float maxAngle) {
			q.x /= q.w;
			q.y /= q.w;
			q.z /= q.w;
			q.w = 1.0f;

			float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

			angleX = Mathf.Clamp(angleX, minAngle, maxAngle);

			q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

			return q;
		}
    }
}
