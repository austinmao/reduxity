using UnityEngine;

namespace Reduxity.Example.PlayerMovementLook {
	public class ReduxityInitializer : MonoBehaviour {

		// set up GameObject references needed for InitialState
		public CharacterController Player;
		public Camera Camera;
		public static Transform PlayerTransform;
		public static Transform CameraTransform;

		private void Awake () {
			PlayerTransform = Player.transform;
			CameraTransform = Camera.transform;

			App app = new App();
			app.Initialize();
		}

	}
}
