using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Zenject;
using Reduxity;

namespace Reduxity.Example.Zenject {
	public class DisplayHttpText : IInitializable, IComponent {

		readonly App app_;
		readonly Text text_;

		public DisplayHttpText(
			App app,
			[Inject(Id = "HttpLogger")]
			Text text
		) {
			app_ = app;
			text_ = text;
		}

		public void Initialize() {
			RenderApiText();
			RenderApiError();
		}

		void RenderApiText() {
            app_.Store
                .Where(state => state.Api.isLoaded == true)
				.Select(ApiDataSelector.GetApiData)
                .Subscribe(result => {
					text_.text = result;
                })
                .AddTo(text_);
		}

		void RenderApiError() {
            app_.Store
                .Where(state => state.Api.isLoaded == true)
				.Where(state => state.Api.error != null)
				.Select(ApiDataSelector.GetApiError)
                .Subscribe(error => {
					text_.text = $"ERROR: {error.ToString()}";
                })
                .AddTo(text_);
		}
	}
}
