using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller> {

    [Inject]
    Settings settings_ = null;

    public override void InstallBindings() {
        InstallCube();
    }

    void InstallCube() {
        Container.BindAllInterfacesAndSelf<CubeManager>().To<CubeManager>();

        // install cube factory
        Container.BindFactory<Cube, Cube.Factory>()
            .FromPrefab(settings_.CubePrefab)
            .WithGameObjectName("Cube")
            .UnderTransformGroup("Cubes");
    }

    [Serializable]
    public class Settings {
        public GameObject CubePrefab;
    }
}