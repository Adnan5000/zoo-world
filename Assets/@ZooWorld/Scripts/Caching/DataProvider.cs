using UnityEngine;
using ZooWorld.Scripts.Common;
using ZooWorld.Scripts.Context;

namespace ZooWorld.Scripts.Caching
{
    public class DataProvider : MonoSingleton<DataProvider>
    {
        private void Start()
        {
            InitializeSingleton();
        }

        [SerializeField] public AnimalModelConfig animalModelConfig;
    }
}
