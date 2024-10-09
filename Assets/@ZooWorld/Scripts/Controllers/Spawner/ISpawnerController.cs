using System;

namespace ZooWorld.Scripts.Controllers.Spawner
{
    public interface ISpawnerController
    {
        public void Spawn();
        public Action SpawnRandomAnimal{ get; set; }
    }
}