using UnityEngine;
using Zenject;
using System;
using System.Collections.Generic;

namespace Reduxity.Example.Zenject {

    /// <summary>
    /// </summary>
    public class NetworkState : IState {

		/// <summary>
        /// Connection in progress
        /// </summary>
        public bool isConnecting { get; set; }

		/// <summary>
        /// Connection successful
        /// </summary>
        public bool isConnected { get; set; }

		/// <summary>
        /// Connection failure
        /// </summary>
        public bool isConnectionFailed { get; set; }

		/// <summary>
        /// Disconnection in progress
        /// </summary>
        public bool isDisconnecting { get; set; }

		/// <summary>
        /// The Ui Text to inform the user about the connection progress
        /// </summary>
        public string feedbackText { get; set; }
    }

    public class NetworkStateInitializer : IStateInitializer, IInitializable {
        readonly NetworkState networkState_;

        public NetworkStateInitializer(NetworkState networkState) {
            networkState_ = networkState;
        }

        public void Initialize() {
            networkState_.isConnecting = false;
            networkState_.isConnected = false;
            networkState_.isConnectionFailed = false;
            networkState_.isDisconnecting = false;
            networkState_.feedbackText = "";
        }
    }
}
