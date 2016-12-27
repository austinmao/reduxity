using System;
using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {
	public class ReduxZenjectInstaller : MonoInstaller<ReduxZenjectInstaller> {

		public override void InstallBindings() {
			InstallState();
			InstallStateInitializers();
            InstallReducers();
            InstallComponents();
            InstallApp();
		}

		// create new state on each injection
        private void InstallState() {
			Container.Bind<State>().AsSingle(); // TODO: should this be transient?
        }

		// initialize state on each injection
        private void InstallStateInitializers() {
            Container.Bind<IInitializable>().To<CharacterState>().AsSingle();
            Container.Bind<CharacterState>().AsSingle();
            Container.Bind<IInitializable>().To<CameraState>().AsSingle();
            Container.Bind<CameraState>().AsSingle();
		}

        private void InstallReducers() {
            Container.Bind<Movement.Reducer>().AsSingle();
            Container.Bind<Look.Reducer>().AsSingle();
        }

        private void InstallComponents() {
            Container.Bind<IInitializable>().To<MoveCharacter>().AsSingle();
            Container.Bind<MoveCharacter>().AsSingle();
            Container.Bind<IInitializable>().To<MoveCamera>().AsSingle();
            Container.Bind<MoveCamera>().AsSingle();
        }

        private void InstallApp() {
            Container.Bind<IInitializable>().To<App>().AsSingle();
            Container.Bind<App>().AsSingle();
        }

        // void InitExecutionOrder() {
        //     init state before app to ensure store gets default app state
        //     Container.BindExecutionOrder<State>(-10);
        //     Container.BindExecutionOrder<Reducers>(-20);
        //     Container.BindExecutionOrder<App>(-30);
        // }

		[Serializable]
		public class Settings {
		}
	}
}
