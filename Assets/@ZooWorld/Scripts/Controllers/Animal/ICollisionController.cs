using System;

namespace ZooWorld.Scripts.Controllers.Animal
{
    public interface ICollisionController
    {
        public Action OnEatPredator { get; set; }
        public Action OnEatPrey { get; set; }
    }
}