using System;
using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {

    [RequireComponent(typeof(CharacterController))]
	public class Player : MonoBehaviour {

		Settings settings_;
        CharacterController character_; // bound through ZenjectBinding script on GameObject

		[Inject]
		public void Construct(Settings settings) {
			settings_ = settings; 
			character_ = character;
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
