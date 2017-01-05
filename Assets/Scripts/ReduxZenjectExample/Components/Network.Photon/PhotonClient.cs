using UniRx;
using Zenject;
using System;

namespace Reduxity.Example.Zenject {

    public class PhotonClient : IInitializable, IComponent {

        readonly App app_;

        public PhotonClient(
            App app
        ) {
            app_ = app;
        }

        public void Initialize() {
        }
    }
}
