using System;
using Arch.SoundManager;
using Arch.Views.Mediation;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ZooWorld.Scripts.Views.MenuPanel
{
    public class MenuPanelView: View, IMenuPanelView
    {
        public Action PlayButtonClicked { get; set; }
        
        [Header("Buttons")]
        [SerializeField] private Button btnPlay;
        
        [Inject] private ISoundManager _soundManager;

        private void Start()
        {
            btnPlay.onClick.AddListener(ClickToPlay);
        }
        

        private void ClickToPlay()
        {
            _soundManager.PlayAudioClip(new AudioClipManagerModel()
            {
                ClipName = "Click"
            });
            
            PlayButtonClicked?.Invoke();
        }
    }
}