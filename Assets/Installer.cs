using Zenject;

public class Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayingField>().FromComponentInHierarchy().AsSingle();
    }
}