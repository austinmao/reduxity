using UniRx;
using Zenject;
using System;

namespace Reduxity.Example.Zenject {

    public class PhotonLobbyContainer : IInitializable, IContainer {

        readonly App app_;

        public PhotonLobbyContainer(
            App app
        ) {
            app_ = app;
        }

        public void Initialize() {
        }
    }
}
