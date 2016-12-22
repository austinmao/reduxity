using System;
using ModestTree;
using UnityEngine;

namespace Zenject.SpaceFighter
{
    public class EnemyStateFollow : IEnemyState
    {
        readonly Settings _settings;
        readonly EnemyTunables _tunables;
        readonly EnemyStateManager _stateManager;
        readonly EnemyModel _model;
        readonly PlayerFacade _player;

        bool _strafeRight;
        float _lastStrafeChangeTime;

        public EnemyStateFollow(
            PlayerFacade player,
            EnemyModel model,
            EnemyStateManager stateManager,
            EnemyTunables tunables,
            Settings settings)
        {
            _settings = settings;
            _tunables = tunables;
            _stateManager = stateManager;
            _model = model;
            _player = player;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }

        public void Update()
        {
            if (_player.IsDead)
            {
                _stateManager.ChangeState(EnemyStates.Idle);
                return;
            }

            var distanceToPlayer = (_player.Position - _model.Position).magnitude;

            Assert.That(_settings.TeleportDistance > _settings.TeleportNewDistance);

            // If they are far enough away, just teleport them to the other side of the player
            // This is good because otherwise the best strategy is just to keep running away and shooting
            // and it gets boring
            if (distanceToPlayer > _settings.TeleportDistance)
            {
                var playerDir = (_player.Position - _model.Position).normalized;
                _model.Position = _player.Position + playerDir * _settings.TeleportNewDistance;
            }

            // Always look towards the player
            _model.DesiredLookDir = (_player.Position - _model.Position).normalized;

            // Strafe back and forth over the given interval
            // This helps avoiding being too easy a target
            if (Time.realtimeSinceStartup - _lastStrafeChangeTime > _settings.StrafeChangeInterval)
            {
                _lastStrafeChangeTime = Time.realtimeSinceStartup;
                _strafeRight = !_strafeRight;
            }

            if (distanceToPlayer < _tunables.AttackDistance)
            {
                _stateManager.ChangeState(EnemyStates.Attack);
            }
        }

        public void FixedUpdate()
        {
            MoveTowardsPlayer();
            Strafe();
        }

        void Strafe()
        {
            // Strafe to avoid getting hit too easily
            if (_strafeRight)
            {
                _model.AddForce(_model.RightDir * _settings.StrafeMultiplier * _model.MoveSpeed);
            }
            else
            {
                _model.AddForce(-_model.RightDir * _settings.StrafeMultiplier * _model.MoveSpeed);
            }
        }

        void MoveTowardsPlayer()
        {
            var playerDir = (_player.Position - _model.Position).normalized;

            _model.AddForce(playerDir * _model.MoveSpeed);
        }

        [Serializable]
        public class Settings
        {
            public float StrafeMultiplier;
            public float StrafeChangeInterval;
            public float TeleportDistance;
            public float TeleportNewDistance;
        }
    }
}

