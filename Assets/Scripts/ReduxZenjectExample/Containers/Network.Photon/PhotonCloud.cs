using UniRx;
using Zenject;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Reduxity.Example.Zenject {

    public class PhotonCloudContainer : IInitializable, IContainer {

        readonly App app_;
        readonly Settings settings_;
        // readonly Text logger_;

        /* components */
        readonly PhotonLoggerComponent logger_;
        readonly ConnectToCloudButtonComponent button_;

        private PhotonLoggerComponent.ConnectStatus status_;
        private PhotonLoggerComponent.ConnectType type_;

        public PhotonCloudContainer(
            App app,
            Settings settings,
            // [Inject(Id = "PhotonLogger")]
            // Text logger,
            PhotonLoggerComponent logger,
            ConnectToCloudButtonComponent button
        ) {
            app_ = app;
			settings_ = settings;
            logger_ = logger;
            button_ = button;
        }

        public void Initialize() {
            // connect immediately on startup if settings say so
            if (settings_.shouldConnectOnStartup) {
                StartConnect();
            }

            RenderIsNotConnected();
            RenderConnecting();
            RenderConnected();
            RenderConnectionFailure();
        }

        /// <summary>
        /// Start connecting to Photon Cloud
        /// </summary>
        void StartConnect() {
            // app_.Store
            //     // .TakeUntilDestroy
            //     .Where(state => {
            //         // only connect if not already connected or connecting
            //         return (
            //             state.Cloud.isDisconnected &&
            //             !state.Cloud.isConnected &&
            //             !state.Cloud.isConnecting
            //         );
            //     })
            //     .Subscribe(_ => {
            //         Debug.Log("Dispatching Connect to Photon Cloud");
            //         var action = new CloudConnector.Action.ConnectStart {};
            //         app_.Store.Dispatch(action);
            //     });
        }


        void RenderIsNotConnected() {
            app_.Store
                // .TakeUntilDestroy
                .Where(state => state.Cloud.isDisconnected)
                .Subscribe(_ => {
                    logger_.Render(PhotonLoggerComponent.ConnectStatus.notConnected, PhotonLoggerComponent.ConnectType.cloud);
                    button_.Render(ConnectToCloudButtonComponent.ConnectStatus.notConnected);
                });
        }

        void RenderConnecting() {
            app_.Store
                // .TakeUntilDestroy
                .Where(state => state.Cloud.isConnecting)
                .Subscribe(state => {
                    logger_.Render(PhotonLoggerComponent.ConnectStatus.connecting, PhotonLoggerComponent.ConnectType.cloud);
                    button_.Render(ConnectToCloudButtonComponent.ConnectStatus.connecting);
                });
        }

        void RenderConnected() {
            app_.Store
                // .TakeUntilDestroy
                .Where(state => state.Cloud.isConnected)
                .Subscribe(_ => {
                    logger_.Render(PhotonLoggerComponent.ConnectStatus.connected, PhotonLoggerComponent.ConnectType.cloud);
                    button_.Render(ConnectToCloudButtonComponent.ConnectStatus.connected);
                });
        }

        void RenderConnectionFailure() {
            app_.Store
                .Where(state => state.Cloud.isConnectionFailed)
                .Subscribe(_ => {
                    logger_.Render(PhotonLoggerComponent.ConnectStatus.connectionFailure, PhotonLoggerComponent.ConnectType.cloud);
                    button_.Render(ConnectToCloudButtonComponent.ConnectStatus.notConnected);
                });
        }

		[Serializable]
		/// <summary>
		/// </summary>
		public class Settings {
            public bool shouldConnectOnStartup = true;
		}
    }
}
