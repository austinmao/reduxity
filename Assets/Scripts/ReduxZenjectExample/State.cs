using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {

    public class State : IInitializable {
        [Inject] public MoveState Movement { get; set; }
        [Inject] public CounterState Counter { get; set; }

        public void Initialize() {
            new MoveState {
                isMoving = false,
                distance = Vector3.zero
            };
            new CounterState {
                count = 0
            };
        }
    }

    public class MoveState {
        public bool isMoving { get; set; }
        public Vector3 distance { get; set; }

        public void Initialize() {
            isMoving = false;
            distance = Vector3.zero;
        }
    }

    public class CounterState : IInitializable {
        public int count { get; set; }

        public void Initialize() {
            count = 0;
        }
    }
}
