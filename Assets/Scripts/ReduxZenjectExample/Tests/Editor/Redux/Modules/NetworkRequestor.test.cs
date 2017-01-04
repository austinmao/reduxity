using NUnit.Framework;
using Zenject;

namespace Reduxity.Example.Zenject.NetworkRequestor.Tests
{

    [TestFixture]
	public class Test : ZenjectUnitTestFixture {

		DiContainer container_;
		private NetworkState mockNetworkState_;
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
			mockNetworkState_ = new NetworkState {};
			reducer_ = container_.Resolve<Reducer>();
		}

		[Test]
		public void Should_set_isConnecting_and_isConnected_when_starting_request() {
			// arrange
			var mockNetworkAction = new Action.ConnectStart {};
			var sut = reducer_;

			// act
			var result = sut.Reduce(mockNetworkState_, mockNetworkAction);

			// assert
			Assert.IsFalse(result.isConnected);
			Assert.IsTrue(result.isConnecting);
			Assert.IsFalse(result.isConnectionFailed);
			Assert.IsFalse(result.isDisconnecting);
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }

		[Test]
		public void Should_set_Network_state_when_request_succeeds() {
            // arrange
			var mockNetworkAction = new Action.ConnectSuccess {
			};

            // act
			var result = reducer_.Reduce(mockNetworkState_, mockNetworkAction);

            // assert
			Assert.IsTrue(result.isConnected);
			Assert.IsFalse(result.isConnecting);
			Assert.IsFalse(result.isConnectionFailed);
			Assert.IsFalse(result.isDisconnecting);
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }

		[Test]
		public void Should_set_Network_state_when_connecting_fails() {
            // arrange
			var mockNetworkAction = new Action.ConnectFailure {
			};

            // act
			var result = reducer_.Reduce(mockNetworkState_, mockNetworkAction);

            // assert
			Assert.IsFalse(result.isConnected);
			Assert.IsFalse(result.isConnecting);
			Assert.IsTrue(result.isConnectionFailed);
			Assert.IsFalse(result.isDisconnecting);
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }

		[Test]
		public void Should_set_isDisconnecting_and_isConnected_when_starting_request() {
			// arrange
			var mockNetworkAction = new Action.DisconnectStart {};
			var sut = reducer_;

			// act
			var result = sut.Reduce(mockNetworkState_, mockNetworkAction);

			// assert
			Assert.IsTrue(result.isConnected);
			Assert.IsFalse(result.isConnecting);
			Assert.IsFalse(result.isConnectionFailed);
			Assert.IsTrue(result.isDisconnecting);
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }

		[Test]
		public void Should_set_Network_state_when_disconnecting_succeeds() {
            // arrange
			var mockNetworkAction = new Action.DisconnectSuccess {
			};

            // act
			var result = reducer_.Reduce(mockNetworkState_, mockNetworkAction);

            // assert
			Assert.IsFalse(result.isConnected);
			Assert.IsFalse(result.isConnecting);
			Assert.IsFalse(result.isConnectionFailed);
			Assert.IsFalse(result.isDisconnecting);
			Assert.IsInstanceOf<string>(result.feedbackText);
			Assert.IsNotEmpty(result.feedbackText);
        }
    }
}