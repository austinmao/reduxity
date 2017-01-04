using UnityEngine;
using Redux;
using Redux.Middleware;
using System;
using Hash = System.Collections.Generic.Dictionary<string, string>;
using HashEntry = System.Collections.Generic.KeyValuePair<string, string>;


namespace Reduxity.Example.Zenject.ApiRequestor {
    public class Action {
        /// <summary>
        /// Properties for each type of http request
        /// </summary>

        /* Actions for starting an http request */
        public abstract class IQueryStart {}

        public class GetStart : IQueryStart, IAction {}
        public class PostStart : IQueryStart, IAction {}
        public class PutStart : IQueryStart, IAction {}
        public class DelStart : IQueryStart, IAction {}

        /* Actions for succeeding with an http request */
        public abstract class IQuerySuccess {
            public string text { get; set; }
        }
        public class GetSuccess : IQuerySuccess, IAction {}
        public class PostSuccess : IQuerySuccess, IAction {}
        public class PutSuccess : IQuerySuccess, IAction {}
        public class DelSuccess : IQuerySuccess, IAction {}

        /* Actions for failing an http request */
        public abstract class IQueryFailure {
            public Exception error { get; set; }
        }
        public class GetFailure : IQueryFailure, IAction {}
        public class PostFailure : IQueryFailure, IAction {}
        public class PutFailure : IQueryFailure, IAction {}
        public class DelFailure : IQueryFailure, IAction {}
    }

    public class Reducer : IReducer {
        readonly Settings settings_;

        public Reducer(Settings settings) {
            settings_ = settings;
        }

        public ApiState Reduce(ApiState state, IAction action) {
            // start request for any http request
            if (action is Action.IQueryStart) {
                return StartRequest(state, (Action.IQueryStart)action);
            }

            if (action is Action.IQuerySuccess) {
                return RequestSuccess(state, (Action.IQuerySuccess)action);
            }

            if (action is Action.IQueryFailure) {
                return RequestFailure(state, (Action.IQueryFailure)action);
            }

            return state;
        }

        private ApiState StartRequest(ApiState state, Action.IQueryStart action) {
            state.isLoaded = false;
            state.isLoading = true;
            state.isError = false;
            return state;
        }

        private ApiState RequestSuccess(ApiState state, Action.IQuerySuccess action) {
            state.isLoaded = true;
            state.isLoading = false;
            state.isError = false;
            state.text = action.text;
            state.error = null;
            return state;
        }

        private ApiState RequestFailure(ApiState state, Action.IQueryFailure action) {
            state.isLoaded = true;
            state.isLoading = false;
            state.isError = true;
            state.text = null;
            state.error = action.error;
            return state;
        }
    }

    [Serializable]
    /// <summary>
    /// Public settings for api loading
    /// </summary>
    public class Settings {
        // public int retries = 3;
    }
}
