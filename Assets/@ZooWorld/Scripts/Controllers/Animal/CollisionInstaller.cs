using Zenject;

namespace ZooWorld.Scripts.Controllers.Animal
{
    public class CollisionInstaller: Installer<CollisionInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<CollisionController>().AsSingle();
        }
    }
}