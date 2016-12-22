using UnityEngine;
using UniRx;

namespace Reduxity.Counter {
    public class DisplayCount : MonoBehaviour {
        void Start() {
            renderCounter();
        }

        void renderCounter() {
            Debug.Log($"App.Store before counter subscription: {App.Store}");
            App.Store.Subscribe(store => {
                    Debug.Log($"going to change count to: {store.Counter.count}");
                })
                // clean up subscribable when game object is destroyed in order to not leak memory
                .AddTo(this);
        }
    }
}
