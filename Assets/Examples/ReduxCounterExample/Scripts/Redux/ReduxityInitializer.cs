using UnityEngine;

namespace Reduxity.Example.Counter {
	public class ReduxityInitializer : MonoBehaviour {

		private void Awake () {
			App app = new App();
			app.Initialize();
		}

	}
}
