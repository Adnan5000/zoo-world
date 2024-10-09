using System;
using Arch.Views.Mediation;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ZooWorld.Scripts.Views.GameplayPanel
{
    public class GameplayPanelView: View, IGameplayPanel
    {
        public Action HighButtonClicked { get; set; }
        public Action LowButtonClicked { get; set; }
        public Action EqualButtonClicked { get; set; }
        public int PredatorCount { get; set; }
        public int PreyCount { get; set; }

        private void OnGUI()
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 24;
            style.alignment = TextAnchor.MiddleCenter;

            GUI.Box(new Rect(Screen.width - 250, 10, 200, 100), "Zoo Stats");

            GUI.Label(new Rect(Screen.width - 240, 40, 180, 30), $"Predators: {PredatorCount}", style);

            GUI.Label(new Rect(Screen.width - 240, 70, 180, 30), $"Prey: {PreyCount}", style);
        }
    }
}