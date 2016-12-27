using System;
using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {

    [RequireComponent(typeof(CharacterController))]
	public class Player : MonoBehaviour {

        CharacterController character_; // bound through ZenjectBinding script on GameObject
		Settings settings_;

		[Inject]
		public void Construct(CharacterController character, Settings settings) {
			character_ = character;
			settings_ = settings; 
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
