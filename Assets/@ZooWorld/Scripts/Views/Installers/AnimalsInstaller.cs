using Arch.Views.Mediation;
using Zenject;
using ZooWorld.Scripts.Views.Animals;

namespace ZooWorld.Scripts.Views.Installers
{
    public class AnimalsInstaller: Installer<AnimalsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindViewToMediator<GameplayAnimalView, GameplayAnimalMediator>();
        }
    }
}