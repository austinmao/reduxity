using Zenject;
using UnityEngine.UI;
using System;
using Redux;

namespace Reduxity.Example.Zenject {

    public class Component : IComponent {

		readonly Dispatcher dispatch_;

        public Component(
			App app
        ) {
			dispatch_ = app.Store.Dispatch;
        }

		public void Render() {
		}
    }
}
