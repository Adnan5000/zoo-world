using System;
using Arch.Views.Mediation;
using TMPro;

namespace ZooWorld.Scripts.Views.GameplayPanel
{
    public interface IGameplayPanel: IView
    {
        public int PredatorCount{ get; set; }
        public int PreyCount{ get; set; }
    }
}