using System.Collections.Generic;
using ModestTree.Util;
using UnityEngine;
using Zenject;
using System.Linq;
using ModestTree;

namespace Zenject.SpaceFighter
{
    public enum EnemyStates
    {
        None,
        Idle,
        RunAway,
        Attack,
        Follow,
        Dead,
    }

    public class EnemyStateFactory : IValidatable
    {
        readonly DiContainer _container;

        public EnemyStateFactory(DiContainer container)
        {
            _container = container;
        }

        public IEnemyState Create(EnemyStates state)
        {
            switch (state)
            {
                case EnemyStates.Idle:
                {
                    return _container.Instantiate<EnemyStateIdle>();
                }
                case EnemyStates.RunAway:
                {
                    return _container.Instantiate<EnemyStateRunAway>();
                }
                case EnemyStates.Attack:
                {
                    return _container.Instantiate<EnemyStateAttack>();
                }
                case EnemyStates.Follow:
                {
                    return _container.Instantiate<EnemyStateFollow>();
                }
            }

            throw Assert.CreateException();
        }

        // If we were using a factory then we wouldn't need to do this
        // However, since we are instantiating these classes directly
        // we need to (if we care enough about validating the state classes)
        public void Validate()
        {
            // Validation is only called in unity editor when user presses CTRL+SHIFT+V
            Assert.That(Application.isEditor);

            foreach (var state in new [] { EnemyStates.Idle, EnemyStates.RunAway, EnemyStates.Attack, EnemyStates.Follow })
            {
                Create(state);
            }
        }
    }
}
