using System;
using System.Collections.Generic;
using Arch.Signals;
using UniRx;
using UnityEngine;
using Zenject;
using ZooWorld.Scripts.Common;
using ZooWorld.Scripts.Controllers.Animal.Signals;
using ZooWorld.Scripts.Views.Animals;
using Random = UnityEngine.Random;

namespace ZooWorld.Scripts.Controllers.Animal
{
    public class CollisionController : ICollisionController, IInitializable
    {
        private ISignalService _signalService;
        private readonly CompositeDisposable _disposeOnDestroy = new CompositeDisposable();
        
        private HashSet<(IGameplayAnimalView, IGameplayAnimalView)> _handledCollisions = new HashSet<(IGameplayAnimalView, IGameplayAnimalView)>();

        public Action OnEatPredator { get; set; }
        public Action OnEatPrey { get; set; }

        [Inject]
        private void Init(ISignalService signalService)
        {
            _signalService = signalService;
        }

        public void Initialize()
        {
            AddListeners();
        }

        private void AddListeners()
        {
            _signalService.Receive<OnCollisionSignal>().Subscribe(OnCollision).AddTo(_disposeOnDestroy);
        }

        private void OnCollision(OnCollisionSignal signal)
        {
            var animal = signal.Animal.GetModel();
            var otherAnimal = signal.OtherAnimal.GetModel();

            var sortedPair = SortAnimals(signal.Animal, signal.OtherAnimal);

            if (_handledCollisions.Contains(sortedPair))
            {
                return;
            }

            _handledCollisions.Add(sortedPair);

            if (animal.IsAlive && otherAnimal.IsAlive)
            {
                if (animal.AnimalType == EnumsHandler.AnimalType.Predator && otherAnimal.AnimalType == EnumsHandler.AnimalType.Prey)
                {
                    signal.Animal.Eat();
                    signal.OtherAnimal.Die();
                    OnEatPrey?.Invoke();
                }
                else if (animal.AnimalType == EnumsHandler.AnimalType.Predator && otherAnimal.AnimalType == EnumsHandler.AnimalType.Predator)
                {
                    if (Random.value > 0.5f)
                    {
                        signal.OtherAnimal.Eat();
                        signal.Animal.Die();
                    }
                    else
                    {
                        signal.Animal.Eat();
                        signal.OtherAnimal.Die();
                    }
                    OnEatPredator?.Invoke();

                }
            }
            
            Observable.Timer(TimeSpan.FromSeconds(0.00001)).Subscribe(_ => _handledCollisions.Remove(sortedPair)).AddTo(_disposeOnDestroy);
            
        }

        private (IGameplayAnimalView, IGameplayAnimalView) SortAnimals(IGameplayAnimalView animal1, IGameplayAnimalView animal2)
        {
            return animal1.GetGameObject.GetInstanceID() < animal2.GetGameObject.GetInstanceID() ? (animal1, animal2) : (animal2, animal1);
        }

        private void Dispose()
        {
            _disposeOnDestroy.Dispose();
        }
    }
}
