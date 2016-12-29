using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {

    /// <summary>
    /// </summary>
    public class ApiState : IState, IInitializable {
        public string data { get; set; }

        // /// <summary>
        // /// Data state constructor
        // /// </summary>
        // /// <param name="camera">Data GameObject injected via editor</param>
        // public DataState() {
        // }

        public void Initialize() {
            data = "";
        }
    }
}
