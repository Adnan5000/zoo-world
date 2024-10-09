using System;
using Arch.Views.Mediation;

namespace ZooWorld.Scripts.Views.MenuPanel
{
    public interface IMenuPanelView: IView
    {
        public Action PlayButtonClicked { get; set; }
    }
}