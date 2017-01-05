using UniRx;
using Zenject;
using System;
using UnityEngine.UI;

namespace Reduxity.Example.Zenject {

    public class PhotonCloud : IInitializable, IComponent {

        readonly App app_;
        readonly Text logger_;
        readonly Button button_;
        readonly Text buttonText_;

        public PhotonCloud(
            App app,
            [Inject(Id = "PhotonLogger")]
            Text logger,
            [Inject(Id = "ConnectToCloud")]
            Button button,
            [Inject(Id = "ConnectToCloud")]
            Text buttonText
        ) {
            app_ = app;
            logger_ = logger;
            button_ = button;
            buttonText_ = buttonText;
        }

        public void Initialize() {
            RenderIsNotConnected();
            RenderConnecting();
            RenderConnected();
            RenderConnectionFailure();
        }

        void RenderIsNotConnected() {
            app_.Store
                // .TakeUntilDestroy
                .Where(state => state.Cloud.isDisconnected)
                .Subscribe(_ => {
                    logger_.text = "Click to connect to Photon Cloud.";
                    button_.interactable = true;
                    buttonText_.text = "Connect";
                });
        }

        void RenderConnecting() {
            app_.Store
                // .TakeUntilDestroy
                .Where(state => state.Cloud.isConnecting)
                .Subscribe(state => {
                    logger_.text = "Connecting...";
                    button_.interactable = false;
                });
        }

        void RenderConnected() {
            app_.Store
                // .TakeUntilDestroy
                .Where(state => state.Cloud.isConnected)
                .Subscribe(_ => {
                    logger_.text = "Connected.";
                    button_.interactable = true;
                    buttonText_.text = "Disconnect";
                });
        }

        void RenderConnectionFailure() {
            app_.Store
                .Where(state => state.Cloud.isConnectionFailed)
                .Subscribe(_ => {
                    logger_.text = "Connection failure!";
                    button_.interactable = true;
                    buttonText_.text = "Connect";
                });
        }
    }
}
