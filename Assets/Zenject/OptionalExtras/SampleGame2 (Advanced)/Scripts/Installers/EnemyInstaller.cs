using System;
using UnityEngine;
using Zenject;

namespace Zenject.SpaceFighter
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField]
        Settings _settings = null;

        [InjectOptional]
        EnemyTunables _settingsOverride = null;

        public override void InstallBindings()
        {
            // Doing it this way allows us to drop enemies into the scene and set these settings
            // directly but also allow creating enemies dynamically and injecting these settings instead
            Container.BindInstance(_settingsOverride != null ? _settingsOverride : _settings.DefaultSettings);

            Container.BindInstance(_settings.Rigidbody).WhenInjectedInto<EnemyModel>();
            Container.BindInstance(_settings.Collider).WhenInjectedInto<EnemyModel>();
            Container.BindInstance(_settings.Renderer).WhenInjectedInto<EnemyModel>();

            Container.Bind<EnemyModel>().AsSingle();

            Container.BindAllInterfaces<EnemyCollisionHandler>().To<EnemyCollisionHandler>().AsSingle();

            Container.BindAllInterfacesAndSelf<EnemyStateManager>().To<EnemyStateManager>().AsSingle();

            Container.Bind<EnemyStateFactory>().AsSingle();

            Container.BindAllInterfaces<EnemyHealthWatcher>().To<EnemyHealthWatcher>().AsSingle();

            Container.BindSignal<EnemySignals.Hit>();

            Container.BindAllInterfaces<EnemyStateCommon>().To<EnemyStateCommon>().AsSingle();
            Container.BindAllInterfaces<EnemyRotationHandler>().To<EnemyRotationHandler>().AsSingle();

            InstallSettings();
        }

        void InstallSettings()
        {
            Container.BindInstance(_settings.EnemyCollisionHandler);
            Container.BindInstance(_settings.EnemyStateIdle);
            Container.BindInstance(_settings.EnemyStateRunAway);
            Container.BindInstance(_settings.EnemyRotationHandler);
            Container.BindInstance(_settings.EnemyStateFollow);
            Container.BindInstance(_settings.EnemyStateAttack);
        }

        [Serializable]
        public class Settings
        {
            public GameObject RootObject;
            public Rigidbody Rigidbody;
            public Collider Collider;
            public MeshRenderer Renderer;

            public EnemyTunables DefaultSettings;
            public EnemyCollisionHandler.Settings EnemyCollisionHandler;
            public EnemyStateIdle.Settings EnemyStateIdle;
            public EnemyStateRunAway.Settings EnemyStateRunAway;
            public EnemyRotationHandler.Settings EnemyRotationHandler;
            public EnemyStateFollow.Settings EnemyStateFollow;
            public EnemyStateAttack.Settings EnemyStateAttack;
        }
    }
}
