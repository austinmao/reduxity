using UnityEngine;
using UniRx;
using Zenject;
using System;
using ModestTree;

namespace Reduxity.Example.Zenject {

    public class MoveCharacter : IInitializable {

        readonly App app_;
        readonly CharacterController character_;

        public MoveCharacter(
            App app,
            CharacterController character // via ZenjectBinding in editor
        ) {
            app_ = app;
            character_ = character;
        }

        public void Initialize() {
            // Debug.Log($"In MoveCharacter.Initialize, App.Store: {app_.Store}");
            renderMove();
            renderTurn();
        }

        void renderMove() {
            app_.Store
                .Where(state => state.Character.isMoving == true)
                .Subscribe(state => {
                    // Assert.That(state.Character.isMoving == true);
                    // Assert.That(state.Character.isTurning == false);
                    Debug.Log($"in renderMove, isMoving: {state.Character.isMoving}, isTurning: {state.Character.isTurning}");
                    // Debug.Log($"going to move character by: {state.Character.moveDistance}");
                    character_.Move(state.Character.moveDistance);
                })
                // .Select(CharacterMoverSelector.GetMoveDistance)
                // .Subscribe(distance => {
                //     Debug.Log($"going to move character by: {distance}");
                //     character_.Move(distance);
                // })
                .AddTo(character_);
        }

        void renderTurn() {
            app_.Store
                .Where(state => state.Character.isTurning == true)
                // .Select(CharacterMoverSelector.GetTurnRotation)
                .Subscribe(state => {
                    // Assert.That(state.Character.isTurning == true);
                    // Assert.That(state.Character.isMoving == false);
                    Debug.Log($"in renderTurn, isTurning: {state.Character.isTurning}, isMoving: {state.Character.isMoving}");
                    // Debug.Log($"going to turn character by: {state.Character.localRotation}");
                    character_.transform.localRotation = state.Character.localRotation;
                })
                // .Subscribe(rotation => {
                //     Debug.Log($"going to turn character by: {rotation}");
                //     character_.transform.localRotation = rotation;
                // })
                .AddTo(character_);
        }

        [Serializable]
        public class Settings {
        }
    }
}
