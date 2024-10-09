using System;
using System.Threading;
using System.Threading.Tasks;
using Arch.InteractiveObjectsSpawnerService;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace ZooWorld.Scripts.Controllers.Spawner
{
    public class SpawnerController : ISpawnerController, IDisposable
    {
        public Action SpawnRandomAnimal { get; set; }

        private CancellationTokenSource _cancellationTokenSource;
        
        public void Spawn()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            SpawnWithDelay(_cancellationTokenSource.Token);
        }

        public void StopSpawning()
        {
            _cancellationTokenSource?.Cancel();
        }

        private async Task SpawnWithDelay(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    var randomDelay = new Random().Next(1000, 2000);
                    await Task.Delay(randomDelay, token);
                    
                    SpawnRandomAnimal?.Invoke();
                }
            }
            catch (TaskCanceledException)
            {
                Debug.Log("Spawning was canceled.");
            }
        }

        public void Dispose()
        {
            StopSpawning();
            _cancellationTokenSource?.Dispose();
        }
    }
}