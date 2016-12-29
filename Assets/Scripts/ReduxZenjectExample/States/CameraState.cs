using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {

    /// <summary>
    /// Nested State node that governs Camera transform.
    /// </summary>
    public class CameraState : IState, IInitializable {
        readonly Camera camera_;

        public bool isLooking { get; set; }
        public Quaternion localRotation { get; set; }

        /// <summary>
        /// Camera state constructor
        /// </summary>
        /// <param name="camera">Camera GameObject injected via editor</param>
        public CameraState(Camera camera) {
            camera_ = camera;
        }

        public void Initialize() {
            isLooking = false;
            localRotation = camera_.transform.localRotation;
        }
    }
}
