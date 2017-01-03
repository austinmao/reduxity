using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Zenject;
using Reduxity;

namespace Reduxity.Example.Zenject {
	public class DisplayHttpText : IInitializable, IComponent {

		readonly App app_;
		readonly Text textUI_; // bound by Zenject Binding (script) on GameObject
		readonly ApiDataSelector selector_;

		public DisplayHttpText(
			App app,
			[Inject(Id = "HttpLogger")]
			Text text,
			ApiDataSelector apiDataSelector
		) {
			app_ = app;
			textUI_ = text;
			selector_ = apiDataSelector;
		}

		public void Initialize() {
			RenderApiText();
			RenderApiError();
		}

		void RenderApiText() {
            app_.Store
                .Where(state => (state.Api.isLoaded && !state.Api.isError))
				.Select(selector_.GetApiData)
				.Where(text => text != null)
                .Subscribe(text => {
					Debug.Log($"DisplayHttpText => textUI_.text on success: {text}");
					textUI_.text = text;
                })
                .AddTo(textUI_);
		}

		void RenderApiError() {
            app_.Store
                .Where(state => state.Api.isError)
				.Select(selector_.GetApiError)
				.Where(error => error != null)
                .Subscribe(error => {
					Debug.Log($"DisplayHttpText => textUI_.text on error: {error}");
					textUI_.text = $"ERROR: {error}";
                })
                .AddTo(textUI_);
		}
	}
}
