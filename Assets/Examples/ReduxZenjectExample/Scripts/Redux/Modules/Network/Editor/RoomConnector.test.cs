using NUnit.Framework;
using Zenject;

namespace Reduxity.Example.Zenject.RoomConnector
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

			// set default state
			mockRoomState_ = new RoomState {};
			reducer_ = container_.Resolve<Reducer>();
		}

		[Test]
		public void Should_set_isJoining_and_isJoined_when_joining() {
			// arrange
			var mockRoomAction = new Action.JoinStart {};
			var sut = reducer_;

			// act
			var result = sut.Reduce(mockRoomState_, mockRoomAction);

			// assert
			Assert.IsFalse(result.isJoined, "isJoined");
			Assert.IsTrue(result.isJoining, "isJoining");
			Assert.IsFalse(result.isJoinFailed, "isJoinFailed");
			Assert.IsFalse(result.isCreating, "isCreating");
			Assert.IsFalse(result.isCreated, "isCreated");
			Assert.IsFalse(result.isCreateFailed, "isCreateFailed");
			Assert.IsFalse(result.isLeaving, "isLeaving");
			Assert.IsFalse(result.hasLeft, "hasLeft");
			Assert.IsFalse(result.isLeavingFailed, "isLeavingFailed");
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }

		[Test]
		public void Should_set_isJoined_when_joining_succeeds() {
            // arrange
			var mockRoomAction = new Action.JoinSuccess {
			};

            // act
			var result = reducer_.Reduce(mockRoomState_, mockRoomAction);

            // assert
			Assert.IsTrue(result.isJoined, "isJoined");
			Assert.IsFalse(result.isJoining, "isJoining");
			Assert.IsFalse(result.isJoinFailed, "isJoinFailed");
			Assert.IsFalse(result.isCreating, "isCreating");
			Assert.IsFalse(result.isCreated, "isCreated");
			Assert.IsFalse(result.isCreateFailed, "isCreateFailed");
			Assert.IsFalse(result.isLeaving, "isLeaving");
			Assert.IsFalse(result.hasLeft, "hasLeft");
			Assert.IsFalse(result.isLeavingFailed, "isLeavingFailed");
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }

		[Test]
		public void Should_set_isJoined_when_joining_fails() {
            // arrange
			var mockRoomAction = new Action.JoinFailure {
			};

            // act
			var result = reducer_.Reduce(mockRoomState_, mockRoomAction);

            // assert
			Assert.IsFalse(result.isJoined, "isJoined");
			Assert.IsFalse(result.isJoining, "isJoining");
			Assert.IsTrue(result.isJoinFailed, "isJoinFailed");
			Assert.IsFalse(result.isCreating, "isCreating");
			Assert.IsFalse(result.isCreated, "isCreated");
			Assert.IsFalse(result.isCreateFailed, "isCreateFailed");
			Assert.IsFalse(result.isLeaving, "isLeaving");
			Assert.IsFalse(result.hasLeft, "hasLeft");
			Assert.IsFalse(result.isLeavingFailed, "isLeavingFailed");
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }

		[Test]
		public void Should_set_isLeaving_and_isJoined_when_leaving() {
			// arrange
			var mockState = new RoomState {
				isJoined = true
			};
			var mockRoomAction = new Action.LeaveStart {};
			var sut = reducer_;

			// act
			var result = sut.Reduce(mockState, mockRoomAction);

			// assert
			Assert.IsTrue(result.isJoined, "isJoined");
			Assert.IsFalse(result.isJoining, "isJoining");
			Assert.IsFalse(result.isJoinFailed, "isJoinFailed");
			Assert.IsFalse(result.isCreating, "isCreating");
			Assert.IsFalse(result.isCreated, "isCreated");
			Assert.IsFalse(result.isCreateFailed, "isCreateFailed");
			Assert.IsTrue(result.isLeaving, "isLeaving");
			Assert.IsFalse(result.hasLeft, "hasLeft");
			Assert.IsFalse(result.isLeavingFailed, "isLeavingFailed");
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }

		[Test]
		public void Should_set_hasLeft_when_leaving_succeeds() {
            // arrange
			var mockRoomAction = new Action.LeaveSuccess {
			};

            // act
			var result = reducer_.Reduce(mockRoomState_, mockRoomAction);

            // assert
			Assert.IsFalse(result.isJoined, "isJoined");
			Assert.IsFalse(result.isJoining, "isJoining");
			Assert.IsFalse(result.isJoinFailed, "isJoinFailed");
			Assert.IsFalse(result.isCreating, "isCreating");
			Assert.IsFalse(result.isCreated, "isCreated");
			Assert.IsFalse(result.isCreateFailed, "isCreateFailed");
			Assert.IsFalse(result.isLeaving, "isLeaving");
			Assert.IsTrue(result.hasLeft, "hasLeft");
			Assert.IsFalse(result.isLeavingFailed, "isLeavingFailed");
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }

		[Test]
		public void Should_stay_in_room_when_leaving_fails() {
            // arrange
			var mockRoomAction = new Action.LeaveFailure {
			};

            // act
			var result = reducer_.Reduce(mockRoomState_, mockRoomAction);

            // assert
			Assert.IsTrue(result.isJoined, "isJoined");
			Assert.IsFalse(result.isJoining, "isJoining");
			Assert.IsFalse(result.isJoinFailed, "isJoinFailed");
			Assert.IsFalse(result.isCreating, "isCreating");
			Assert.IsFalse(result.isCreated, "isCreated");
			Assert.IsFalse(result.isCreateFailed, "isCreateFailed");
			Assert.IsFalse(result.isLeaving, "isLeaving");
			Assert.IsFalse(result.hasLeft, "hasLeft");
			Assert.IsTrue(result.isLeavingFailed, "isLeavingFailed");
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
			Assert.IsFalse(result.isJoined, "isJoined");
			Assert.IsTrue(result.isJoining, "isJoining");
			Assert.IsFalse(result.isJoinFailed, "isJoinFailed");
			Assert.IsTrue(result.isCreating, "isCreating");
			Assert.IsFalse(result.isCreated, "isCreated");
			Assert.IsFalse(result.isCreateFailed, "isCreateFailed");
			Assert.IsFalse(result.isLeaving, "isLeaving");
			Assert.IsFalse(result.hasLeft, "hasLeft");
			Assert.IsFalse(result.isLeavingFailed, "isLeavingFailed");
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }

		[Test]
		public void Should_set_isCreated_when_creating_succeeds() {
			// arrange
			var mockRoomAction = new Action.CreateStart {};
			var sut = reducer_;

			// act
			var result = sut.Reduce(mockRoomState_, mockRoomAction);

			// assert
			Assert.IsTrue(result.isJoined, "isJoined");
			Assert.IsFalse(result.isJoining, "isJoining");
			Assert.IsFalse(result.isJoinFailed, "isJoinFailed");
			Assert.IsFalse(result.isCreating, "isCreating");
			Assert.IsTrue(result.isCreated, "isCreated");
			Assert.IsFalse(result.isCreateFailed, "isCreateFailed");
			Assert.IsFalse(result.isLeaving, "isLeaving");
			Assert.IsFalse(result.hasLeft, "hasLeft");
			Assert.IsFalse(result.isLeavingFailed, "isLeavingFailed");
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }

		[Test]
		public void Should_set_isJoined_and_isCreated_when_creating_fails() {
			// arrange
			var mockRoomAction = new Action.CreateStart {};
			var sut = reducer_;

			// act
			var result = sut.Reduce(mockRoomState_, mockRoomAction);

			// assert
			Assert.IsFalse(result.isJoined, "isJoined");
			Assert.IsFalse(result.isJoining, "isJoining");
			Assert.IsFalse(result.isJoinFailed, "isJoinFailed");
			Assert.IsFalse(result.isCreating, "isCreating");
			Assert.IsFalse(result.isCreated, "isCreated");
			Assert.IsTrue(result.isCreateFailed, "isCreateFailed");
			Assert.IsFalse(result.isLeaving, "isLeaving");
			Assert.IsFalse(result.hasLeft, "hasLeft");
			Assert.IsFalse(result.isLeavingFailed, "isLeavingFailed");
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }
    }
}