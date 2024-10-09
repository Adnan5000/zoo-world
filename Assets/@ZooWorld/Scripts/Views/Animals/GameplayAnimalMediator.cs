using Arch.InteractiveObjectsSpawnerService;
using Arch.Signals;
using Arch.Views.Mediation;
using UnityEngine;
using Zenject;
using ZooWorld.Scripts.Controllers.Animal.Signals;
using ZooWorld.Scripts.Controllers.Pooler;

namespace ZooWorld.Scripts.Views.Animals
{
    public class GameplayAnimalMediator: Mediator<IGameplayAnimalView>
    {
        private ISignalService _signalService;
        private IInteractiveObjectsManager _interactiveObjectsManager;
        private IPoolController _poolController;


        [Inject]
        public void Init(ISignalService signalService, 
            IInteractiveObjectsManager interactiveObjectsManager,
            IPoolController poolController)
        {
            _signalService = signalService;
            _interactiveObjectsManager = interactiveObjectsManager;
            _poolController = poolController;
        }

        protected override void OnMediatorEnable()
        {
            base.OnMediatorEnable();
            View.OnCollisionEnterAction += OnCollisionEnter;
            View.OnSpawnTastyText += SpawnTastyText;
            View.OnDie += Die;
        }

        private void Die()
        {
            _poolController.ReturnAnimalToPool(View.GetModel().Name, View.GetGameObject);
        }
        
        private void OnCollisionEnter(GameplayAnimalView animal, GameplayAnimalView otherAnimal)
        {
            _signalService.Publish(new OnCollisionSignal()
            {
                Animal = animal,
                OtherAnimal = otherAnimal
            });
        }
        
        private void SpawnTastyText(Vector3 objPosition)
        {
            _interactiveObjectsManager.Instantiate("TastyText", "TastyTextContainer",  tastyText =>
            {
                tastyText.gameObject.GetComponent<Transform>().position = objPosition;
                tastyText.gameObject.GetComponent<Transform>().position += new Vector3(0, 0, -2);
            });
        }
    }
}