using UnityEngine;
using UniRx;
using Zenject;
using System;
using ModestTree;

namespace Reduxity.Example.Zenject {

    public class MoveCharacter : IInitializable, IComponent {

        readonly App app_;
        readonly CharacterController character_;
        readonly CharacterMoverSelector selector_;

        public MoveCharacter(
            App app,
            CharacterController character, // via ZenjectBinding in editor
            CharacterMoverSelector selector
        ) {
            app_ = app;
            character_ = character;
            selector_ = selector;
        }

        public void Initialize() {
            RenderMove();
            RenderTurn();
        }

        void RenderMove() {
            app_.Store
                .Where(state => state.Character.isMoving)
                .Select(selector_.GetMoveDistance)
                .Subscribe(distance => {
                    // Debug.Log($"going to move character by: {distance}");
                    character_.Move(distance);
                })
                .AddTo(character_);
        }

        void RenderTurn() {
            app_.Store
                .Where(state => state.Character.isTurning)
                .Select(selector_.GetTurnRotation)
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
