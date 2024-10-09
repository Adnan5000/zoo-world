using Zenject;

namespace ZooWorld.Scripts.Controllers.Pooler
{
    public class PoolInstaller: Installer<PoolInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PoolController>().AsSingle();
        }
    }
}