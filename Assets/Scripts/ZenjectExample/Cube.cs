using System;
using UnityEngine;
using Zenject;

namespace ZenjectTest {
	public class Cube : MonoBehaviour {

		Rigidbody rigidBody_;
		Settings settings_;

		[Inject]
		public void Construct() {
			rigidBody_ = GetComponent<Rigidbody>();
		}

		public Vector3 Position {
			get { return transform.position; }
			set { transform.position = value; }
		}
		
		public void FixedTick() {
			rigidBody_.velocity *= settings_.maxSpeed;
		}

		public void Tick() {
		}

		[Serializable]
		public class Settings {
			public float maxSpeed;
		}

		public class Factory : Factory<Cube> {}
	}
}
