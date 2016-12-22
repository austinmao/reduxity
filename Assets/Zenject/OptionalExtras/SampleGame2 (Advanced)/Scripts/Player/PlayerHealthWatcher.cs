using System;
using UnityEngine;
using Zenject;

namespace Zenject.SpaceFighter
{
    public class PlayerHealthWatcher : ITickable
    {
        readonly PlayerKilledSignal _killedSignal;
        readonly Explosion.Factory _explosionFactory;
        readonly PlayerModel _model;

        public PlayerHealthWatcher(
            PlayerModel model,
            Explosion.Factory explosionFactory,
            PlayerKilledSignal killedSignal)
        {
            _killedSignal = killedSignal;
            _explosionFactory = explosionFactory;
            _model = model;
        }

        public void Tick()
        {
            if (_model.Health <= 0 && !_model.IsDead)
            {
                Die();
            }
        }

        void Die()
        {
            _model.IsDead = true;

            var explosion = _explosionFactory.Create();
            explosion.transform.position = _model.Position;

            _model.Renderer.enabled = false;

            _killedSignal.Fire();
        }
    }
}
