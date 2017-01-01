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
            renderMove();
            renderTurn();
        }

        void renderMove() {
            app_.Store
                .Where(state => state.Character.isMoving == true)
                .Select(CharacterMoverSelector.GetMoveDistance)
                .Subscribe(distance => {
                    // Debug.Log($"going to move character by: {distance}");
                    character_.Move(distance);
                })
                .AddTo(character_);
        }

        void renderTurn() {
            app_.Store
                .Where(state => state.Character.isTurning == true)
                .Select(CharacterMoverSelector.GetTurnRotation)
                .Subscribe(rotation => {
                    // Debug.Log($"going to turn character by: {rotation}");
                    character_.transform.localRotation = rotation;
                })
                .AddTo(character_);
        }

        [Serializable]
        public class Settings {
        }
    }
}
