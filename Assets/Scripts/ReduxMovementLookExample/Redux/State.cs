using UnityEngine;

namespace Reduxity.Example.PlayerMovementLook {
    public class State : IState<State> {
        public Character Character { get; set; }
        private Settings settings_; // TODO: Inject

        /* default state at app start-up */
        public State Initialize() {
            settings_ = new Settings();

            return new State {
                Character = new Character {
                    isMoving = false,
                    isLooking = false,
                    moveDistance = Vector3.zero,
                    lookRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up),
                    playerTransform = settings_.Player.transform, // TODO: Inject this
                    cameraTransform = settings_.Camera.transform // TODO: Inject this
                }
            };
        }
    }

    public class Character {
        public bool isMoving { get; set; }
        public bool isLooking { get; set; }
        public Vector3 moveDistance { get; set; }
        public Quaternion lookRotation { get; set; }
        public Transform playerTransform { get; set; }
        public Transform cameraTransform { get; set; }
    }
}
