using System;
using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {
    /// <summary>
    /// See here for more details:
    /// https://github.com/modesttree/zenject#installers
    /// </summary>
	public class NetworkInstaller : MonoInstaller<NetworkInstaller> {

        [SerializeField]
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
            // 11. install settings
            InstallSettings();
		}

        /// <summary>
        /// Initialize each state as a single instance. Bind Initialize() to each.
        /// </summary>
        void InstallState() {
            Container.Bind<CloudState>().AsSingle();
            Container.Bind<ClientState>().AsSingle();
            Container.Bind<LobbyState>().AsSingle();
            Container.Bind<RoomState>().AsSingle();
            Container.Bind<NetworkPlayerState>().AsSingle();
            Container.Bind<NetworkPlayersState>().AsSingle();
		}

        /// <summary>
        /// Auto-bind IInitializable to all state initializers
        /// @see https://github.com/modesttree/Zenject#examples
        /// </summary>
        void InstallStateInitializers() {
        }

        /// <summary>
        /// Install Actions to be used in thunks (actions that will fire off other actions,
        /// usually in async usage).
        /// </summary>
        void InstallActionCreators() {
            Container.Bind<CloudConnectCreator.ActionCreator>().AsSingle();
            Container.Bind<ClientConnectCreator.ActionCreator>().AsSingle();
            Container.Bind<LobbyConnectCreator.ActionCreator>().AsSingle();
            Container.Bind<RoomConnectCreator.ActionCreator>().AsSingle();
            Container.Bind<NetworkPlayerCreator.ActionCreator>().AsSingle();
        }

        void InstallMiddleware() {
        }

        /// <summary>
        /// Install reducers and use `WhenInjectedIno` in order to ensure that they are
        /// only available when used by App to create the Store. This is because nothing
        /// but the Store intiailizer should interact directly with reducers; only actions
        /// that are dispatched will.
        /// </summary>
        void InstallReducers() {
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
        void InstallApp() {
        }

        void InstallObservers() {
        }

        void InstallSelectors() {
        }

        /// <summary>
        /// Install Components that subscribe to and render state changes.
        /// </summary>
        void InstallComponents() {
            Container.Bind<PhotonLoggerComponent>().AsSingle();
            Container.Bind<ConnectToCloudButtonComponent>().AsSingle();
        }

        /// <summary>
        /// Install Containers, which are MonoBehaviours that may include mah components.
        /// </summary>
        void InstallContainers() {
            Container.Bind<PhotonCloudContainer>().AsSingle();
            Container.Bind<PhotonClientContainer>().AsSingle();
            Container.Bind<PhotonLobbyContainer>().AsSingle();
            Container.Bind<PhotonRoomContainer>().AsSingle();
            Container.Bind<PhotonNetworkPlayerContainer>().AsSingle();
        }

        void InstallSettings() {
            Container.BindInstance(settings_.GameSettings);
        }

		[Serializable]
		public class Settings {
            public NetworkSettings.GameSettings GameSettings;
		}
	}
}
