using Zenject;
using ExitGames.Client.Photon;

namespace Reduxity.Example.Zenject
{

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
        /// Creating room successful
        /// </summary>
        public bool isCreated { get; set; }

		/// <summary>
        /// Creating room failure
        /// </summary>
        public bool isCreateFailed { get; set; }

		/// <summary>
        /// Leaving in progress
        /// </summary>
        public bool isLeaving { get; set; }

		/// <summary>
        /// Leaving room successful
        /// </summary>
        public bool hasLeft { get; set; }

		/// <summary>
        /// Leaving room failure
        /// </summary>
        public bool isLeavingFailed { get; set; }

		/// <summary>
        /// Photon room name
        /// </summary>
        public string roomName { get; set; }

        public Hashtable roomProperties { get; set; }

		/// <summary>
        /// The Ui Text to inform the user about the Joining progress
        /// </summary>
        public string feedbackText { get; set; }
    }

    public class RoomStateInitializer : IStateInitializer, IInitializable {
        readonly RoomState state_;

        public RoomStateInitializer(RoomState roomState) {
            state_ = roomState;
        }

        public void Initialize() {
            state_.isJoining = false;
            state_.isJoined = false;
            state_.isJoinFailed = false;
            state_.isCreating = false;
            state_.isCreated = false;
            state_.isCreateFailed = false;
            state_.isLeaving = false;
            state_.hasLeft = false;
            state_.isLeavingFailed = false;
            state_.roomName = "";
            state_.roomProperties = null;
            state_.feedbackText = "";
        }
    }
}
