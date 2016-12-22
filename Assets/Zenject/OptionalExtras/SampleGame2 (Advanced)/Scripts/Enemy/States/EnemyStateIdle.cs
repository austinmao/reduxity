using System;
using UnityEngine;
using System.Linq;

namespace Zenject.SpaceFighter
{
    public class EnemyStateIdle : IEnemyState
    {
        readonly EnemyGlobalTunables _globalTunables;
        readonly PlayerFacade _player;
        readonly EnemyRegistry _registry;
        readonly EnemyStateManager _stateManager;
        readonly Settings _settings;
        readonly EnemyModel _model;

        Vector3 _startPos;
        float _theta;
        Vector3 _startLookDir;

        public EnemyStateIdle(
            EnemyModel model, Settings settings,
            EnemyStateManager stateManager,
            EnemyRegistry registry,
            PlayerFacade player,
            EnemyGlobalTunables globalTunables)
        {
            _globalTunables = globalTunables;
            _player = player;
            _registry = registry;
            _stateManager = stateManager;
            _settings = settings;
            _model = model;
        }

        public void Initialize()
        {
            _startPos = _model.Position;
            _theta = 0;
            _startLookDir = _model.LookDir;
        }

        public void Dispose()
        {
        }

        // Just add a bit of subtle movement
        public void Update()
        {
            _theta += Time.deltaTime * _settings.Frequency;

            _model.Position = _startPos + _model.RightDir * _settings.Amplitude * Mathf.Sin(_theta);

            if (_player.IsDead)
            {
                _model.DesiredLookDir = _startLookDir;
            }
            else
            {
                // look away from player
                _model.DesiredLookDir = -(_player.Position - _model.Position).normalized;
            }

            if (!_player.IsDead)
            {
                if (_registry.Enemies.Where(x => x.IsAttacking || x.IsChasing).Count() < _globalTunables.NumAttacking)
                {
                    _stateManager.ChangeState(EnemyStates.Follow);
                }
                else if ((_player.Position - _model.Position).magnitude < _settings.AttackDistance)
                {
                    _stateManager.ChangeState(EnemyStates.Attack);
                }
            }
        }

        public void FixedUpdate()
        {
        }

        [Serializable]
        public class Settings
        {
            public float Amplitude;
            public float Frequency;
            public float AttackDistance;
            public float WaitTime;
        }
    }
}
