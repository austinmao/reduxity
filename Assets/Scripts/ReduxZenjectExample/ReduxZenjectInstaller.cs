using System;
using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {
	public class ReduxZenjectInstaller : MonoInstaller<ReduxZenjectInstaller> {

		[Inject]
		Settings settings_ = null;

		public override void InstallBindings() {
			InstallState();
			InstallStateInitializers();
		}

		// create new state on each injection
        private void InstallState() {
			Container.Bind<State>().AsTransient();
        }

		// initialize state on each injection
        private void InstallStateInitializers() {
            Container.Bind<IInitializable>().To<MoveState>().AsTransient();
            Container.Bind<IInitializable>().To<CounterState>().AsTransient();
		}

		[Serializable]
		public class Settings {
			// public GameObject CubePrefab;
		}
	}
}
