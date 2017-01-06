// using UnityEngine;
// using Zenject;
// // using System;

// namespace Reduxity {

// 	/// <summary>
//     /// Reference template for observers that are attached to a GameObject
// 	/// and bound via Zenject binding script
//     /// </summary>
// 	public class ObserverTemplate : MonoBehaviour, IInitializable {
// 		App app_;
// 		// Settings settings_;

// 		[Inject]
// 		public void Construct(
// 			App app
// 			// Settings settings
// 		) {
// 			app_ = app;
// 			// settings_ = settings;
// 		}

// 		public void Initialize() {
// 			Debug.Log($"ObserverTemplate initialized with store: {ObjectDumper.Dump(app_.Store)}");
// 		}

// 		// [Serializable]
// 		// /// <summary>
// 		// /// </summary>
// 		// public class Settings {
// 		// }
// 	}
// }
