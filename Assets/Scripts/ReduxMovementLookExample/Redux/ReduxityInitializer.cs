using UnityEngine;

namespace Reduxity.Example.PlayerMovementLook {
	public class ReduxityInitializer : MonoBehaviour {

		private void Awake () {
			App app = new App();
			app.Initialize();
		}

	}
}
