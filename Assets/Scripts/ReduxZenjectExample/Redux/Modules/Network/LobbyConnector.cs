using Redux;
using System;
using Photon;
using UnityEngine.UI;

namespace Reduxity.Example.Zenject.LobbyConnector {
    public class Action {
        /// <summary>
        /// Properties for each type of http request
        /// </summary>
        public abstract class ILobbyAction {
            public string feedbackText { get; set; }
            public string lobbyName { get; set; }
        }

        public class JoinStart : ILobbyAction, IAction {}
        public class JoinSuccess : ILobbyAction, IAction {}
        public class JoinFailure : ILobbyAction, IAction {}
        public class CreateLobbyStart : ILobbyAction, IAction {}
        public class CreateSuccess : ILobbyAction, IAction {}
        public class CreateFailure : ILobbyAction, IAction {}
        public class LeaveStart : ILobbyAction, IAction {}
        public class LeaveSuccess : ILobbyAction, IAction {}
        public class LeaveFailure : ILobbyAction, IAction {}
    }

    public class Reducer : IReducer {
        readonly Settings settings_;

        public Reducer(Settings settings) {
            settings_ = settings;
        }

        public LobbyState Reduce(LobbyState state, IAction action) {
            if (action is Action.JoinStart) {
                return StartJoin(state, (Action.JoinStart)action);
            }

            if (action is Action.JoinSuccess) {
                return JoinSuccess(state, (Action.JoinSuccess)action);
            }

            if (action is Action.JoinFailure) {
                return JoinFailure(state, (Action.JoinFailure)action);
            }

            if (action is Action.CreateLobbyStart) {
                return StartCreate(state, (Action.CreateLobbyStart)action);
            }

            if (action is Action.LeaveStart) {
                return Leave(state, (Action.LeaveStart)action);
            }

            if (action is Action.LeaveSuccess) {
                return LeaveSuccess(state, (Action.LeaveSuccess)action);
            }

            return state;
        }

        private LobbyState StartJoin(LobbyState state, Action.JoinStart action) {
            state.isJoining = true;
            state.isJoined = false;
            state.isJoinFailed = false;
            state.isCreating = false;
            state.isLeaving = false;
            state.feedbackText = "Joining lobby...";
            state.lobbyName = action.lobbyName;
            return state;
        }

        private LobbyState JoinSuccess(LobbyState state, Action.JoinSuccess action) {
            state.isJoining = false;
            state.isJoined = true;
            state.isJoinFailed = false;
            state.isCreating = false;
            state.isLeaving = false;
            state.feedbackText = "Joined lobby.";
            state.lobbyName = action.lobbyName;
            return state;
        }

        private LobbyState JoinFailure(LobbyState state, Action.JoinFailure action) {
            state.isJoining = false;
            state.isJoined = false;
            state.isJoinFailed = true;
            state.isCreating = false;
            state.isLeaving = false;
            state.feedbackText = "Joining lobby failed.";
            state.lobbyName = null;
            return state;
        }

        private LobbyState StartCreate(LobbyState state, Action.CreateLobbyStart action) {
            state.isJoining = true;
            state.isJoined = false;
            state.isJoinFailed = false;
            state.isCreating = true;
            state.isLeaving = false;
            state.feedbackText = "Creating lobby...";
            state.lobbyName = action.lobbyName;
            return state;
        }

        private LobbyState Leave(LobbyState state, Action.LeaveStart action) {
            state.isJoining = false;
            state.isJoined = false;
            state.isJoinFailed = false;
            state.isLeaving = true;
            state.feedbackText = "Leaving lobby...";
            state.lobbyName = action.lobbyName;
            return state;
        }

        private LobbyState LeaveSuccess(LobbyState state, Action.LeaveSuccess action) {
            state.isJoining = false;
            state.isJoined = false;
            state.isJoinFailed = false;
            state.isLeaving = false;
            state.feedbackText = "Left lobby";
            state.lobbyName = null;
            return state;
        }
    }

    [Serializable]
    /// <summary>
    /// Public settings for api loading
    /// </summary>
    public class Settings {

		// #Critical: The first we try to do is to join a potential existing lobby.
		// If there is, good, else, we'll be called back with OnPhotonRandomJoinFailed()
		public bool shouldJoinRandomLobby = true;
    }
}
