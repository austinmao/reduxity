using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ReduxZenjectSettingsInstaller", menuName = "Installers/ReduxZenjectSettingsInstaller")]
public class ReduxZenjectSettingsInstaller : ScriptableObjectInstaller<ReduxZenjectSettingsInstaller>
{
    public override void InstallBindings()
    {
    }
}