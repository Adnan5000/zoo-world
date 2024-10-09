using System;
using System.Threading.Tasks;
using Arch.Views.Mediation;
using UnityEngine;
using UnityEngine.UIElements;
using ZooWorld.Scripts.Models.Animal;

namespace ZooWorld.Scripts.Views.Animals
{
    public interface IGameplayAnimalView: IView
    {
        public void SetModel(AnimalServiceModel model);
        public AnimalServiceModel GetModel();
        public GameObject GetGameObject { get; }
        public Action<GameplayAnimalView,GameplayAnimalView> OnCollisionEnterAction { get; set; }
        public Action<Vector3> OnSpawnTastyText { get; set; }
        public Action OnDie { get; set; }
        public void SetName(string name);
        
    }
}