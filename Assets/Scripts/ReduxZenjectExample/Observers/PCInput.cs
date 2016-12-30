using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;

namespace Reduxity.Example.Zenject {
    public class PCInput : MonoBehaviour {

        App app_;

        [Inject]
        public void Construct(App app) {
            app_ = app;
        }

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
                    app_.Store.Dispatch(
                        // dispatch move action
                        new CharacterMover.Action.Move {
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
                    Debug.Log($"going to dispatch Action.Look");
                    app_.Store.Dispatch(
                        new CharacterLook.Action.Look {
                            inputRotation = inputRotation,
                            fixedDeltaTime = Time.fixedDeltaTime
                        }
                    );

                    // TODO: get this to work
                    // turn character along y-axis (left and right)
                    Debug.Log($"going to dispatch Action.Turn");
                    app_.Store.Dispatch(
                        new CharacterMover.Action.Turn {
                            inputRotation = inputRotation,
                            fixedDeltaTime = Time.fixedDeltaTime
                        }
                    );
				}).AddTo(this);
        }
    }
}
