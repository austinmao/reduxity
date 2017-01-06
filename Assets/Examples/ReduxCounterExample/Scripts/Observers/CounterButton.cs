using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Reduxity.Example.Counter {
    public class CounterButton : MonoBehaviour {
        void Start() {
            observeAndDispatchButtonPressed();
        }

        void observeAndDispatchButtonPressed() {
                App.Store.Dispatch(
                    // dispatch counter action
                    new Action.Increment {}
                );
            }
        }
}
