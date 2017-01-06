using UnityEngine;
using NUnit.Framework;
using Zenject;
using Moq;

namespace Reduxity.Example.Zenject.CharacterMover {

	[TestFixture]
	public class Test : ZenjectUnitTestFixture {

		DiContainer container_;
        private Action.Move mockMoveAction_;
        private Action.Turn mockTurnAction_;
        private CharacterState mockCharacterState_;

		[SetUp]
		public void Setup() {
			// create private container
			container_ = new DiContainer();
			container_.Bind<Reducer>().AsSingle();

			// set up mock settings
			var mockSettings = new Settings {
			};
			container_.Bind<Settings>().FromInstance(mockSettings);

			// stub move action
			mockMoveAction_ = new Action.Move {
				inputVelocity = Vector2.up.normalized,
				fixedDeltaTime = 1.0f
			};

            // stub turn action
            mockTurnAction_ = new Action.Turn {
                inputRotation = Vector2.up,
                fixedDeltaTime = 1.0f
            };

            // stub state condition
            mockCharacterState_ = new CharacterState {
                isMoving = false,
                isTurning = false,
                moveDistance = Vector3.zero,
                transformForward = Vector3.forward,
                transformRight = Vector3.right,
                localRotation = Quaternion.identity
			};
		}

		[Test]
		public void Should_set_isMoving_to_true_on_Move_action() {
			var sut = container_.Resolve<Reducer>();
			var result = sut.Reduce(mockCharacterState_, mockMoveAction_);
			Assert.IsTrue(result.isMoving);
		}

		[Test]
		public void Should_set_isTurning_to_false_on_Move_action() {
			var sut = container_.Resolve<Reducer>();
			var result = sut.Reduce(mockCharacterState_, mockMoveAction_);
			Assert.IsFalse(result.isTurning);
		}

		[Test]
		public void Should_set_isMoving_to_false_on_Stop_action() {
			var mockStopAction = new Action.StopMove();
            mockCharacterState_.isMoving = true;

			var sut = container_.Resolve<Reducer>();
            var result = sut.Reduce(mockCharacterState_, mockStopAction);

			Assert.IsFalse(result.isMoving);
		}

		[Test]
		public void Should_set_isTurning_to_true_on_Turn_action() {
			var sut = container_.Resolve<Reducer>();
			var result = sut.Reduce(mockCharacterState_, mockTurnAction_);
			Assert.IsTrue(result.isTurning);
		}

		[Test]
		public void Should_set_isMoving_to_false_on_Turn_action() {
			var sut = container_.Resolve<Reducer>();
			var result = sut.Reduce(mockCharacterState_, mockTurnAction_);
			Assert.IsFalse(result.isMoving);
		}

		[Test]
		public void Should_set_isTurning_to_false_on_Stop_action() {
			var mockStopAction = new Action.StopTurn();
            mockCharacterState_.isTurning = true;

			var sut = container_.Resolve<Reducer>();
            var result = sut.Reduce(mockCharacterState_, mockStopAction);

			Assert.IsFalse(result.isTurning);
		}
	}
}