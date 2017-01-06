using System;
using Zenject;

namespace Reduxity.Example.Zenject {
	public class NetworkSettings {

		[Serializable]
		public class GameSettings {

            /// <summary>
            /// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
            /// </summary>
			public string GameVersion;
		}
	}
}
