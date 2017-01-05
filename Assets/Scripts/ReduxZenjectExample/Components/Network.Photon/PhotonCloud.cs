using UniRx;
using Zenject;
using System;

namespace Reduxity.Example.Zenject {

    public class PhotonCloud : IInitializable, IComponent {

        readonly App app_;

        public PhotonCloud(
            App app
        ) {
            app_ = app;
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
                    // TODO: appropriate to dispatch here?
                    // do some rendering here
                });
        }

        void RenderConnecting() {
            app_.Store
                // .TakeUntilDestroy
                .Where(state => state.Cloud.isConnecting)
                .Subscribe(_ => {
                    // do some rendering here
                });
        }

        void RenderConnected() {
            app_.Store
                // .TakeUntilDestroy
                .Where(state => state.Cloud.isConnected)
                .Subscribe(_ => {
                    // do some rendering here
                });
        }

        void RenderConnectionFailure() {
            app_.Store
                // .TakeUntilDestroy
                .Where(state => state.Cloud.isConnectionFailed)
                .Subscribe(_ => {
                    // do some rendering here
                });
        }
    }
}
