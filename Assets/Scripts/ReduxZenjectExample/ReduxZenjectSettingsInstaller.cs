using UnityEngine;
using Zenject;
using System;

namespace Reduxity.Example.Zenject {
    [CreateAssetMenu(fileName = "ReduxZenjectSettingsInstaller", menuName = "Installers/ReduxZenjectSettingsInstaller")]
    public class ReduxZenjectSettingsInstaller : ScriptableObjectInstaller<ReduxZenjectSettingsInstaller> {

        /* variable to access nested classes below */
        public PlayerSettings Player;

        [Serializable]
        /// <summary>
        /// Settings related to the player including character and camera
        /// </summary>
        public class PlayerSettings {
            public Movement.Settings Movement;
            public Look.Settings Looking;
        }

        /// <summary>
        /// Binding settings as an instance
        /// </summary>
        public override void InstallBindings() {
            Container.BindInstance(Player.Movement);
            Container.BindInstance(Player.Looking);
        }
    }
}
