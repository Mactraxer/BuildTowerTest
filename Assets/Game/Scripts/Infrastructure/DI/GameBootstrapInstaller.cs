using AnyColorBall.Services.Data;
using Core.Cube;
using Infrastructure;
using UnityEngine;
using Zenject;

namespace AnyColorBall.Infrastructure
{
    public class GameBootstrapInstaller : MonoInstaller
    {
        [SerializeField] private LoadingUI _loadingUI;
        [SerializeField] private GameBootstrapper _gameBootstrapper;
        [SerializeField] private Transform _poolParent;

        public override void InstallBindings()
        {
            RegisterLoadingUI();
            RegisterServices();
            RegisterGamestateMachine();
            RegisterGame();
            RegisterGameBootstrapper();
            RegisterLevel();
        }

        private void RegisterLevel()
        {
            var gameConfig = Container.Resolve<GameConfigData>();
            Container.Bind<ITowerState>().To<TowerState>().AsSingle().WithArguments(gameConfig.MaxBuildOffset);
            Container.Bind<ITowerPlacementValidator>().To<SimpleTowerPlacementValidator>().AsSingle().WithArguments(gameConfig.MaxHorizontalOffsetRatio, gameConfig.MinVerticalOffset);
            Container.Bind<ICubePlacer>().To<TowerCubePlacer>().AsSingle();
            Container.Bind<ICubeRemover>().To<TowerCubeRemover>().AsSingle();
            Container.Bind<ITrashZoneValidator>().To<EllipseTrashZoneValidator>().AsSingle();
            Container.Bind<IItemCollectionState>().To<ItemCollectionState>().AsSingle();
            Container.Bind<CubeTowerCoordinator>().AsSingle();
        }

        private void RegisterGameBootstrapper()
        {
            Container.Bind<GameBootstrapper>().FromComponentInNewPrefab(_gameBootstrapper).AsSingle().NonLazy();
        }

        private void RegisterLoadingUI()
        {
            Container.Bind<LoadingUI>().FromComponentInNewPrefab(_loadingUI).AsSingle().NonLazy();
        }

        private void RegisterGamestateMachine()
        {
            Container.Bind<SceneLoader>().AsSingle().NonLazy();

            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadProgressState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();

            var stateMachine = Container.Resolve<GameStateMachine>();

            var bootstrap = Container.Resolve<BootstrapState>();
            var loadProgress = Container.Resolve<LoadProgressState>();
            var loadLevel = Container.Resolve<LoadLevelState>();
            var gameLoop = Container.Resolve<GameLoopState>();

            bootstrap.Init(stateMachine);
            loadProgress.Init(stateMachine);
            loadLevel.Init(stateMachine);
            gameLoop.Init(stateMachine);
        }

        private void RegisterGame()
        {
            Container.Bind<Game>().AsSingle().NonLazy();
        }

        private void RegisterServices()
        {
            Container.Bind<GameConfigData>().FromScriptableObjectResource("Configs/GameConfig").AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
            Container.Bind<ICubeFactory>().To<CubeFactory>().AsSingle();
            var cubeItemPrefab = Resources.Load<GameObject>(AssetsPath.CubeItemPath);
            Container.BindMemoryPool<CubeItem, CubeItemPool>().WithInitialSize(30).FromComponentInNewPrefab(cubeItemPrefab).UnderTransform(_poolParent);

            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<IMessageService>().To<GameMessageService>().AsSingle();
        }
    }
}