// using UnityEngine;

// namespace Reduxity.Example.Zenject {
// 	public class ReduxityInitializer : MonoBehaviour {

// 		readonly App app_;

// 		// set up GameObject references needed for InitialState
// 		public CharacterController Player;
// 		public Camera Camera;

// 		public ReduxityInitializer(App app) {
// 			app_ = app;
// 		}

// 		public static ReduxityInitializer Instance { get; private set; }

// 		private void Awake () {
// 			// create Reduxity settings singleton
// 			Instance = this;

// 			app_.Initialize();
// 		}

// 	}
// }
