using UnityEngine;

public class State {
    public MoveState Movement { get; set; }
    public CounterState Counter { get; set; }

    /* default state at app start-up */
    public State initialize() {
        return new State {
            Movement = new MoveState {
                isMoving = false,
                distance = Vector3.zero
            },
            Counter = new CounterState {
                count = 0
            }
        };
    }
}

public class MoveState {
    public bool isMoving { get; set; }
    public Vector3 distance { get; set; }
}

public class CounterState {
    public int count { get; set; }
}
