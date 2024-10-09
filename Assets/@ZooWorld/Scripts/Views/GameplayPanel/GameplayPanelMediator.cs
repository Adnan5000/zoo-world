using System.Threading.Tasks;
using Arch.InteractiveObjectsSpawnerService;
using Arch.SoundManager;
using Arch.Views.Mediation;
using UnityEngine;
using Zenject;
using ZooWorld.Scripts.Caching;
using ZooWorld.Scripts.Context;
using ZooWorld.Scripts.Controllers.Animal;
using ZooWorld.Scripts.Controllers.Pooler;
using ZooWorld.Scripts.Controllers.Spawner;
using ZooWorld.Scripts.Models.Animal;

namespace ZooWorld.Scripts.Views.GameplayPanel
{
    public class GameplayPanelMediator : Mediator<IGameplayPanel>
    {
        private ISpawnerController _spawnerController;
        private ICollisionController _collisionController;
        private IPoolController _poolController;
        //private AnimalModelConfig animalModelConfig;
        private int randomId;



        [Inject]
        private void Init(
            IInteractiveObjectsManager interactiveObjectsManager,
            ISpawnerController spawnerController,
            ICollisionController collisionController,
            IPoolController poolController)
        {
            _spawnerController = spawnerController;
            _collisionController = collisionController;
            _poolController = poolController;
        }

        protected override void OnMediatorInitialize()
        {
            base.OnMediatorInitialize();
            _spawnerController.SpawnRandomAnimal += SpawnAnimal;
            _collisionController.OnEatPredator += IncrementPredatorCount;
            _collisionController.OnEatPrey += IncrementPreyCount;
        }

        private void IncrementPreyCount()
        {
            View.PreyCount++;
            Debug.Log("Prey Count: " + View.PreyCount);
        }

        private void IncrementPredatorCount()
        {
            View.PredatorCount++;
            Debug.Log("Predator Count: " + View.PredatorCount);
        }

        private void SpawnAnimal()
        {
            randomId = Random.Range(0, DataProvider.Instance.animalModelConfig.Animals.Count);
            
            _poolController.GetAnimalFromPool(DataProvider.Instance.animalModelConfig
                     .GetAnimal(randomId.ToString()).Info
                     .Name);
        }

        protected override void OnMediatorDispose()
        {
            _spawnerController.SpawnRandomAnimal -= SpawnAnimal;
            _collisionController.OnEatPredator -= IncrementPredatorCount;
            _collisionController.OnEatPrey -= IncrementPreyCount;
        }
    }
}