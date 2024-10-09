using Arch.Signals;
using ZooWorld.Scripts.Views.Animals;

namespace ZooWorld.Scripts.Controllers.Animal.Signals
{
    public class OnCollisionSignal: ISignal
    {
        public GameplayAnimalView Animal;
        public GameplayAnimalView OtherAnimal;
    }
}