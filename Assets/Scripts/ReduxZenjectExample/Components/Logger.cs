// using UnityEngine;
// using UnityEngine.UI;
// using UniRx;
// using Zenject;
// using System;

// namespace Reduxity.Example.Zenject {
// 	public class Logger : IInitializable, IComponent {

// 		readonly App app_;
// 		readonly Settings settings_;

// 		public Logger(
// 			App app,
// 			Settings settings
// 		) {
// 			app_ = app;
// 			settings_ = settings;
// 		}

// 		public void Initialize() {
// 			// LogPunFeedback();
// 		}

// 		/// <summary>
//         /// set state to loading text if it is not loaded
//         /// </summary>
// 		void LogPunFeedback() {
// 			// app_.Store
// 			// 	.Where(state => state.Network.feedbackText != null)
// 			// 	.Subscribe(_ => {
// 			// 		// Debug.Log($"DisplayHttpButton.RenderingLoading");
// 			// 		button_.text = settings_.loadingText;
// 			// 	})
// 			// 	.AddTo(button_);
// 		}

// 		[Serializable]
// 		/// <summary>
// 		/// Settings for logger
// 		/// </summary>
// 		public class Settings {
// 		}
// 	}
// }
