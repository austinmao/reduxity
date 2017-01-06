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
		readonly CloudConnectCreator.ActionCreator actionCreator_;
		readonly Button button_;
        readonly Text buttonText_;

        public ConnectToCloudButtonComponent(
			App app,
			// Settings settings,
			CloudConnectCreator.ActionCreator actionCreator,
			[Inject(Id = "ConnectToCloud")]
			Button button,
            [Inject(Id = "ConnectToCloud")]
            Text buttonText
        ) {
			app_ = app;
			actionCreator_ = actionCreator;
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
					var action = new CloudConnectCreator.Action.Connect {
						gameVersion = "0.1" // TODO: make this settable in settings
					};

					// dispatch action creator that starts connect to cloud
					Debug.Log("Dispatching Connect to ActionCreator");
					app_.Store.Dispatch(actionCreator_.Connect(action));
				})
				.AddTo(button_);
		}
    }
}
