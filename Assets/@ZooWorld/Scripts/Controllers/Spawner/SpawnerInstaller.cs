using Zenject;

namespace ZooWorld.Scripts.Controllers.Spawner
{
    public class SpawnerInstaller : Installer<SpawnerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SpawnerController>().AsSingle();
        }
    }
}