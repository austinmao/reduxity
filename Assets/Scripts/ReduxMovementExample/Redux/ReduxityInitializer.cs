using UnityEngine;

namespace Reduxity.Example.PlayerMovement {
	public class ReduxityInitializer : MonoBehaviour {

		private void Awake () {
			App app = new App();
			app.Initialize();
		}

	}
}
