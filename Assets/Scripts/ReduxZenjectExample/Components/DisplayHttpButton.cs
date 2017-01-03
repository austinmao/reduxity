using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using System;

namespace Reduxity.Example.Zenject {
	public class DisplayHttpButton : IInitializable, IComponent {

		readonly App app_;
		readonly Text button_; // bound by Zenject Binding (script) on GameObject
		readonly ApiDataSelector selector_;
		readonly Settings settings_;

		public DisplayHttpButton(
			App app,
			[Inject(Id = "Submit")]
			Text text,
			ApiDataSelector apiDataSelector,
			Settings settings
		) {
			app_ = app;
			button_ = text;
			selector_ = apiDataSelector;
			settings_ = settings;
		}

		public void Initialize() {
			RenderLoading();
			RenderSuccess();
			RenderError();
		}

		/// <summary>
        /// set state to loading text if it is not loaded
        /// </summary>
		void RenderLoading() {
			app_.Store
				.Where(state => state.Api.isLoading)
				.Subscribe(_ => {
					Debug.Log($"DisplayHttpButton.RenderingLoading");
					button_.text = settings_.loadingText;
				})
				.AddTo(button_);
		}

		/// <summary>
        /// set text to state if there is text; otherwise, to defaults
        /// </summary>
		void RenderSuccess() {
            app_.Store
				.Where(state => (state.Api.isLoaded && !state.Api.isError))
				.Select(selector_.GetApiData)
                .Subscribe(text => {
					Debug.Log($"DisplayHttpButton.RenderSuccess => {text}");
					button_.text = text != null ? text : settings_.defaultText;
                })
                .AddTo(button_);
		}

		/// <summary>
        /// set error text if api is loaded and there is an error
        /// </summary>
		void RenderError() {
            app_.Store
				.Where(state => state.Api.isError)
				.Select(selector_.GetApiError)
				.Subscribe(error => {
					Debug.Log($"DisplayHttpButton.RenderError => {error}");
					button_.text = $"{settings_.errorPrefixText} {error}";
				})
				.AddTo(button_);
		}


		[Serializable]
		/// <summary>
		/// Default text to display for submit button.
		/// </summary>
		public class Settings {
			public string defaultText = "Submit";
			public string loadingText = "Loading...";
			public string errorPrefixText = "Error: ";
		}
	}
}
