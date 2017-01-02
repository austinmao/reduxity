using UnityEngine;
using System;

namespace Reduxity.Example.Zenject {
	public static class ApiDataSelector {
		public static string GetApiData(State state) {
			return state.Api.text;
		}

		public static Exception GetApiError(State state) {
			return state.Api.error;
		}
	}
}
