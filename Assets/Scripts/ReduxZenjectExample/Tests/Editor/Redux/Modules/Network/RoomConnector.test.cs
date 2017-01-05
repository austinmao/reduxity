using NUnit.Framework;
using Zenject;

namespace Reduxity.Example.Zenject.RoomConnector.Tests
{

    [TestFixture]
	public class Test : ZenjectUnitTestFixture {

		DiContainer container_;
		private RoomState mockRoomState_;
		private Reducer reducer_;

		[SetUp]
		public void Setup() {
			// create private container
			container_ = new DiContainer();
			container_.Bind<Reducer>().AsSingle();

			// set up mock settings
			var mockSettings = new Settings {
			};
			container_.Bind<Settings>().FromInstance(mockSettings);

			// set default state
			mockRoomState_ = new RoomState {};
			reducer_ = container_.Resolve<Reducer>();
		}

		[Test]
		public void Should_set_isJoining_and_isJoined_when_starting_request() {
			// arrange
			var mockRoomAction = new Action.JoinStart {};
			var sut = reducer_;

			// act
			var result = sut.Reduce(mockRoomState_, mockRoomAction);

			// assert
			Assert.IsFalse(result.isJoined);
			Assert.IsTrue(result.isJoining);
			Assert.IsFalse(result.isJoinFailed);
			Assert.IsFalse(result.isCreating);
			Assert.IsFalse(result.isLeaving);
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }

		[Test]
		public void Should_set_Room_state_when_joining_succeeds() {
            // arrange
			var mockRoomAction = new Action.JoinSuccess {
			};

            // act
			var result = reducer_.Reduce(mockRoomState_, mockRoomAction);

            // assert
			Assert.IsTrue(result.isJoined);
			Assert.IsFalse(result.isJoining);
			Assert.IsFalse(result.isJoinFailed);
			Assert.IsFalse(result.isCreating);
			Assert.IsFalse(result.isLeaving);
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }

		[Test]
		public void Should_set_isLeaving_and_isJoined_when_starting_request() {
			// arrange
			var mockRoomAction = new Action.LeaveStart {};
			var sut = reducer_;

			// act
			var result = sut.Reduce(mockRoomState_, mockRoomAction);

			// assert
			Assert.IsFalse(result.isJoined);
			Assert.IsFalse(result.isJoining);
			Assert.IsTrue(result.isLeaving);
			Assert.IsFalse(result.isCreating);
			Assert.IsFalse(result.isJoinFailed);
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }

		[Test]
		public void Should_set_isJoining_and_isCreating_when_creating_starts() {
			// arrange
			var mockRoomAction = new Action.CreateStart {};
			var sut = reducer_;

			// act
			var result = sut.Reduce(mockRoomState_, mockRoomAction);

			// assert
			Assert.IsFalse(result.isJoined);
			Assert.IsTrue(result.isJoining);
			Assert.IsFalse(result.isJoinFailed);
			Assert.IsTrue(result.isCreating);
			Assert.IsFalse(result.isLeaving);
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }

		[Test]
		public void Should_set_Room_state_when_leaving_succeeds() {
            // arrange
			var mockRoomAction = new Action.LeaveSuccess {
			};

            // act
			var result = reducer_.Reduce(mockRoomState_, mockRoomAction);

            // assert
			Assert.IsFalse(result.isJoined);
			Assert.IsFalse(result.isJoining);
			Assert.IsFalse(result.isLeaving);
			Assert.IsFalse(result.isCreating);
			Assert.IsFalse(result.isJoinFailed);
			Assert.IsFalse(result.isLeaving);
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }
    }
}