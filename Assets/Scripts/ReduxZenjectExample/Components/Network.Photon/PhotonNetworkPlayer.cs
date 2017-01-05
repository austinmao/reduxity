using UniRx;
using Zenject;
using System;

namespace Reduxity.Example.Zenject {

    public class PhotonNetworkPlayer : IInitializable, IComponent {

        readonly App app_;

        public PhotonNetworkPlayer(
            App app
        ) {
            app_ = app;
        }

        public void Initialize() {
        }
    }
}
