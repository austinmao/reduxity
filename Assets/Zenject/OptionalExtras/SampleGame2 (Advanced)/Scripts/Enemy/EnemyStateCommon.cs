using System;
using UnityEngine;
using Zenject;

namespace Zenject.SpaceFighter
{
    // Any behaviour that is common regardless of what state the enemy is in
    // goes here
    public class EnemyStateCommon : IInitializable, IDisposable, ITickable
    {
        readonly EnemyModel _model;
        readonly EnemyStateManager _stateManager;

        EnemySignals.Hit _hitSignal;

        public EnemyStateCommon(
            EnemySignals.Hit hitSignal,
            EnemyStateManager stateManager,
            EnemyModel model)
        {
            _model = model;
            _stateManager = stateManager;
            _hitSignal = hitSignal;
        }

        public void Initialize()
        {
            _hitSignal += OnHit;
        }

        public void Dispose()
        {
            _hitSignal -= OnHit;
        }

        public void Tick()
        {
            // Always ensure we are on the main plane
            _model.Position = new Vector3(_model.Position.x, _model.Position.y, 0);
        }

        void OnHit(Bullet bullet)
        {
            // Run away to fight another day
            _stateManager.ChangeState(EnemyStates.RunAway);
        }
    }
}

