using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Zenject.SpaceFighter
{
    public class EnemyStateAttack : IEnemyState
    {
        readonly EnemyTunables _tunables;
        readonly EnemyStateManager _stateManager;
        readonly PlayerFacade _player;
        readonly Settings _settings;
        readonly EnemyModel _model;
        readonly Bullet.Factory _bulletFactory;

        float _lastShootTime;
        bool _strafeRight;
        float _lastStrafeChangeTime;

        public EnemyStateAttack(
            Bullet.Factory bulletFactory,
            EnemyModel model,
            Settings settings,
            PlayerFacade player,
            EnemyStateManager stateManager,
            EnemyTunables tunables)
        {
            _tunables = tunables;
            _stateManager = stateManager;
            _player = player;
            _settings = settings;
            _model = model;
            _bulletFactory = bulletFactory;
            _strafeRight = Random.Range(0.0f, 1.0f) < 0.5f;
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

            _model.DesiredLookDir = (_player.Position - _model.Position).normalized;

            // Strafe back and forth over the given interval
            if (Time.realtimeSinceStartup - _lastStrafeChangeTime > _settings.StrafeChangeInterval)
            {
                _lastStrafeChangeTime = Time.realtimeSinceStartup;
                _strafeRight = !_strafeRight;
            }

            // Shoot every X seconds
            if (Time.realtimeSinceStartup - _lastShootTime > _settings.ShootInterval)
            {
                _lastShootTime = Time.realtimeSinceStartup;
                Fire();
            }

            // If the player runs away then chase them
            if ((_player.Position - _model.Position).magnitude > _tunables.AttackDistance + _settings.AttackRangeBuffer)
            {
                _stateManager.ChangeState(EnemyStates.Follow);
            }
        }

        public void FixedUpdate()
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

        void Fire()
        {
            var bullet = _bulletFactory.Create(
                _settings.BulletSpeed, _settings.BulletLifetime, BulletTypes.FromEnemy);

            // Randomize our aim a bit
            var accuracy = Mathf.Clamp(_tunables.Accuracy, 0, 1);
            var maxError = 1.0f - accuracy;
            var error = Random.Range(0, maxError);

            if (Random.Range(0.0f, 1.0f) < 0.5f)
            {
                error *= -1;
            }

            var thetaError = error * _settings.ErrorRangeTheta;

            bullet.transform.position = _model.Position + _model.LookDir * _settings.BulletOffsetDistance;
            bullet.transform.rotation = Quaternion.AngleAxis(thetaError, Vector3.forward) * _model.Rotation;
        }

        [Serializable]
        public class Settings
        {
            public float BulletLifetime;
            public float BulletSpeed;
            public float BulletOffsetDistance;
            public float ShootInterval;
            public float ErrorRangeTheta;
            public float AttackRangeBuffer;
            public float StrafeMultiplier;
            public float StrafeChangeInterval;
        }
    }
}
