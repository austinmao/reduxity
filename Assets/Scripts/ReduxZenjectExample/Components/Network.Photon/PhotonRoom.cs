using UniRx;
using Zenject;
using System;

namespace Reduxity.Example.Zenject {

    public class PhotonRoom : IInitializable, IComponent {

        readonly App app_;

        public PhotonRoom(
            App app
        ) {
            app_ = app;
        }

        public void Initialize() {
        }
    }
}
