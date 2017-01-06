using UniRx;
using Zenject;
using System;

namespace Reduxity.Example.Zenject {

    public class PhotonClientContainer : IInitializable, IContainer {

        readonly App app_;

        public PhotonClientContainer(
            App app
        ) {
            app_ = app;
        }

        public void Initialize() {
        }
    }
}
