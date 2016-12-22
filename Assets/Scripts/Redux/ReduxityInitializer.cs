using UnityEngine;

namespace Reduxity {
	public class ReduxityInitializer : MonoBehaviour {

		private void Awake () {
			App app = new App();
			app.Initialize();
		}

	}
}
