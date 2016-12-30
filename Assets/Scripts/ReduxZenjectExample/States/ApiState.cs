using UnityEngine;
using Zenject;

namespace Reduxity.Example.Zenject {

    /// <summary>
    /// </summary>
    public class ApiState : IState {
        public string data { get; set; }
    }

    public class ApiStateInitializer : IInitializable {
        readonly ApiState apiState_;

        public ApiStateInitializer(ApiState apiState) {
            apiState_ = apiState;
        }

        public void Initialize() {
            apiState_.data = "";
        }
    }
}
