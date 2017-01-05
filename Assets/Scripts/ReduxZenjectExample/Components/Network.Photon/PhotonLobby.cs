using UniRx;
using Zenject;
using System;

namespace Reduxity.Example.Zenject {

    public class PhotonLobby : IInitializable, IComponent {

        readonly App app_;

        public PhotonLobby(
            App app
        ) {
            app_ = app;
        }

        public void Initialize() {
        }
    }
}
