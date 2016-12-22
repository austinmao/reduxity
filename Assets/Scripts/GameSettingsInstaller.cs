using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller> {
    
    public GameInstaller.Settings GameInstaller;
    public CubeManager.Settings CubeManager;
    public Cube.Settings Cube;

    public override void InstallBindings() {
        Container.BindInstance(GameInstaller);
        Container.BindInstance(CubeManager);
        Container.BindInstance(Cube);
    }
}