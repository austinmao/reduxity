using UnityEngine;
using NUnit.Framework;
using Zenject;
using Moq;
using System;

namespace Reduxity.Example.Zenject.ApiRequestor.Tests {

	[TestFixture]
	public class Test : ZenjectUnitTestFixture {

		DiContainer container_;
		private ApiState mockApiState_;
		private Reducer reducer_;

		[SetUp]
		public void Setup() {
			// create private container
			container_ = new DiContainer();
			container_.Bind<Reducer>().AsSingle();

			// set up mock settings
			// var mockSettings = new Settings {
			// };
			// container_.Bind<Settings>().FromInstance(mockSettings);

			// set default state
			mockApiState_ = new ApiState {};
			reducer_ = container_.Resolve<Reducer>();
		}

		[Test]
		public void Should_set_isLoading_and_isLoaded_when_starting_request() {
			var mockApiAction = new Action.GetStart {};
			var sut = reducer_;
			var result = sut.Reduce(mockApiState_, mockApiAction);

			Assert.IsFalse(result.isLoaded);
			Assert.IsTrue(result.isLoading);
        }

		[Test]
		public void Should_set_api_state_when_request_succeeds() {
			var mockApiAction = new Action.GetSuccess {
				text = "foo"
			};
			var result = reducer_.Reduce(mockApiState_, mockApiAction);

			Assert.IsTrue(result.isLoaded);
			Assert.IsFalse(result.isLoading);
			Assert.IsNull(result.error);
			Assert.AreEqual(result.text, "foo");
        }

		[Test]
		public void Should_set_api_state_when_request_fails() {
			var mockApiAction = new Action.GetFailure {
				error = new Exception("bar")
			};
			var result = reducer_.Reduce(mockApiState_, mockApiAction);

			Assert.IsTrue(result.isLoaded);
			Assert.IsFalse(result.isLoading);
			Assert.IsNotNull(result.error);
			Assert.IsNull(result.text);
        }
    }
}