using UnityEngine;
using UniRx;

namespace Reduxity.Example.PlayerMovementLook {

    [RequireComponent(typeof(CharacterController))]
    public class MoveCharacter : MonoBehaviour {

        private CharacterController character_;

        private void Awake() {
            character_ = GetComponent<CharacterController>();
        }

        void Start() {
            renderMove();
            renderTurn();
        }

        void renderMove() {
            // Debug.Log($"App.Store: {App.Store}");
            App.Store
                .Where(state => state.Character.isMoving)
                .Select(CharacterMoverSelector.GetMoveDistance)
                .Subscribe(distance => {
                    // Debug.Log($"going to move character by: {distance}");
                    character_.Move(distance);
                })
                .AddTo(this);
        }

        void renderTurn() {
            App.Store
                .Where(state => state.Character.isTurning)
                .Select(CharacterMoverSelector.GetTurnRotation)
                .Subscribe(rotation => {
                    Debug.Log($"going to turn character by: {rotation}");
                    character_.transform.localRotation = rotation;
                })
                .AddTo(this);
        }
    }
}
