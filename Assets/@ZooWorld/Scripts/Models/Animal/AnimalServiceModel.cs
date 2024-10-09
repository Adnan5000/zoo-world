using System;
using ZooWorld.Scripts.Common;

namespace ZooWorld.Scripts.Models.Animal
{
    [Serializable]
    public class AnimalServiceModel
    {
        public string ID;
        public string Name;
        public EnumsHandler.AnimalType AnimalType;
        public bool IsAlive;
        
        public AnimalServiceModel()
        {
            
        }
        
        public AnimalServiceModel(AnimalModelInfo info)
        {
            ID = info.ID;
            Name = info.Name;
            AnimalType = info.AnimalType;
            IsAlive = info.IsAlive;
        }
        
        public static AnimalServiceModel CreateNewAnimal(AnimalModelInfo cardModel)
        {
            AnimalServiceModel animal = new AnimalServiceModel();

            animal.ID = cardModel.ID;
            animal.Name = cardModel.Name;
            animal.AnimalType = cardModel.AnimalType;
            animal.IsAlive = cardModel.IsAlive;

            return animal;
        }
    }
}