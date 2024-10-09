using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZooWorld.Scripts.Models.Animal;

namespace ZooWorld.Scripts.Context
{
     [CreateAssetMenu(fileName = "AnimalModelConfig", menuName = "ScriptableObjects/AnimalModelConfig", order = 1)]
    public class AnimalModelConfig : ScriptableObject
    {
        [SerializeField] public List<AnimalModel> Animals = null;

        public AnimalModel GetAnimal(string id)
            => Animals?.FirstOrDefault(st => st.Info.ID.Equals(id));
        
        public AnimalModel GetAnimalByName(string name)
            => Animals?.FirstOrDefault(st => st.Info.Name.Equals(name));

        /// <summary>
        ///  Get stats of animal by its id
        /// </summary>
        /// <param name="id">Animal Id</param>
        /// <returns>AnimalStatsModel animal </returns>
        public AnimalModelInfo GetAnimalInfo(string id)
            => GetAnimal(id)?.Info;

        public GameObject GetAnimalModelPrefab(string id)
            => GetAnimal(id)?.AnimalModelPrefab;

        public string[] AnimalIds => Animals?.Select(h => h.Info.ID).ToArray();
    }

    [Serializable]
    public class AnimalModel
    {
        [SerializeField] public GameObject AnimalModelPrefab;
        [SerializeField] public AnimalModelInfo Info = null;

        public void SetInfos(AnimalModelInfo info)
        {
            Info = info;
        }
    }
}
