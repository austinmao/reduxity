using UnityEngine;
using UniRx;
using Zenject;
using System;
using ModestTree;

namespace Reduxity.Example.Zenject {

    public class MoveCharacter : IInitializable, IComponent {

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
            RenderMove();
            RenderTurn();
        }

        void RenderMove() {
            app_.Store
                .Where(state => state.Character.isMoving)
                .Select(CharacterMoverSelector.GetMoveDistance)
                .Subscribe(distance => {
                    // Debug.Log($"going to move character by: {distance}");
                    character_.Move(distance);
                })
                .AddTo(character_);
        }

        void RenderTurn() {
            app_.Store
                .Where(state => state.Character.isTurning)
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
