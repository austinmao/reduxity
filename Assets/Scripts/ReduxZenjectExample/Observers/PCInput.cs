using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;

namespace Reduxity.Example.Zenject {
    public class PCInput : IInitializable {

        readonly App app_;
        readonly Transform player_;

        public PCInput(
            App app,
			[Inject(Id = "Player")]
            Transform player
        ) {
            app_ = app;
            player_ = player;
        }

        public void Initialize() {
            observeMovement();
            observeLook();
        }

        // reference: https://ornithoptergames.com/reactiverx-in-unity3d-part-1/
        IObservable<Vector2> observeKeyInput() {
            return player_.FixedUpdateAsObservable()
                // get inputs by key press
                .Select(_ => {
                    var x = Input.GetAxis("Horizontal");
                    var y = Input.GetAxis("Vertical");
                    return new Vector2(x, y).normalized;
                });
        }

        void observeMovement() {
            observeKeyInput()
                // performance optimization: only dispatch if non-zero movement
                .Where(v => v != Vector2.zero)
                // dispatch inputVelocity and GameObject transform to move reducer
                .Subscribe(inputVelocity => {
                    // Debug.Log($"going to dispatch CharacterMover.Action.Move");
                    app_.Store.Dispatch(
                        // dispatch move action
                        new CharacterMover.Action.Move {
                            inputVelocity = inputVelocity,
                            fixedDeltaTime = Time.fixedDeltaTime
                        }
                    );
                })
                // dispose of the observable if GameObject is disposed
                .AddTo(player_);
        }

        IObservable<Vector2> observeMouseInput() {
            return player_.UpdateAsObservable()
				.Select(_ => {
                    // get inputs by axis
					var x = Input.GetAxis("Mouse X");
					var y = Input.GetAxis("Mouse Y");
					return new Vector2(x, y);
				});
        }

        void observeLook() {
            observeMouseInput()
                // ignore if mouse look is zero
				.Where(v => v != Vector2.zero)
				.Subscribe(inputRotation => {
                    // rotate camera along x-axis (up and down)
                    // Debug.Log($"going to dispatch Action.Look");
                    app_.Store.Dispatch(
                        new CameraLook.Action.Look {
                            inputRotation = inputRotation,
                            fixedDeltaTime = Time.fixedDeltaTime
                        }
                    );

                    // turn character along y-axis (left and right)
                    // Debug.Log($"going to dispatch Action.Turn");
                    app_.Store.Dispatch(
                        new CharacterMover.Action.Turn {
                            inputRotation = inputRotation,
                            fixedDeltaTime = Time.fixedDeltaTime
                        }
                    );
				})
                .AddTo(player_);
        }
    }
}
