
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;
using System;
using UnityEngine.UI;

namespace Reduxity.Example.Zenject {
    public class ApiButton : IInitializable {

        readonly App app_;
        readonly Button button_;

        public ApiButton(
            App app,
			[Inject(Id = "Submit")]
            Button button
        ) {
            app_ = app;
            button_ = button;
        }

        public void Initialize() {
            RequestApi();
        }

        private void RequestApi() {
            button_.OnClickAsObservable()
                .Subscribe(_ => {
                    var action = new ApiRequestCreator.Action.Get {
                        url = "https://www.google.com"
                        // query = {},
                        // headers = {}
                    };

                    // app_.Store.Dispatch(
                    //     new ApiRequestCreator.ActionCreator.Reduce(app_.Store, )
                    // );
                });
        }
    }
}