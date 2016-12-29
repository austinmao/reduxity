using UnityEngine;

namespace Reduxity.Example.Counter {
    public class State {
        public CounterState Counter { get; set; }

        /* default state at app start-up */
        public State Initialize() {
            return new State {
                Counter = new CounterState {
                    count = 0
                }
            };
        }
    }

    public class CounterState : IState {
        public int count { get; set; }
    }
}
