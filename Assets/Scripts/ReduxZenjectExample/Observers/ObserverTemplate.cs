using UnityEngine;
using Zenject;
using System;

namespace Reduxity.Example.Zenject {
	public class ObserverTemplate : MonoBehaviour, IInitializable {
		App app_;
		// Settings settings_;

		[Inject]
		public void Construct(
			App app
			// Settings settings
		) {
			app_ = app;
			// settings_ = settings;
		}

		public void Initialize() {
			Debug.Log($"ObserverTemplate initialized ith store: {app_.Store}");
		}

		// [Serializable]
		// /// <summary>
		// /// </summary>
		// public class Settings {
		// }
	}
}
