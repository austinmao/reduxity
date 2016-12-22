using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Zenject.Asteroids;

namespace Zenject.Asteroids
{
    [TestFixture]
    public class TestAsteroidManager : ZenjectIntegrationTestFixture
    {
        [SetUp]
        public void CommonInstall()
        {
            GameSettingsInstaller.InstallFromResource(Container);
            var gameSettings = Container.Resolve<GameInstaller.Settings>();
            Container.Bind<AsteroidManager>().AsSingle();
            Container.BindFactory<Asteroid, Asteroid.Factory>().FromPrefab(gameSettings.AsteroidPrefab);
            Container.Bind<Camera>().WithId("Main").FromGameObject();
            Container.Bind<LevelHelper>().AsSingle();

            Initialize();
        }

        [Inject]
        AsteroidManager _asteroidManager;

        [Inject]
        AsteroidManager.Settings _asteroidManagerSettings;

        [Test]
        [ValidateOnly]
        public void TestValidate()
        {
        }

        [Test]
        public void TestInitialSpawns()
        {
            _asteroidManager.Start();

            Assert.AreEqual(_asteroidManager.Asteroids.Count(), _asteroidManagerSettings.startingSpawns);
        }

        [Test]
        public void TestSpawnNext()
        {
            _asteroidManager.Start();
            _asteroidManager.SpawnNext();

            Assert.AreEqual(_asteroidManager.Asteroids.Count(), _asteroidManagerSettings.startingSpawns + 1);
        }
    }
}
