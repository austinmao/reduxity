using System;
using Zenject;
using ModestTree;

namespace Zenject.SpaceFighter
{
    public interface IEnemyState : IDisposable
    {
        void Initialize();
        void Update();
        void FixedUpdate();
    }

    // This class controls the basic "AI" of our enemy
    // Which works as a finite state machine, between four states:
    // - Attack
    // - Follow/Chase
    // - Idle
    // - RunAway
    public class EnemyStateManager : ITickable, IFixedTickable, IInitializable
    {
        readonly EnemyStateFactory _stateFactory;

        IEnemyState _stateHandler;
        EnemyStates _currentState = EnemyStates.None;

        public EnemyStateManager(EnemyStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public EnemyStates CurrentState
        {
            get
            {
                return _currentState;
            }
        }

        public void Initialize()
        {
            Assert.IsEqual(_currentState, EnemyStates.None);
            Assert.IsNull(_stateHandler);

            ChangeState(EnemyStates.Idle);
        }

        public void ChangeState(EnemyStates state)
        {
            if (_currentState == state)
            {
                // Already in state
                return;
            }

            //Log.Trace("Enemy Changing state from {0} to {1}", _currentState, state);

            _currentState = state;

            if (_stateHandler != null)
            {
                _stateHandler.Dispose();
                _stateHandler = null;
            }

            _stateHandler = _stateFactory.Create(state);
            _stateHandler.Initialize();
        }

        public void Tick()
        {
            _stateHandler.Update();
        }

        public void FixedTick()
        {
            _stateHandler.FixedUpdate();
        }
    }
}
