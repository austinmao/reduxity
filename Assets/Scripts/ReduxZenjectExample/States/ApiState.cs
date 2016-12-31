using UnityEngine;
using Zenject;
using System;
using System.Collections.Generic;

namespace Reduxity.Example.Zenject {

    /// <summary>
    /// </summary>
    public class ApiState : IState {
        public bool isLoading { get; set; }
        public bool isLoaded { get; set; }
        public string text { get; set; }
        public Exception error { get; set; }
    }

    public class ApiStateInitializer : IStateInitializer, IInitializable {
        readonly ApiState apiState_;

        public ApiStateInitializer(ApiState apiState) {
            apiState_ = apiState;
        }

        public void Initialize() {
            apiState_.isLoading = false;
            apiState_.isLoaded = false;
            apiState_.text = "";
            apiState_.error = null;
        }
    }
}
