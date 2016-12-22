using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Zenject.SpaceFighter
{
    public class EnemySpawner : ITickable
    {
        readonly EnemyGlobalTunables _globalTunables;
        readonly EnemyRegistry _enemyRegistry;
        readonly EnemyFacade.Factory _enemyFactory;
        readonly Settings _settings;
        readonly PlayerFacade _player;

        public EnemySpawner(
            PlayerFacade player,
            Settings settings,
            EnemyFacade.Factory enemyFactory,
            EnemyRegistry enemyRegistry,
            EnemyGlobalTunables globalTunables)
        {
            _globalTunables = globalTunables;
            _enemyRegistry = enemyRegistry;
            _enemyFactory = enemyFactory;
            _settings = settings;
            _player = player;
        }

        public void Tick()
        {
            if (_enemyRegistry.NumEnemies < _globalTunables.DesiredNumEnemies)
            {
                SpawnEnemy();
            }
        }

        void SpawnEnemy()
        {
            var settings = new EnemyTunables()
            {
                Speed = Random.Range(_settings.SpeedMin, _settings.SpeedMax),
                Accuracy = Random.Range(_settings.AccuracyMin, _settings.AccuracyMax),
                AttackDistance = Random.Range(_settings.AttackDistanceMin, _settings.AttackDistanceMax),
            };

            var enemyFacade = _enemyFactory.Create(settings);

            // Spawn from a random direction
            var startTheta = Random.Range(0, 2 * Mathf.PI);

            var spawnDistance = Random.Range(_settings.SpawnDistanceMin, _settings.SpawnDistanceMax);

            enemyFacade.Position = _player.Position
                + Vector3.right * spawnDistance * Mathf.Cos(startTheta)
                + Vector3.up * spawnDistance * Mathf.Sin(startTheta);
        }

        [Serializable]
        public class Settings
        {
            public float SpeedMin;
            public float SpeedMax;

            public float AccuracyMin;
            public float AccuracyMax;

            public float AttackDistanceMin;
            public float AttackDistanceMax;

            public float SpawnDistanceMin;
            public float SpawnDistanceMax;
        }
    }
}
