using UnityEngine;
using Zenject;
using System;

namespace Reduxity.Example.Zenject {
    [CreateAssetMenu(fileName = "ReduxZenjectSettingsInstaller", menuName = "Installers/ReduxZenjectSettingsInstaller")]
    public class ReduxZenjectSettingsInstaller : ScriptableObjectInstaller<ReduxZenjectSettingsInstaller> {

        /* These variables provide access nested classes below */
        public PlayerSettings Player;
        // public HttpSettings Http;
        public GameInstaller.Settings GameInstaller;

        [Serializable]
        /// <summary>
        /// Settings related to the player including character and camera
        /// </summary>
        public class PlayerSettings {
            public CharacterMover.Settings Movement;
            public CharacterLook.Settings Looking;
        }

        public class HttpSettings {
            // public ApiRequestor.Settings Api;
        }

        /// <summary>
        /// Binding settings as an instance
        /// </summary>
        public override void InstallBindings() {
            Container.BindInstance(Player.Movement);
            Container.BindInstance(Player.Looking);
            // Container.BindInstance(Http.Api);
            Container.BindInstance(GameInstaller);
        }
    }
}
