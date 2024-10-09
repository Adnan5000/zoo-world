using Arch.AssetReferences;
using Arch.Installers;
using Arch.InteractiveObjectsSpawnerService;
using Arch.Signals;
using Arch.SoundManager;
using Zenject;
using ZooWorld.Scripts.Controllers.Animal;
using ZooWorld.Scripts.Controllers.Pooler;
using ZooWorld.Scripts.Controllers.Spawner;
using ZooWorld.Scripts.Views.Installers;

namespace DI.Resources
{
    public class CommonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            MessageBrokerInstaller.Install(Container);
            SignalsInstaller.Install(Container);
            AssetReferenceInstaller.Install(Container);
            InteractiveObjectServiceInstaller.Install(Container);
            SoundManagerInstaller.Install(Container);
            
            MenuPanelInstaller.Install(Container);
            
            GameplayPanelInstaller.Install(Container);
            AnimalsInstaller.Install(Container);
            PoolInstaller.Install(Container);
            SpawnerInstaller.Install(Container);
            CollisionInstaller.Install(Container);
        }
    }
}