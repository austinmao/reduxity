using UnityEngine;
using UniRx;
using Zenject;
using System;

namespace Reduxity.Example.Zenject {

    public class MoveCharacter : IInitializable {

        App app_;
        CharacterController character_; // bound through ZenjectBinding script on GameObject

        public MoveCharacter(App app, CharacterController character) {
            app_ = app;
            character_ = character;
        }

        public void Initialize() {
            renderMove();
            renderTurn();
        }

        void renderMove() {
            // Debug.Log($"App.Store: {App.Store}");
            app_.Store
                .Select(CharacterMoverSelector.GetMoveDistance)
                .Subscribe(distance => {
                    // Debug.Log($"going to move character by: {distance}");
                    character_.Move(distance);
                })
                .AddTo(character_);
        }

        void renderTurn() {
            app_.Store
                .Select(CharacterMoverSelector.GetTurnRotation)
                .Subscribe(rotation => {
                    Debug.Log($"going to turn character by: {rotation}");
                    character_.transform.localRotation = rotation;
                })
                .AddTo(character_);
        }

        [Serializable]
        public class Settings {
        }
    }
}
