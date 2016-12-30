using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {

    /// <summary>
    /// Nested State node that governs Character movement.
    /// </summary>
    public class CharacterState : IState {
        public bool isMoving { get; set; }
        public bool isTurning { get; set; }
        public Vector3 moveDistance { get; set; } // how much to move character
        public Transform transform { get; set; }
    }

    public class CharacterStateInitializer : IInitializable {
        readonly CharacterState characterState_;
        readonly CharacterController character_;

        /// <summary>
        /// Character state constructor
        /// </summary>
        /// <param name="character">CharacterController GameObject injected via editor</param>
        public CharacterStateInitializer(CharacterState characterState, CharacterController character) {
            characterState_ = characterState;
            character_ = character;
        }

        /// <summary>
        /// Initiale state
        /// </summary>
        public void Initialize() {
            characterState_.isMoving = false;
            characterState_.isTurning = false;
            characterState_.moveDistance = Vector3.zero;
            characterState_.transform = character_.transform;
        }
    }
}
