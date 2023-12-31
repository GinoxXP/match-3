using Zenject;

public class Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayingField>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SwapChips>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Score>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Timer>().FromComponentInHierarchy().AsSingle();
        Container.Bind<AdsService>().FromComponentInHierarchy().AsSingle();

        Container.Bind<ISaveLoadService>().To<LocalSaveLoadService>().AsSingle();
    }
}