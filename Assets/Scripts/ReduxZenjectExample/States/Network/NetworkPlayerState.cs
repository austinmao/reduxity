using Zenject;

namespace Reduxity.Example.Zenject
{

    /// <summary>
    /// </summary>
    public class NetworkPlayerState : IState {
		PhotonPlayer photonPlayer { get; set; }
    }

    public class NetworkPlayerStateInitializer : IStateInitializer, IInitializable {
        readonly NetworkPlayerState state_;

        public NetworkPlayerStateInitializer(NetworkPlayerState networkPlayerState) {
            state_ = networkPlayerState;
        }

        public void Initialize() {
        }
    }
}
