using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Reduxity.Example.PlayerMovement {
    public class PCInput : MonoBehaviour {
        void Start() {
            observeKeyInput();
        }

        // reference: https://ornithoptergames.com/reactiverx-in-unity3d-part-1/
        void observeKeyInput() {
            this.FixedUpdateAsObservable()
                // get inputs by axis
                .Select(_ => {
                    var x = Input.GetAxis("Horizontal");
                    var y = Input.GetAxis("Vertical");
                    return new Vector2(x, y).normalized;
                })
                // performance optimization: only dispatch if non-zero movement
                .Where(v => v != Vector2.zero)
                // dispatch inputVelocity and GameObject transform to move reducer
                .Subscribe(inputVelocity => 
                    App.Store.Dispatch(
                        // dispatch move action
                        new Action.Move {
                            inputVelocity = inputVelocity,
                            playerTransform = transform,
                            fixedDeltaTime = Time.fixedDeltaTime
                        }
                    )
                )
                // dispose of the observable if GameObject is disposed
                .AddTo(this);
        }
    }
}
