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

        readonly CloudState cloudState_;

        public CloudStateInitializer(CloudState cloudState) {
            cloudState_ = cloudState;
        }

        public void Initialize() {
            cloudState_.isConnecting = false;
            cloudState_.isConnected = false;
            cloudState_.isConnectionFailed = false;
            cloudState_.isDisconnected = true; // start disconnected
            cloudState_.isDisconnectionFailed = false;
            cloudState_.isDisconnecting = false;
            cloudState_.feedbackText = "";
        }
    }
}
