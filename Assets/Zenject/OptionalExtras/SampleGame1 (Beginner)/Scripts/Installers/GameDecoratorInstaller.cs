using UnityEngine;
using System.Collections;
using Zenject;
using ModestTree;

namespace Zenject.Asteroids
{
    // This scene is an example of how decorators work
    // We override move settings to make the ship much slower and add a hotkey to manually spawn asteroids
    // Decorators are really useful in particular for running different test configurations
    public class GameDecoratorInstaller : MonoInstaller
    {
        public ShipStateMoving.Settings OverrideMoveSettings;

        // If you are injecting into an installer then you will need to put the binding in PreInstall
        public override void InstallBindings()
        {
            Container.Bind<ITickable>().To<TestHotKeysAdder>().AsSingle();
            // Do not spawn asteroids automatically
            Container.BindInstance(false).WhenInjectedInto<AsteroidManager>();
        }
    }

    public class TestHotKeysAdder : ITickable
    {
        readonly AsteroidManager _asteroidManager;

        public TestHotKeysAdder(AsteroidManager asteroidManager)
        {
            _asteroidManager = asteroidManager;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.F4))
            {
                _asteroidManager.SpawnNext();
                Log.Info("Spawned new asteroid!");
            }
        }
    }
}
