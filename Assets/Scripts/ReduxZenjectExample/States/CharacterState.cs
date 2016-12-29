using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {

    /// <summary>
    /// Nested State node that governs Character movement.
    /// </summary>
    public class CharacterState : IState, IInitializable {
        readonly CharacterController character_;
        public bool isMoving { get; set; }
        public bool isTurning { get; set; }
        public Vector3 moveDistance { get; set; } // how much to move character
        public Transform transform { get; set; }

        /// <summary>
        /// Character state constructor
        /// </summary>
        /// <param name="character">CharacterController GameObject injected via editor</param>
        public CharacterState(CharacterController character) {
            character_ = character;
        }

        /// <summary>
        /// Initiale state
        /// </summary>
        public void Initialize() {
            isMoving = false;
            isTurning = false;
            moveDistance = Vector3.zero;
            transform = character_.transform;
        }
    }
}
