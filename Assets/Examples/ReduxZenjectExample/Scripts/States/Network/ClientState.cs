using Zenject;

namespace Reduxity.Example.Zenject {

    /// <summary>
    /// </summary>
    public class ClientState : IState {

		/// <summary>
        /// Server Connection in progress
        /// </summary>
        public bool isConnecting { get; set; }

		/// <summary>
        /// Server Connection successful
        /// </summary>
        public bool isConnected { get; set; }

		/// <summary>
        /// Server Connection failure
        /// </summary>
        public bool isConnectionFailed { get; set; }

		/// <summary>
        /// Switching client Connection in progress
        /// </summary>
        public bool isSwitching { get; set; }

		/// <summary>
        /// Server Disconnection in progress
        /// </summary>
        public bool isDisconnecting { get; set; }

		/// <summary>
        /// Server Disonnection successful
        /// </summary>
        public bool isDisconnected { get; set; }

		/// <summary>
        /// Server Disconnection failure
        /// </summary>
        public bool isDisconnectionFailed { get; set; }

        /// <summary>
        /// Photon player that acts as the Master Client
        /// </summary>
        public PhotonPlayer photonClient { get; set; }

		/// <summary>
        /// The Ui Text to inform the user about the connection progress
        /// </summary>
        public string feedbackText { get; set; }
    }

    public class ClientStateInitializer : IStateInitializer, IInitializable {
        readonly ClientState state_;

        public ClientStateInitializer(ClientState clientState) {
            state_ = clientState;
        }

        public void Initialize() {
            state_.isConnecting = false;
            state_.isConnected = false;
            state_.isConnectionFailed = false;
            state_.isSwitching = false;
            state_.isDisconnecting = false;
            state_.isDisconnected = true; // start disconnected
            state_.isDisconnectionFailed = false;
            state_.feedbackText = "";
        }
    }
}
