using UnityEngine;
using NUnit.Framework;

namespace Reduxity.Tests.Example {

	[TestFixture]
	public class StoreTests {

		private App app_;
		
		[Test]
		public void Should_initialize_state_on_App_Awake() {
			app_ = new App();
			app_.Initialize();
			State currentState = App.Store.GetState();
			State initialState = new State {}.Initialize();

			// convert object to string
			var currentStateDump = ObjectDumper.Dump(currentState);
			var initialStateDump = ObjectDumper.Dump(initialState);

			Assert.AreEqual(currentStateDump, initialStateDump);
		}
	}
}