using Arch.Views.Mediation;
using Zenject;
using ZooWorld.Scripts.Views.MenuPanel;

namespace ZooWorld.Scripts.Views.Installers
{
    public class MenuPanelInstaller: Installer<MenuPanelInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindViewToMediator<MenuPanelView, MenuPanelMediator>();
        }
    }
}