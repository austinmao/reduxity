using UnityEngine;

namespace Reduxity.Example.Zenject {
	public class CharacterMoverSelector {
		public Vector3 GetMoveDistance(State state) {
			return state.Character.moveDistance;
		}

		public Quaternion GetTurnRotation(State state) {
			return state.Character.localRotation;
		}
	}
}
