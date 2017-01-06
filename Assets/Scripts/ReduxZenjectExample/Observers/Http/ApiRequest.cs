
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;
using System;
using UnityEngine.UI;

namespace Reduxity.Example.Zenject {
    public class ApiRequest : IInitializable {

        readonly App app_;
        readonly Button button_;
        readonly ApiRequestCreator.ActionCreator actionCreator_;
		readonly Settings settings_;

        public ApiRequest(
            App app,
			[Inject(Id = "Submit")]
            Button button,
            ApiRequestCreator.ActionCreator actionCreator,
			Settings settings
        ) {
            app_ = app;
            button_ = button;
            actionCreator_ = actionCreator;
			settings_ = settings;
        }

        public void Initialize() {
            dispatchGetRequest();
        }

        void dispatchGetRequest() {
            button_.OnClickAsObservable()
                .Subscribe(_ => {
                    var action = new ApiRequestCreator.Action.Get {
                        url = settings_.defaultUrl
                        // query = {},
                        // headers = {}
                    };

                    // dispatch thunk, which returns an IAction to dispatch to store
                    // on success or failure
                    // Debug.Log($"Dispatching: {ObjectDumper.Dump(action)}");
                    app_.Store.Dispatch(actionCreator_.Get(action));
                })
                .AddTo(button_);
        }

		[Serializable]
		/// <summary>
		/// Default text to display for submit button.
		/// </summary>
		public class Settings {
			public string defaultUrl = "https://api.github.com/repos/austinmao/reduxity";
		}
    }
}