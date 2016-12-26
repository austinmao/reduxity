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
                    Debug.Log($"looking by rotation: {state.Camera.localRotation}");
                    camera_.transform.localRotation = state.Camera.localRotation;
                })
                .AddTo(this);
        }
    }
}
