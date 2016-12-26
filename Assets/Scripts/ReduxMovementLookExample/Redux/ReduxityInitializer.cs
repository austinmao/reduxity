using UnityEngine;

namespace Reduxity.Example.PlayerMovementLook {
	public class ReduxityInitializer : MonoBehaviour {

		// set up GameObject references needed for InitialState
		public CharacterController Player;
		public Camera Camera;
		[RangeAttribute(-90, 0)]
		public float maxViewAngle = 60f;
		[RangeAttribute(0, 90)]
		public float minViewAngle = -60f;
		public int lookSpeed = 100;
		public static Transform PlayerTransform;
		public static Transform CameraTransform;
		public static ReduxityInitializer Instance { get; private set; }

		private void Awake () {
			Instance = this;

			PlayerTransform = Player.transform;
			CameraTransform = Camera.transform;

			App app = new App();
			app.Initialize();
		}

	}
}
