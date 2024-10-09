using System.Threading.Tasks;
using UnityEngine;

namespace ZooWorld.Scripts.Controllers.Pooler
{
    public interface IPoolController
    {
        public Task PreloadAnimal(string prefabId, int count);
        public GameObject GetAnimalFromPool(string prefabAddress);
        public void ReturnAnimalToPool(string prefabAddress, GameObject animal);
    }
}