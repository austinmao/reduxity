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
    public class CameraStateInitializer : IStateInitializer, IInitializable {
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
            var transform_ = (camera_ != null && camera_.transform != null) ? camera_.transform : null;
            if (!transform_) {
                Debug.LogWarning("Character transform cannot be found. Did you bind the object?");
            }

            cameraState_.isLooking = false;
            // set default localRotation if GameObject is not initialized (null)
            cameraState_.localRotation = (transform_ != null) ? transform_.localRotation : Quaternion.identity;
        }
    }
}
