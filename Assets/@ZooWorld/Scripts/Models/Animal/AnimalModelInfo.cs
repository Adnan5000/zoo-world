using System;
using ZooWorld.Scripts.Common;

namespace ZooWorld.Scripts.Models.Animal
{
    [Serializable]
    public class AnimalModelInfo
    {
        public string ID;
        public string Name;
        public EnumsHandler.AnimalType AnimalType;
        public bool IsAlive;
    }
}