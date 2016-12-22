using UnityEngine;

namespace Reduxity {
	public static class CharacterMoverSelector {
		public static Vector3 GetMoveDistance(State state) {
			return state.Movement.distance;
		}
	}
}
