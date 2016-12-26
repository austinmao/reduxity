using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Reduxity.Example.PlayerMovementLook {
    public class PCInput : MonoBehaviour {

        void Start() {
            dispatchMovement();
            dispatchLook();
        }

        // reference: https://ornithoptergames.com/reactiverx-in-unity3d-part-1/
        IObservable<Vector2> observeKeyInput() {
            return this.FixedUpdateAsObservable()
                // get inputs by key press
                .Select(_ => {
                    var x = Input.GetAxis("Horizontal");
                    var y = Input.GetAxis("Vertical");
                    return new Vector2(x, y).normalized;
                });
        }

        void dispatchMovement() {
            observeKeyInput()
                // performance optimization: only dispatch if non-zero movement
                .Where(v => v != Vector2.zero)
                // dispatch inputVelocity and GameObject transform to move reducer
                .Subscribe(inputVelocity => 
                    App.Store.Dispatch(
                        // dispatch move action
                        new Movement.Action.Move {
                            inputVelocity = inputVelocity,
                            fixedDeltaTime = Time.fixedDeltaTime
                        }
                    )
                )
                // dispose of the observable if GameObject is disposed
                .AddTo(this);
        }

        IObservable<Vector2> observeMouseInput() {
            return this.UpdateAsObservable()
				.Select(_ => {
                    // get inputs by axis
					var x = Input.GetAxis("Mouse X");
					var y = Input.GetAxis("Mouse Y");
					return new Vector2(x, y);
				});
        }

        void dispatchLook() {
            observeMouseInput()
                // ignore if mouse look is zero
				.Where(v => v != Vector2.zero)
				.Subscribe(inputRotation => {
                    // rotate camera along x-axis (up and down)
                    App.Store.Dispatch(
                        new Look.Action.Look {
                            inputRotation = inputRotation,
                            fixedDeltaTime = Time.fixedDeltaTime
                        }
                    );

                    // TODO: get this to work
                    // turn character along y-axis (left and right)
                    App.Store.Dispatch(
                        new Movement.Action.Turn {
                            inputRotation = inputRotation,
                            fixedDeltaTime = Time.fixedDeltaTime
                        }
                    );
				}).AddTo(this);
        }
    }
}
