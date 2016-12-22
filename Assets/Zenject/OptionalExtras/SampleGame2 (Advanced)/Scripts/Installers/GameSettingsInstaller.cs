using System;
using UnityEngine;
using System.Collections;
using Zenject;

namespace Zenject.SpaceFighter
{
    // We prefer to use ScriptableObjectInstaller for installers that contain game settings
    // There's no reason why you couldn't use a MonoInstaller here instead, however
    // using ScriptableObjectInstaller has advantages here that make it nice for settings:
    //
    // 1) You can change these values at runtime and have those changes persist across play
    //    sessions.  If it was a MonoInstaller then any changes would be lost when you hit stop
    // 2) You can easily create multiple ScriptableObject instances of this installer to test
    //    different customizations to settings.  For example, you might have different instances
    //    for each difficulty mode of your game, such as "Easy", "Hard", etc.
    // 3) If your settings are associated with a game object composition root, then using
    //    ScriptableObjectInstaller can be easier since there will only ever be one definitive
    //    instance for each setting.  Otherwise, you'd have to change the settings for each game
    //    object composition root separately at runtime
    //
    // Uncomment if you want to add alternative game settings
    //[CreateAssetMenu(menuName = "Space Fighter/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public CameraHandler.Settings CameraHandler;
        public EnemySpawner.Settings EnemySpawner;
        public GameDifficultyHandler.Settings GameDifficultyHandler;
        public GameRestartHandler.Settings GameRestartHandler;
        public EnemyGlobalTunables EnemyGlobalTunables;
        public GameInstaller.Settings GameInstaller;

        public override void InstallBindings()
        {
            Container.BindInstance(CameraHandler);
            Container.BindInstance(EnemySpawner);
            Container.BindInstance(GameDifficultyHandler);
            Container.BindInstance(GameRestartHandler);
            Container.BindInstance(EnemyGlobalTunables);
            Container.BindInstance(GameInstaller);
        }
    }
}

