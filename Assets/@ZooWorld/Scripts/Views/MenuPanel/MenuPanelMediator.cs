using Arch.InteractiveObjectsSpawnerService;
using Arch.Views.Mediation;
//using HighLow.Scripts.Controllers.Stat;
using Zenject;
using ZooWorld.Scripts.Caching;
using ZooWorld.Scripts.Controllers.Pooler;
using ZooWorld.Scripts.Controllers.Spawner;

namespace ZooWorld.Scripts.Views.MenuPanel
{
    public class MenuPanelMediator: Mediator<IMenuPanelView>
    {
        private IInteractiveObjectsManager _interactiveObjectsManager;
        private ISpawnerController _spawnerController;
        private IPoolController _poolController;

        [Inject]
        private void Init(IInteractiveObjectsManager interactiveObjectsManager,
            ISpawnerController spawnerController,
            IPoolController poolController)
        {
            _interactiveObjectsManager = interactiveObjectsManager;
            _spawnerController = spawnerController;
            _poolController = poolController;
        }

        protected override void OnMediatorInitialize()
        {
            base.OnMediatorInitialize();
            View.PlayButtonClicked += OnPlay;
        }

        private void OnPlay()
        {
            View.Remove(() =>
            {
                _interactiveObjectsManager.Instantiate("GameplayPanel", "GamplayUIContainer");
                {
                    DataProvider.Instance.animalModelConfig.Animals.ForEach(animal =>
                    {
                        _poolController.PreloadAnimal(animal.Info.Name, 50);
                    });
                    _spawnerController.Spawn();
                }
            });
        }
    }
}