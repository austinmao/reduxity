using UnityEngine;
using UniRx;
using Zenject;

namespace Reduxity.Example.Zenject {

    public class MoveCamera : IInitializable {

        App app_;
        Camera camera_; // bound through ZenjectBinding script on GameObject

        public MoveCamera(App app, Camera camera) {
            app_ = app;
            camera_ = camera;
        }

        public void Initialize() {
            RenderLook();
        }

        void RenderLook() {
            // Debug.Log($"App.Store: {App.Store}");
            app_.Store
                .Subscribe(state => {
                    Debug.Log($"looking by rotation: {state.Camera.localRotation}");
                    camera_.transform.localRotation = state.Camera.localRotation;
                })
                .AddTo(camera_);
        }
    }
}
