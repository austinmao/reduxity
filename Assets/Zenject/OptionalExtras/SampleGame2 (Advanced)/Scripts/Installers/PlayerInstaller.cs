using System;
using UnityEngine;
using Zenject;

namespace Zenject.SpaceFighter
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        Settings _settings = null;

        public override void InstallBindings()
        {
            Container.BindInstance(_settings.Rigidbody).WhenInjectedInto<PlayerModel>();
            Container.BindInstance(_settings.MeshRenderer).WhenInjectedInto<PlayerModel>();
            Container.Bind<PlayerModel>().AsSingle();

            Container.BindAllInterfaces<PlayerInputHandler>().To<PlayerInputHandler>().AsSingle();
            Container.BindAllInterfaces<PlayerMoveHandler>().To<PlayerMoveHandler>().AsSingle();
            Container.BindAllInterfaces<PlayerBulletHitHandler>().To<PlayerBulletHitHandler>().AsSingle();
            Container.BindAllInterfaces<PlayerDirectionHandler>().To<PlayerDirectionHandler>().AsSingle();
            Container.BindAllInterfaces<PlayerShootHandler>().To<PlayerShootHandler>().AsSingle();

            Container.Bind<PlayerInputState>().AsSingle();

            Container.BindAllInterfaces<PlayerHealthWatcher>().To<PlayerHealthWatcher>().AsSingle();

            Container.BindSignal<PlayerSignals.Hit>();

            InstallSettings();
        }

        void InstallSettings()
        {
            Container.BindInstance(_settings.PlayerMoveHandler);
            Container.BindInstance(_settings.PlayerShootHandler);
            Container.BindInstance(_settings.PlayerCollisionHandler);
        }

        [Serializable]
        public class Settings
        {
            public Rigidbody Rigidbody;
            public MeshRenderer MeshRenderer;

            public PlayerMoveHandler.Settings PlayerMoveHandler;
            public PlayerShootHandler.Settings PlayerShootHandler;
            public PlayerBulletHitHandler.Settings PlayerCollisionHandler;
        }
    }
}
