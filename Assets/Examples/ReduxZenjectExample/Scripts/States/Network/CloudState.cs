using Zenject;

namespace Reduxity.Example.Zenject {

    /// <summary>
    /// </summary>
    public class CloudState : IState {

		/// <summary>
        /// Cloud network provider Connection in progress
        /// </summary>
        public bool isConnecting { get; set; }

		/// <summary>
        /// Cloud network provider Connection successful
        /// </summary>
        public bool isConnected { get; set; }

		/// <summary>
        /// Cloud network provider Connection failure
        /// </summary>
        public bool isConnectionFailed { get; set; }

		/// <summary>
        /// Cloud network provider Disconnection in progress
        /// </summary>
        public bool isDisconnecting { get; set; }

		/// <summary>
        /// Cloud network provider Connection successful
        /// </summary>
        public bool isDisconnected { get; set; }

		/// <summary>
        /// Cloud network provider Connection failure
        /// </summary>
        public bool isDisconnectionFailed { get; set; }

		/// <summary>
        /// The Ui Text to inform the user about the connection progress
        /// </summary>
        public string feedbackText { get; set; }
    }

    public class CloudStateInitializer : IStateInitializer, IInitializable {

        readonly CloudState state_;

        public CloudStateInitializer(CloudState cloudState) {
            state_ = cloudState;
        }

        public void Initialize() {
            state_.isConnecting = false;
            state_.isConnected = false;
            state_.isConnectionFailed = false;
            state_.isDisconnected = true; // start disconnected
            state_.isDisconnectionFailed = false;
            state_.isDisconnecting = false;
            state_.feedbackText = "";
        }
    }
}
