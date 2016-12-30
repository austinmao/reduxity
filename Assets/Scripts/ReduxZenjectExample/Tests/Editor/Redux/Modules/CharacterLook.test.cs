using UnityEngine;
using NUnit.Framework;
using Zenject;
using Moq;

namespace Reduxity.Example.Zenject.CharacterLook.Tests {

	[TestFixture]
	public class CharacterLookTest : ZenjectUnitTestFixture {

		DiContainer container_;

		[SetUp]
		public void Setup() {
			container_ = new DiContainer();
			container_.Bind<CharacterLook.Reducer>().AsSingle();

			// set up mock settings
			var mockSettings = new Settings {
				maxViewAngle = 1,
				minViewAngle = -1,
				yLookSpeed = 1
			};
			container_.Bind<Settings>().FromInstance(mockSettings);
		}

		[Test]
		public void Should_return_accurate_move_distance_from_move_reducer() {
			var mockLookAction = new CharacterLook.Action.Look {
				inputRotation = Vector2.up,
				fixedDeltaTime = 1.0f
			};

			var mockCameraState = new CameraState {
				isLooking = false,
				localRotation = new Quaternion(1f,1f,1f,1f)
			};

			var sut = container_.Resolve<CharacterLook.Reducer>();
			var result = sut.Reduce(mockCameraState, mockLookAction);

			Debug.Log(result);
			Assert.IsTrue(result.isLooking);
			// Assert.Equals(result.localRotation, Quaternion.Euler(1f,1f,1f));
		}

		[Test]
		public void Should_stop_on_stop_action() {
			var mockLookAction = new CharacterLook.Action.StopLook();
			var mockCameraState = new CameraState {
				isLooking = true
			};
			var sut = container_.Resolve<CharacterLook.Reducer>();
			var result = sut.Reduce(mockCameraState, mockLookAction);

			Assert.IsFalse(result.isLooking);

		}
	}
}