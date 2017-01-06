using UnityEngine;
using System;

namespace Reduxity.Example.Zenject {
	public class ApiDataSelector {
		public string GetApiData(State state) {
			return state.Api.text;
		}

		public Exception GetApiError(State state) {
			return state.Api.error;
		}
	}
}
