using UniRx;
using Zenject;
using System;

namespace Reduxity.Example.Zenject {

    public class PhotonRoomContainer : IInitializable, IContainer {

        readonly App app_;

        public PhotonRoomContainer(
            App app
        ) {
            app_ = app;
        }

        public void Initialize() {
        }
    }
}
