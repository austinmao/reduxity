using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Reduxity {
    public class CounterButton : MonoBehaviour {
        void Start() {
            observeAndDispatchButtonPressed();
        }

    void observeAndDispatchButtonPressed() {
            App.Store.Dispatch(
                // dispatch move action
                new Counter.Action.Increment {}
            );
            // this.FixedUpdateAsObservable()
            //     // get inputs by axis
            //     .Select(_ => {
            //         var x = Input.GetAxis("Horizontal");
            //         var y = Input.GetAxis("Vertical");
            //         return new Vector2(x, y).normalized;
            //     })
            //     // performance optimization: only dispatch if non-zero movement
            //     .Where(v => v != Vector2.zero)
            //     // dispatch inputVelocity and GameObject transform to move reducer
            //     .Subscribe(inputVelocity => 
            //         // Debug.Log($"going to dispatch. inputVelocity: {inputVelocity}, playerTransform: {transform}");
            //         App.Store.Dispatch(
            //             // dispatch move action
            //             new CharacterMover.Action.Move {
            //                 inputVelocity = inputVelocity,
            //                 playerTransform = transform
            //             }
            //         )
            //     )
            //     // dispose of the observable if GameObject is disposed
            //     .AddTo(this);
        }
    }
}
