using UnityEngine;
using UniRx;

namespace Reduxity.Example.PlayerMovementLook {

    [RequireComponent(typeof(Camera))]
    public class MoveCamera : MonoBehaviour {

        private Camera camera_;

        private void Awake() {
            camera_ = GetComponent<Camera>();
        }

        void Start() {
            RenderLook();
        }

        void RenderLook() {
            // Debug.Log($"App.Store: {App.Store}");
            App.Store
                .Subscribe(state => {
                    // Debug.Log($"going to move character by: {distance}");
                    // camera_.transform.localRotation = state.Look.lookRotation;
                })
                .AddTo(this);
        }

        // Ripped straight out of the Standard Assets MouseLook script. (This should really be a standard function...)
		private Quaternion ClampRotationAroundXAxis(Quaternion q, float minAngle, float maxAngle) {
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
