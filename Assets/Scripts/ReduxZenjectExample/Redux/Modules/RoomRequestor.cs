using Redux;
using System;
using Photon;
using UnityEngine.UI;

namespace Reduxity.Example.Zenject.RoomRequestor {
    public class Action {
        /// <summary>
        /// Properties for each type of http request
        /// </summary>
        public abstract class IRoomAction {
            public string feedbackText { get; set; }
            public string roomName { get; set; }
        }

        public class JoinStart : IRoomAction, IAction {}
        public class JoinSuccess : IRoomAction, IAction {}
        public class JoinFailure : IRoomAction, IAction {}
        public class CreateRoomStart : IRoomAction, IAction {
        }
        public class LeaveStart : IRoomAction, IAction {}
        public class LeaveSuccess : IRoomAction, IAction {}
    }

    public class Reducer : IReducer {
        readonly Settings settings_;

        public Reducer(Settings settings) {
            settings_ = settings;
        }

        public RoomState Reduce(RoomState state, IAction action) {
            if (action is Action.JoinStart) {
                return StartJoin(state, (Action.JoinStart)action);
            }

            if (action is Action.JoinSuccess) {
                return JoinSuccess(state, (Action.JoinSuccess)action);
            }

            if (action is Action.JoinFailure) {
                return JoinFailure(state, (Action.JoinFailure)action);
            }

            if (action is Action.CreateRoomStart) {
                return StartCreate(state, (Action.CreateRoomStart)action);
            }

            if (action is Action.LeaveStart) {
                return Leave(state, (Action.LeaveStart)action);
            }

            if (action is Action.LeaveSuccess) {
                return LeaveSuccess(state, (Action.LeaveSuccess)action);
            }

            return state;
        }

        private RoomState StartJoin(RoomState state, Action.JoinStart action) {
            state.isJoining = true;
            state.isJoined = false;
            state.isJoinFailed = false;
            state.isCreating = false;
            state.isLeaving = false;
            state.feedbackText = "Joining room...";
            return state;
        }

        private RoomState JoinSuccess(RoomState state, Action.JoinSuccess action) {
            state.isJoining = false;
            state.isJoined = true;
            state.isJoinFailed = false;
            state.isCreating = false;
            state.isLeaving = false;
            state.feedbackText = "Joined room.";
            return state;
        }

        private RoomState JoinFailure(RoomState state, Action.JoinFailure action) {
            state.isJoining = false;
            state.isJoined = false;
            state.isJoinFailed = true;
            state.isCreating = false;
            state.isLeaving = false;
            state.feedbackText = "Joining room failed.";
            return state;
        }

        private RoomState StartCreate(RoomState state, Action.CreateRoomStart action) {
            state.isJoining = true;
            state.isJoined = false;
            state.isJoinFailed = false;
            state.isCreating = true;
            state.isLeaving = false;
            state.feedbackText = "Creating room...";
            return state;
        }

        private RoomState Leave(RoomState state, Action.LeaveStart action) {
            state.isJoining = false;
            state.isJoined = false;
            state.isJoinFailed = false;
            state.isLeaving = true;
            state.feedbackText = "Leaving room...";
            return state;
        }

        private RoomState LeaveSuccess(RoomState state, Action.LeaveSuccess action) {
            state.isJoining = false;
            state.isJoined = false;
            state.isJoinFailed = false;
            state.isLeaving = false;
            state.feedbackText = "Left room";
            return state;
        }
    }

    [Serializable]
    /// <summary>
    /// Public settings for api loading
    /// </summary>
    public class Settings {

		// #Critical: The first we try to do is to join a potential existing room.
		// If there is, good, else, we'll be called back with OnPhotonRandomJoinFailed()
		public bool shouldJoinRandomRoom = true;
    }
}
