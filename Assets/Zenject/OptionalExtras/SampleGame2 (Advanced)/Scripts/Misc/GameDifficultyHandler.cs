using System;
using UnityEngine;
using Zenject;

namespace Zenject.SpaceFighter
{
    public class GameDifficultyHandler : ITickable
    {
        readonly Settings _settings;
        readonly EnemyGlobalTunables _globalTunables;

        float _numAttacking;
        float _numEnemies;

        public GameDifficultyHandler(
            EnemyGlobalTunables globalTunables,
            Settings settings)
        {
            _settings = settings;
            _globalTunables = globalTunables;

            _numAttacking = globalTunables.NumAttacking;
            _numEnemies = globalTunables.DesiredNumEnemies;
        }

        public void Tick()
        {
            _numEnemies += _settings.NumEnemiesIncreaseRate * Time.deltaTime;
            _numAttacking += _settings.NumAttackingIncreaseRate * Time.deltaTime;

            _globalTunables.DesiredNumEnemies = (int)_numEnemies;
            _globalTunables.NumAttacking = (int)_numAttacking;
        }

        [Serializable]
        public class Settings
        {
            public float NumAttackingIncreaseRate;
            public float NumEnemiesIncreaseRate;
        }
    }
}
