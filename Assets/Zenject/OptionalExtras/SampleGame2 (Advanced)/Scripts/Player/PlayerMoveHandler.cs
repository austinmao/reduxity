using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Zenject.SpaceFighter
{
    public class PlayerMoveHandler : IFixedTickable
    {
        readonly Settings _settings;
        readonly PlayerModel _player;
        readonly PlayerInputState _inputState;

        public PlayerMoveHandler(
            PlayerInputState inputState,
            PlayerModel player,
            Settings settings)
        {
            _settings = settings;
            _player = player;
            _inputState = inputState;
        }

        public void FixedTick()
        {
            if (_player.IsDead)
            {
                return;
            }

            if (_inputState.IsMovingLeft)
            {
                _player.AddForce(
                    Vector3.left * _settings.MoveSpeed);
            }

            if (_inputState.IsMovingRight)
            {
                _player.AddForce(
                    Vector3.right * _settings.MoveSpeed);
            }

            if (_inputState.IsMovingUp)
            {
                _player.AddForce(
                    Vector3.up * _settings.MoveSpeed);
            }

            if (_inputState.IsMovingDown)
            {
                _player.AddForce(
                    Vector3.down * _settings.MoveSpeed);
            }

            // Always ensure we are on the main plane
            _player.Position = new Vector3(_player.Position.x, _player.Position.y, 0);
        }

        [Serializable]
        public class Settings
        {
            public float MoveSpeed;
            public float SlowDownSpeed;
        }
    }
}
