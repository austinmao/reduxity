using UniRx;
using Zenject;
using System;

namespace Reduxity.Example.Zenject {

    public class PhotonNetworkPlayerContainer : IInitializable, IContainer {

        readonly App app_;

        public PhotonNetworkPlayerContainer(
            App app
        ) {
            app_ = app;
        }

        public void Initialize() {
        }
    }
}
