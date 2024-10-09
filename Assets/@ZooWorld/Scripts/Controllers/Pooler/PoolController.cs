using System;
using System.Collections.Generic;
using Arch.Signals;
using UniRx;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using Arch.InteractiveObjectsSpawnerService;
using ZooWorld.Scripts.Caching;
using ZooWorld.Scripts.Context;
using ZooWorld.Scripts.Models.Animal;
using ZooWorld.Scripts.Views.Animals;
namespace ZooWorld.Scripts.Controllers.Pooler
{
    public class PoolController : IPoolController
    {
        private AnimalModelConfig animalModelConfig;
        private AnimalServiceModel _animalServiceModel = new AnimalServiceModel();

        private IInteractiveObjectsManager _interactiveObjectsManager;

        private readonly CompositeDisposable _disposeOnDestroy = new CompositeDisposable();

        private Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();
        private Dictionary<string, GameObject> addressablePrefabs = new Dictionary<string, GameObject>();

        [Inject]
        private void Init(IInteractiveObjectsManager interactiveObjectsManager)
        {
            _interactiveObjectsManager = interactiveObjectsManager;
        }

        public async Task PreloadAnimal(string prefabId, int count)
        {
            if (!pool.ContainsKey(prefabId))
            {
                pool[prefabId] = new Queue<GameObject>();
            }

            if (!addressablePrefabs.ContainsKey(prefabId))
            {
                var prefab = await Addressables.LoadAssetAsync<GameObject>(prefabId).Task;
                addressablePrefabs[prefabId] = prefab;
            }

            for (int i = 0; i < count; i++)
            {
                InstantiateAnimal(prefabId, false, pool);
            }
        }

        public GameObject GetAnimalFromPool(string prefabAddress)
        {
            if (!pool.ContainsKey(prefabAddress))
            {
                pool[prefabAddress] = new Queue<GameObject>();
            }

            if (pool[prefabAddress].Count > 0)
            {
                GameObject animal = pool[prefabAddress].Dequeue();
                animal.SetActive(true);
                return animal;
            }
            else
            {
                GameObject newAnimal = InstantiateAnimal(prefabAddress, true);
                return newAnimal;
            }
        }

        public void ReturnAnimalToPool(string prefabAddress, GameObject animal)
        {
            animal.SetActive(false);

            if (!pool.ContainsKey(prefabAddress))
            {
                pool[prefabAddress] = new Queue<GameObject>();
            }

            pool[prefabAddress].Enqueue(animal);
        }

        public GameObject InstantiateAnimal(string prefabId, bool isActive,
            Dictionary<string, Queue<GameObject>> pool = null)
        {
            if (DataProvider.Instance == null)
            {
                Debug.LogError("DataProvider is null");
                return null;
            }

            animalModelConfig = DataProvider.Instance.animalModelConfig;
            if (animalModelConfig == null)
            {
                Debug.LogError("AnimalModelConfig is null. Make sure it is assigned in DataProvider.");
                return null;
            }

            GameObject newAnimal = null;
            _interactiveObjectsManager.Instantiate(
                prefabId, "SpawnerContainer",
                go =>
                {
                    _animalServiceModel = AnimalServiceModel.CreateNewAnimal(new AnimalModelInfo()
                    {
                        ID = animalModelConfig.GetAnimalByName(prefabId).Info.ID,
                        Name = animalModelConfig.GetAnimalByName(prefabId).Info.Name,
                        IsAlive = animalModelConfig.GetAnimalByName(prefabId).Info.IsAlive,
                        AnimalType = animalModelConfig.GetAnimalByName(prefabId).Info.AnimalType
                    });

                    if (go.TryGetComponent(out IGameplayAnimalView animalView))
                    {
                        animalView.SetModel(_animalServiceModel);
                        animalView.SetName(_animalServiceModel.Name);
                        go.SetActive(isActive);
                        newAnimal = go;

                        if (pool != null)
                        {
                            pool[prefabId].Enqueue(go);
                        }
                    }
                    else
                    {
                        Debug.LogError("IGameplayAnimalView component missing on animal prefab");
                    }
                });

            return newAnimal;
        }
    }
}
