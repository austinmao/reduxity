using UnityEngine;
using UniRx;

namespace Reduxity {
    public class MoveCharacter : MonoBehaviour {
        public CharacterController character;

        void Start() {
            renderMove();
        }

        void renderMove() {
            // Debug.Log($"App.Store: {App.Store}");
            App.Store.Select(CharacterMoverSelector.GetMoveDistance)
                .Subscribe(distance => {
                    // Debug.Log($"going to move character by: {distance}");
                    character.Move(distance);
                })
                .AddTo(this);
        }
    }
}
