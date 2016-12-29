using UnityEngine;
using NUnit.Framework;
using Zenject;
using Moq;

namespace Reduxity.Example.Zenject.Tests {

	[TestFixture]
	public class CharacterLookTest : ZenjectUnitTestFixture {

		DiContainer container_;

		public class Settings {
			public float maxViewAngle = 1;
			public float minViewAngle = -1;
			public int yLookSpeed = 1;
		}

		[SetUp]
		public void Setup() {
			container_ = new DiContainer();
			container_.Bind<Look.Reducer>().AsSingle();
		}

		[Test]
		public void Should_return_accurate_move_distance_from_move_reducer() {
            // Quaternion localRotation = state.localRotation;
            // Vector2 rotation = action.inputRotation;
            // float time = action.fixedDeltaTime;

            // // inputLook.y rotates the camera around the horizontal axis (with + being up)
            // Vector3 vertLook = rotation.y * time * Vector3.left * settings_.yLookSpeed;
            // Quaternion cameraLookRotation = localRotation * Quaternion.Euler(vertLook);

            // // We have to flip the signs and positions of min/max view angle here because the math
            // // uses the contradictory interpretation of our angles (+/- is down/up).
            // Quaternion clampedRotation = ClampRotationAroundXAxis_(cameraLookRotation, -settings_.maxViewAngle, -settings_.minViewAngle);

            // state.isLooking = true;
            // state.localRotation = clampedRotation;
            // Debug.Log($"in Look, returning state: {ObjectDumper.Dump(state)}");

			var mockLookAction = new Mock<Look.Action.Look>();
			mockLookAction.Setup(mock => mock.inputRotation).Returns(Vector2.up);
			mockLookAction.Setup(mock => mock.fixedDeltaTime).Returns(1.0f);
			// mockLookAction.SetupProperty(mock => mock.inputRotation, Vector2.up);
			// mockLookAction.SetupProperty(mock => mock.fixedDeltaTime, 1.0f);

			var mockCameraState = new Mock<CameraState>();
			mockCameraState.Setup(mock => mock.isLooking).Returns(false);
			mockCameraState.Setup(mock => mock.localRotation).Returns(new Quaternion());
			// mockCameraState.SetupProperty(mock => mock.isLooking, false);
			// mockCameraState.SetupProperty(mock => mock.localRotation, new Quaternion());

			var sut = container_.Resolve<Look.Reducer>();
			var result = sut.Reduce(mockCameraState.Object, mockLookAction.Object);

			Debug.Log(result);
			Assert.IsTrue(result.isLooking);
			// Assert.Equals(result.localRotation, Quaternion.Euler(1f,1f,1f));
		}

		[Test]
		public void Should_stop_on_stop_action() {
			var mockLookAction = new Mock<Look.Action.StopLook>();
			var mockCameraState = new Mock<CameraState>();
		}
	}
}