using UnityEngine;
using Zenject;
using System;

namespace Reduxity.Example.Zenject {

    /// <summary>
    /// Nested State node that governs Character movement.
    /// </summary>
    [Serializable]
    public class CharacterState : IState {
        public bool isMoving { get; set; }
        public bool isTurning { get; set; }
        public Vector3 moveDistance { get; set; } // how much to move character
        // public Transform transform { get; set; }
        public Vector3 transformForward { get; set; }
        public Vector3 transformRight { get; set; }
        public Quaternion localRotation { get; set; }
    }

    public class CharacterStateInitializer : IStateInitializer, IInitializable {
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
        /// Initialize state with GameObject data or default state if no GameObjects are bound
        /// </summary>
        public void Initialize() {
            var transform_ = (character_ != null && character_.transform != null) ? character_.transform : null;
            if (!transform_) {
                Debug.LogWarning("Character transform cannot be found. Did you bind the object?");
            }

            characterState_.isMoving = false;
            characterState_.isTurning = false;
            characterState_.moveDistance = Vector3.zero;
            characterState_.transformForward = (transform_ != null) ? transform_.forward : Vector3.forward;
            characterState_.transformRight = (transform_ != null) ? transform_.right : Vector3.right;
            characterState_.localRotation = (transform_ != null) ? transform_.localRotation : Quaternion.identity;
        }
    }
}
