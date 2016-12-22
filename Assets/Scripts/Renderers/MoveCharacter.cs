using UnityEngine;
using UniRx;

namespace CharacterMover {
    public class MoveCharacter : MonoBehaviour {
        public CharacterController character;

        void Start() {
            renderMove();
        }

        void renderMove() {
            Debug.Log($"App.Store: {App.Store}");
            App.Store.Select(CharacterMoverSelector.GetMoveDistance)
                // .Subscribe(store => {
                //     Vector3 distance = store.Move.distance;
                //     Debug.Log($"going to dispatch {distance}");
                //     character.Move(distance);
                // })
                .Subscribe(distance => {
                    Debug.Log($"going to move character by: {distance}");
                    character.Move(distance);
                })
                .AddTo(this);
        }
    }
}
