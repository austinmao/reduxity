using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {

    /// <summary>
    /// Nested State node that governs Camera transform.
    /// </summary>
    public class CameraState : IState {
        public bool isLooking { get; set; }
        public Quaternion localRotation { get; set; }
    }

    /// <summary>
    /// Set up default state. This is done in a separate class to make state testable without dependencies.
    /// </summary>
    public class CameraStateInitializer : IInitializable {
        readonly CameraState cameraState_;
        readonly Camera camera_;

        /// <summary>
        /// Camera state constructor
        /// </summary>
        /// <param name="camera">Camera GameObject injected via editor</param>
        public CameraStateInitializer(CameraState cameraState, Camera camera) {
            cameraState_ = cameraState;
            camera_ = camera;
        }

        public void Initialize() {
            cameraState_.isLooking = false;
            cameraState_.localRotation = camera_.transform.localRotation;
        }
    }
}
