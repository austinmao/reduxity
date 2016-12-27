using System;
using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {

    [RequireComponent(typeof(Camera))]
	public class Camera : MonoBehaviour {

        Camera camera_; // bound through ZenjectBinding script on GameObject
		Settings settings_;

		[Inject]
		public void Construct(Settings settings) {
			settings_ = settings; 
            camera_ = camera;
		}

		// public Vector3 Position {
		// 	get { return transform.position; }
		// 	set { transform.position = value; }
		// }
		
		[Serializable]
		public class Settings {
		}

		// public class Factory : Factory<Cube> {}
	}
}
