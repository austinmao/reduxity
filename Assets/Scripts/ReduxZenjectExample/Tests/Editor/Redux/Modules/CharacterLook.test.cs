// using UnityEngine;
// using NUnit.Framework;
// using Zenject;

// namespace Reduxity.Example.Zenject.Tests {

// 	[TestFixture]
// 	public class CharacterLookTest : ZenjectUnitTestFixture {

// 		DiContainer container_;

// 		private class Settings {
// 			float maxViewAngle = 60f;
// 			float minViewAngle = -60f;
// 			int yLookSpeed = 100;
// 		}

// 		[SetUp]
// 		public void Setup() {
// 			container_ = new DiContainer();
// 			container_.Bind<Settings>().FromMock();
// 		}

// 		[Test]
// 		public void Should_return_accurate_move_distance_from_move_reducer() {
// 			Action.Move mockMoveAction = new Action.Move {
// 				inputVelocity = Vector2.up.normalized,
// 				playerTransform = mockGameObject_.transform,
// 				fixedDeltaTime = 1.0f
// 			};
// 			App.Store.Dispatch(mockMoveAction);

// 			State currentState = GetCurrentState();
// 			Assert.IsTrue(currentState.Movement.isMoving);
// 			Assert.AreEqual(currentState.Movement.distance, Vector3.forward);
// 		}

// 		[Test]
// 		public void Should_stop_on_stop_action() {
// 			Action.Stop mockStopAction = new Action.Stop {};
// 			App.Store.Dispatch(mockStopAction);

// 			State currentState = GetCurrentState();
// 			Assert.IsFalse(currentState.Movement.isMoving);
// 		}

// 		private State GetCurrentState() {
// 			return App.Store.GetState();
// 		}
// 	}
// }