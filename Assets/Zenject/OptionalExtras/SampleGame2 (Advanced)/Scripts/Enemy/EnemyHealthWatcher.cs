using UnityEngine;
using Zenject;

namespace Zenject.SpaceFighter
{
    public class EnemyHealthWatcher : ITickable
    {
        readonly EnemyKilledSignal _killedSignal;
        readonly Explosion.Factory _explosionFactory;
        readonly Context _context;
        readonly EnemyModel _model;

        public EnemyHealthWatcher(
            EnemyModel model,
            Context context,
            Explosion.Factory explosionFactory,
            EnemyKilledSignal killedSignal)
        {
            _killedSignal = killedSignal;
            _explosionFactory = explosionFactory;
            _context = context;
            _model = model;
        }

        public void Tick()
        {
            if (_model.Health <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            var explosion = _explosionFactory.Create();
            explosion.transform.position = _model.Position;

            GameObject.Destroy(_context.gameObject);

            _killedSignal.Fire();
        }
    }
}
