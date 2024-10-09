using Arch.Views.Mediation;
using Zenject;
using ZooWorld.Scripts.Views.GameplayPanel;

namespace ZooWorld.Scripts.Views.Installers
{
    public class GameplayPanelInstaller : Installer<GameplayPanelInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindViewToMediator<GameplayPanelView, GameplayPanelMediator>();
        }
    }
}