using UnityEngine;
using UniRx;
using Zenject;
using System;
using ModestTree;

namespace Reduxity.Example.Zenject {

    public class RequestApi : IInitializable {

        readonly App app_;

        public RequestApi(App app) {
            app_ = app;
        }

        public void Initialize() {
            renderData();
        }

        void renderData() {
            app_.Store
                .Where(state => state.Api.isLoaded)
				.Select(ApiDataSelector.GetApiData)
                .Subscribe(text => {
                    Debug.Log($"going to move character by: {text}");
                });
                // .AddTo(this);
        }

        [Serializable]
        public class Settings {
        }
    }
}
