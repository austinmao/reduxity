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
        /// The Ui Text to inform the user about the connection progress
        /// </summary>
        public string feedbackText { get; set; }
    }

    public class ClientStateInitializer : IStateInitializer, IInitializable {
        readonly ClientState clientState_;

        public ClientStateInitializer(ClientState clientState) {
            clientState_ = clientState;
        }

        public void Initialize() {
            clientState_.isConnecting = false;
            clientState_.isConnected = false;
            clientState_.isConnectionFailed = false;
            clientState_.isDisconnecting = false;
            clientState_.isDisconnected = true; // start disconnected
            clientState_.isDisconnectionFailed = false;
            clientState_.feedbackText = "";
        }
    }
}
