using UnityEngine.UI;
using System;
using Zenject;
using UniRx;
using UnityEngine;

namespace Reduxity.Example.Zenject {

    public class ConnectToCloudButtonComponent : IComponent, IInitializable {

		public enum ConnectStatus {
			notConnected,
			connecting,
			connected
		}

		readonly App app_;
		// readonly Settings settings_ = null;
		readonly Button button_;
        readonly Text buttonText_;
		// readonly Dispatcher dispatch_;

        public ConnectToCloudButtonComponent(
			App app,
			// Settings settings,
			[Inject(Id = "ConnectToCloud")]
			Button button,
            [Inject(Id = "ConnectToCloud")]
            Text buttonText
        ) {
			app_ = app;
			// dispatch_ = app.Store.Dispatch;
			button_ = button;
            buttonText_ = buttonText;
        }

		public void Initialize() {
			// HandleClick_(settings_.gameVersion);
			HandleClick_();
		}

		public void Render(ConnectStatus status) {
			// change button text depending on button status
			buttonText_.text = PrepareText_(status);

			// disable button if connecting
			button_.interactable = status == ConnectStatus.connecting ? false : true;
		}
		
		private string PrepareText_(ConnectStatus status) {
			switch (status) {
				case ConnectStatus.notConnected:
					return "Click to connect";
				case ConnectStatus.connecting:
					return "Connecting";
				case ConnectStatus.connected:
					return "Connected";
			}

			throw new Exception("no text provided");
		}

		// private void HandleClick_(string gameVersion) {
		private void HandleClick_() {
			button_.OnClickAsObservable()
				.Subscribe(_ => {
					Debug.Log("Dispatching Connect to Photon Cloud");
					var action = new CloudConnectCreator.Action.Connect {
						// gameVersion = gameVersion
					};
					app_.Store.Dispatch(action);
				})
				.AddTo(button_);
		}
    }
}
