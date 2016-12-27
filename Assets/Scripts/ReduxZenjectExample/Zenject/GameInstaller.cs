using System;
using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {
	public class GameInstaller : MonoInstaller<GameInstaller> {

        [Inject]
        Settings settings_ = null;

        /// <summary>
        /// install bindings into the direct injection container
        /// note: order matters!
        /// </summary>
		public override void InstallBindings() {
            // 1. init default nested state object
			InstallState();
            // 2. reducers are injected into App
            InstallReducers();
            // 3. create store from reducers
            InstallApp();
            // 4. init store state subscribers
            InstallComponents();
            // 5. install renderers
            InstallContainers();
		}

		// initialize state on each injection
        private void InstallState() {
            Container.Bind<IInitializable>().To<CharacterState>().AsSingle();
            Container.Bind<CharacterState>().AsSingle();
            Container.Bind<IInitializable>().To<CameraState>().AsSingle();
            Container.Bind<CameraState>().AsSingle();
			Container.Bind<State>().AsSingle(); // TODO: should this be transient?
		}

        private void InstallReducers() {
            Container.Bind<Movement.Reducer>().AsSingle().WhenInjectedInto<App>();
            Container.Bind<Look.Reducer>().AsSingle().WhenInjectedInto<App>();
        }

        private void InstallApp() {
            Container.Bind<IInitializable>().To<App>().AsSingle();
            Container.Bind<App>().AsSingle();
        }

        private void InstallComponents() {
            Container.Bind<IInitializable>().To<MoveCharacter>().AsSingle();
            Container.Bind<MoveCharacter>().AsSingle();
            Container.Bind<IInitializable>().To<MoveCamera>().AsSingle();
            Container.Bind<MoveCamera>().AsSingle();
        }

        private void InstallContainers() {
        }

		[Serializable]
		public class Settings {
		}
	}
}
