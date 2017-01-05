using UnityEngine;
using Zenject;
using System;
using System.Collections.Generic;

namespace Reduxity.Example.Zenject {

    /// <summary>
    /// </summary>
    public class LobbyState : IState {

		/// <summary>
        /// Joining in progress
        /// </summary>
        public bool isJoining { get; set; }

		/// <summary>
        /// Joining successful
        /// </summary>
        public bool isJoined { get; set; }

		/// <summary>
        /// Joining failure
        /// </summary>
        public bool isJoinFailed { get; set; }

		/// <summary>
        /// Creating lobby in progress
        /// </summary>
        public bool isCreating { get; set; }

		/// <summary>
        /// Creating lobby successful
        /// </summary>
        public bool isCreated { get; set; }

		/// <summary>
        /// Creating lobby failure
        /// </summary>
        public bool isCreateFailed { get; set; }

		/// <summary>
        /// Leaving in progress
        /// </summary>
        public bool isLeaving { get; set; }

		/// <summary>
        /// Leaving lobby successful
        /// </summary>
        public bool hasLeft { get; set; }

		/// <summary>
        /// Leaving lobby failure
        /// </summary>
        public bool isLeavingFailed { get; set; }

		/// <summary>
        /// The Ui Text to inform the user about the Joining progress
        /// </summary>
        public string feedbackText { get; set; }

		/// <summary>
        /// Photon lobby name
        /// </summary>
        public string lobbyName { get; set; }
    }

    public class LobbyStateInitializer : IStateInitializer, IInitializable {
        readonly LobbyState lobbyState_;

        public LobbyStateInitializer(LobbyState lobbyState) {
            lobbyState_ = lobbyState;
        }

        public void Initialize() {
            lobbyState_.isJoining = false;
            lobbyState_.isJoined = false;
            lobbyState_.isJoinFailed = false;
            lobbyState_.isCreating = false;
            lobbyState_.isLeaving = false;
            lobbyState_.hasLeft = false;
            lobbyState_.isLeavingFailed = false;
            lobbyState_.feedbackText = "";
        }
    }
}
