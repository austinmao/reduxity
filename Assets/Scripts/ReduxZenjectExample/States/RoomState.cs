using UnityEngine;
using Zenject;
using System;
using System.Collections.Generic;

namespace Reduxity.Example.Zenject {

    /// <summary>
    /// </summary>
    public class RoomState : IState {

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
        /// Creating room in progress
        /// </summary>
        public bool isCreating { get; set; }

		/// <summary>
        /// Leaving in progress
        /// </summary>
        public bool isLeaving { get; set; }

		/// <summary>
        /// The Ui Text to inform the user about the Joining progress
        /// </summary>
        public string feedbackText { get; set; }
    }

    public class RoomStateInitializer : IStateInitializer, IInitializable {
        readonly RoomState roomState_;

        public RoomStateInitializer(RoomState roomState) {
            roomState_ = roomState;
        }

        public void Initialize() {
            roomState_.isJoining = false;
            roomState_.isJoined = false;
            roomState_.isJoinFailed = false;
            roomState_.isCreating = false;
            roomState_.isLeaving = false;
            roomState_.feedbackText = "";
        }
    }
}
