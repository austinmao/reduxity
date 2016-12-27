// using System;
// using UnityEngine;
// using Zenject;

// namespace Reduxity.Example.Zenject {

//     [RequireComponent(typeof(CharacterController))]
// 	public class Player : MonoBehaviour {

//         // CharacterController character_; // bound through ZenjectBinding script on GameObject
// 		Player player_;
// 		// Settings settings_;

// 		[Inject]
// 		public void Construct(Player player) {
// 		// public void Construct(Player player, CharacterController character) {
// 		// public void Construct(CharacterController character, Settings settings) {
// 			player_ = player;
// 			// character_ = character;
// 			// settings_ = settings; 
// 		}

// 		// public Vector3 Position {
// 		// 	get { return transform.position; }
// 		// 	set { transform.position = value; }
// 		// }
		
// 		// [Serializable]
// 		// public class Settings {
// 		// }

// 		// public class Factory : Factory<Cube> {}
// 	}
// }
