using UnityEngine;

public static class CharacterMoverSelector {
	public static Vector3 GetMoveDistance(State state) {
		return state.Movement.distance;
	}
}
