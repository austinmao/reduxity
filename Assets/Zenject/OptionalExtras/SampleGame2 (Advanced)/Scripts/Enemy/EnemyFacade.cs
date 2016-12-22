using System;
using UnityEngine;
using Zenject;

namespace Zenject.SpaceFighter
{
    public class EnemyFacade : MonoBehaviour
    {
        EnemyModel _model;
        EnemyRegistry _enemyRegistry;
        EnemyStateManager _stateManager;

        // We can't use a constructor here because MonoFacade derives from
        // MonoBehaviour
        [Inject]
        public void Construct(
            EnemyModel model, EnemyRegistry registry, EnemyStateManager stateManager)
        {
            _model = model;
            _enemyRegistry = registry;
            _stateManager = stateManager;

            registry.AddEnemy(this);
        }

        // Here we can add some high-level methods to give some info to other
        // parts of the codebase outside of our enemy facade
        public bool IsAttacking
        {
            get
            {
                return _stateManager.CurrentState == EnemyStates.Attack;
            }
        }

        public bool IsChasing
        {
            get
            {
                return _stateManager.CurrentState == EnemyStates.Follow;
            }
        }

        public Vector3 Position
        {
            get
            {
                return _model.Position;
            }
            set
            {
                _model.Position = value;
            }
        }

        public void OnDestroy()
        {
            _enemyRegistry.RemoveEnemy(this);
        }

        // Here we declare a parameter to our facade factory of type EnemyTunables
        // Note that unlike for normal factories, this parameter gets injected into
        // an installer instead of the EnemyFacade class itself
        // It's done this way because in some cases we want to add the arguments
        // to the container for use by other classes within the facade
        public class Factory : Factory<EnemyTunables, EnemyFacade>
        {
        }
    }
}
