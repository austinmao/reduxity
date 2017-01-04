using Redux;
using System;
using Photon;
using UnityEngine.UI;

namespace Reduxity.Example.Zenject.RoomRequestor {
    public class Action {
        /// <summary>
        /// Properties for each type of http request
        /// </summary>
        public class Join : IAction {}
        public class JoinSuccess : IAction {}
        public class JoinFailure : IAction {}
        public class Leave : IAction {}
        public class LeaveSuccess : IAction {}
    }

    public class Reducer : IReducer {
        readonly Settings settings_;

        public Reducer(Settings settings) {
            settings_ = settings;
        }

        public RoomState Reduce(RoomState state, IAction action) {
            if (action is Action.Join) {
                return StartJoin(state, (Action.Join)action);
            }

            if (action is Action.JoinSuccess) {
                return JoinSuccess(state, (Action.JoinSuccess)action);
            }

            if (action is Action.Join) {
                return JoinFailure(state, (Action.JoinFailure)action);
            }

            if (action is Action.Leave) {
                return StartLeav(state, (Action.Leave)action);
            }

            if (action is Action.LeaveSuccess) {
                return LeaveSuccess(state, (Action.LeaveSuccess)action);
            }

            return state;
        }

        private RoomState StartJoin(RoomState state, Action.Join action) {
            state.isJoining = true;
            state.isJoined = false;
            state.isJoinFailed = false;
            state.isLeaving = false;
            state.feedbackText = "Joining...";
            return state;
        }

        private RoomState JoinSuccess(RoomState state, Action.JoinSuccess action) {
            state.isJoining = false;
            state.isJoined = true;
            state.isJoinFailed = false;
            state.isLeaving = false;
            state.feedbackText = "Joining room...";
            return state;
        }

        private RoomState JoinFailure(RoomState state, Action.JoinFailure action) {
            state.isJoining = false;
            state.isJoined = false;
            state.isJoinFailed = true;
            state.isLeaving = false;
            state.feedbackText = "Joining failed.";
            return state;
        }

        private RoomState StartLeav(RoomState state, Action.Leave action) {
            state.isJoining = false;
            state.isJoined = false;
            state.isJoinFailed = false;
            state.isLeaving = true;
            state.feedbackText = "Leaving...";
            return state;
        }

        private RoomState LeaveSuccess(RoomState state, Action.LeaveSuccess action) {
            state.isJoining = false;
            state.isJoined = false;
            state.isJoinFailed = false;
            state.isLeaving = false;
            state.feedbackText = "Leaving successful.";
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
		bool shouldJoinRandomRoom = true;
    }
}
