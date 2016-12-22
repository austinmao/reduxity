using System;
using UnityEngine;
using System.Collections;
using Zenject;

namespace Zenject.SpaceFighter
{
    // Main installer for our game
    public class GameInstaller : MonoInstaller
    {
        [Inject]
        Settings _settings = null;

        public override void InstallBindings()
        {
            Container.BindAllInterfaces<CameraHandler>().To<CameraHandler>().AsSingle();

            Container.BindSignal<PlayerKilledSignal>();
            Container.BindSignal<EnemyKilledSignal>();

            Container.BindAllInterfaces<EnemySpawner>().To<EnemySpawner>().AsSingle();

            Container.BindFactory<EnemyTunables, EnemyFacade, EnemyFacade.Factory>()
                .FromSubContainerResolve()
                .ByPrefab<EnemyInstaller>(_settings.EnemyFacadePrefab)
                .UnderTransformGroup("Enemies");

            Container.BindAllInterfaces<GameDifficultyHandler>().To<GameDifficultyHandler>().AsSingle();

            Container.Bind<EnemyRegistry>().AsSingle();

            Container.BindFactory<float, float, BulletTypes, Bullet, Bullet.Factory>()
                .FromPrefab(_settings.BulletPrefab)
                .UnderTransformGroup("Bullets");

            Container.BindFactory<Explosion, Explosion.Factory>()
                .FromPrefab(_settings.ExplosionPrefab)
                .UnderTransformGroup("Explosions");

            Container.Bind<AudioPlayer>().AsSingle();

            Container.BindAllInterfaces<GameRestartHandler>().To<GameRestartHandler>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public GameObject EnemyFacadePrefab;
            public GameObject BulletPrefab;
            public GameObject ExplosionPrefab;
        }
    }
}
