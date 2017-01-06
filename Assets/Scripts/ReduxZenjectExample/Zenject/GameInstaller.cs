using System;
using Zenject;

namespace Reduxity.Example.Zenject {
    /// <summary>
    /// See here for more details:
    /// https://github.com/modesttree/zenject#installers
    /// </summary>
	public class GameInstaller : MonoInstaller<GameInstaller> {

        [Inject]
        Settings settings_ = null;

        /// <summary>
        /// Install bindings into the direct injection container.
        /// Note: order matters because of initializers!
        /// </summary>
		public override void InstallBindings() {
            // 1. set up state singleton
			InstallState();
            // 2. init nested state objects with default values
            InstallStateInitializers();
            // 4. reducers are injected into App
            InstallReducers();
            // 5. actions used in (async) thunks
            InstallActionCreators();
            // 6. install redux middleware
            InstallMiddleware();
            // 6. create store from reducers
            InstallApp();
            // 7. install observers, which may be MonoBehaviours
            InstallObservers();
            // 8. install selectors, which are used by components
            InstallSelectors();
            // 9. init store state subscribers
            InstallComponents();
            // 10. renderers
            InstallContainers();
		}

        /// <summary>
        /// Initialize each state as a single instance. Bind Initialize() to each.
        /// </summary>
        private void InstallState() {
            Container.Bind<CharacterState>().AsSingle();
            Container.Bind<CameraState>().AsSingle();
            Container.Bind<ApiState>().AsSingle();

            // network states
            Container.Bind<CloudState>().AsSingle();
            Container.Bind<ClientState>().AsSingle();
            Container.Bind<LobbyState>().AsSingle();
            Container.Bind<RoomState>().AsSingle();
            Container.Bind<NetworkPlayerState>().AsSingle();
            Container.Bind<NetworkPlayersState>().AsSingle();

            // global state object
			Container.Bind<State>().AsSingle();
		}

        /// <summary>
        /// Auto-bind IInitializable to all state initializers
        /// @see https://github.com/modesttree/Zenject#examples
        /// </summary>
        private void InstallStateInitializers() {
            Container.Bind<IInitializable>().To(x => x.AllTypes().DerivingFrom<IStateInitializer>()).AsSingle();
        }

        /// <summary>
        /// Install Actions to be used in thunks (actions that will fire off other actions,
        /// usually in async usage).
        /// </summary>
        private void InstallActionCreators() {
            Container.Bind<ApiRequestCreator.ActionCreator>().AsSingle();
            Container.Bind<CloudConnectCreator.ActionCreator>().AsSingle();
            Container.Bind<ClientConnectCreator.ActionCreator>().AsSingle();
            Container.Bind<LobbyConnectCreator.ActionCreator>().AsSingle();
            Container.Bind<RoomConnectCreator.ActionCreator>().AsSingle();
            Container.Bind<NetworkPlayerCreator.ActionCreator>().AsSingle();
        }

        private void InstallMiddleware() {
            Container.Bind<Reduxity.Middleware.Logger>().AsSingle();
        }

        /// <summary>
        /// Install reducers and use `WhenInjectedIno` in order to ensure that they are
        /// only available when used by App to create the Store. This is because nothing
        /// but the Store intiailizer should interact directly with reducers; only actions
        /// that are dispatched will.
        /// </summary>
        private void InstallReducers() {
            Container.Bind<CharacterMover.Reducer>().AsSingle().WhenInjectedInto<App>();
            Container.Bind<CameraLook.Reducer>().AsSingle().WhenInjectedInto<App>();

            // http requests
            Container.Bind<ApiRequestor.Reducer>().AsSingle().WhenInjectedInto<App>();

            // multiplayer network
            Container.Bind<CloudConnector.Reducer>().AsSingle().WhenInjectedInto<App>();
            Container.Bind<ClientConnector.Reducer>().AsSingle().WhenInjectedInto<App>();
            Container.Bind<LobbyConnector.Reducer>().AsSingle().WhenInjectedInto<App>();
            Container.Bind<RoomConnector.Reducer>().AsSingle().WhenInjectedInto<App>();
            Container.Bind<NetworkPlayer.Reducer>().AsSingle().WhenInjectedInto<App>();
            Container.Bind<NetworkPlayers.Reducer>().AsSingle().WhenInjectedInto<App>();
        }

        /// <summary>
        /// Install the App, which contains the Store that is initialized.
        /// </summary>
        private void InstallApp() {
            Container.Bind<IInitializable>().To<App>().AsSingle();
            Container.Bind<App>().AsSingle();
        }

        private void InstallObservers() {
            Container.Bind<IInitializable>().To<PCInput>().AsSingle();
            Container.Bind<PCInput>().AsSingle();
            Container.Bind<IInitializable>().To<ApiRequest>().AsSingle();
            Container.Bind<ApiRequest>().AsSingle();
        }

        private void InstallSelectors() {
            Container.Bind<CharacterMoverSelector>().AsSingle();
            Container.Bind<ApiDataSelector>().AsSingle();
        }

        /// <summary>
        /// Install Components that subscribe to and render state changes.
        /// </summary>
        private void InstallComponents() {
            Container.Bind<IInitializable>().To(x => x.AllTypes().DerivingFrom<IComponent>()).AsSingle();
            Container.Bind<MoveCharacter>().AsSingle();
            Container.Bind<MoveCamera>().AsSingle();
            Container.Bind<DisplayHttpText>().AsSingle();
            Container.Bind<DisplayHttpButton>().AsSingle();

            // network
            Container.Bind<PhotonCloud>().AsSingle();
            Container.Bind<PhotonClient>().AsSingle();
            Container.Bind<PhotonLobby>().AsSingle();
            Container.Bind<PhotonRoom>().AsSingle();
            Container.Bind<PhotonNetworkPlayer>().AsSingle();
        }

        /// <summary>
        /// Install Containers, which are MonoBehaviours that may include mah components.
        /// </summary>
        private void InstallContainers() {
        }

		[Serializable]
		public class Settings {
            /// <summary>
            /// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
            /// </summary>
            string gameVersion = "1";
		}
	}
}
