using Redux;
using Redux.Middleware;
using System.Collections.Generic;
using UniRx;
using Hash = System.Collections.Generic.Dictionary<string, string>;
using HashEntry = System.Collections.Generic.KeyValuePair<string, string>;
using UnityEngine;


namespace Reduxity.Example.Zenject.ApiRequestCreator {
    public class Action {
        /// <summary>
        /// Properties for each type of http request
        /// </summary>

        /* Actions for starting an http request */
        public abstract class IQuery {
            public string url { get; set; }
            public Dictionary<string, string> query { get; set; }
            public Hash headers { get; set; }
        }
        public class Get: IQuery, IAction {}
        public class Post: IQuery, IAction {
            public byte[] postData { get; set; }
        }
        public class Put: IQuery, IAction {}
        public class Del: IQuery, IAction {}

    }

    public class ActionCreator : IActionCreator {
        /// <summary>
        /// Thunk that performs http request and dispatches an on success or failure action
        /// </summary>
        /// <returns>ThunkAction for Receive or ReceiveError</returns>
        public IAction Get(Action.Get action) {
            // return thunk to store, which will dispatch new actions upon success or failure
            return new ThunkAction<State> ((dispatch, getState) => {
                // dispatch first action to set state to loading
                dispatch(new ApiRequestor.Action.GetStart {});

                // use observable to subscribe and dispatch new actions from results
                ObservableWWW
                    .Get(action.url)
                    .Subscribe(
                        successText => {
                            // dispatch second action on success
                            // Debug.Log($"ApiRequestCreator.Get success => dispatching successText: {successText}");
                            dispatch(new ApiRequestor.Action.GetSuccess{
                                text = successText
                            });
                        },
                        failureError => {
                            // dispatch second action on failure
                            // Debug.Log($"ApiRequestCreator.Get failure => dispatching failureError: {failureError}");
                            dispatch(new ApiRequestor.Action.GetFailure{
                                error = failureError
                            });
                        }
                    );
            });
        }

        /// <summary>
        /// Thunk that performs http request and dispatches an on success or failure action
        /// </summary>
        /// <returns>ThunkAction for Receive or ReceiveError</returns>
        public IAction Post(Action.Post action) {
            // return thunk to store, which will dispatch new actions upon success or failure
            return new ThunkAction<State> ((dispatch, getState) => {
                // dispatch first action to set state to loading
                dispatch(new ApiRequestor.Action.PostStart {});

                // use observable to subscribe and dispatch new actions from results
                ObservableWWW
                    .Post(action.url, action.postData)
                    .Subscribe(
                        successText => {
                            // dispatch second action on success
                            dispatch(new ApiRequestor.Action.PostSuccess{
                                text = successText
                            });
                        },
                        failureError => {
                            // dispatch second action on failure
                            dispatch(new ApiRequestor.Action.PostFailure{
                                error = failureError
                            });
                        }
                    );
            });
        }

    }
}
