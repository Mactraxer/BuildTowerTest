using AnyColorBall.Services.Data;
using System;
using UnityEngine;
using Zenject;

namespace AnyColorBall.Infrastructure
{
    public class GameBootstrapInstaller : MonoInstaller
    {
        [SerializeField] private LoadingUI _loadingUI;
        [SerializeField] private KeyboardInputService _keyboardInputService;

        private Game _game;

        public override void InstallBindings()
        {
            RegisterServices();
            RegisterGamestateMachine();
            RegisterGame();
            _game.StateMachine.Enter<BootstrapState>();
        }

        private void RegisterGamestateMachine()
        {
            Container.Bind<LoadingUI>().FromInstance(_loadingUI).AsSingle();
            Container.Bind<SceneLoader>().AsSingle().NonLazy();

            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadProgressState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
        }

        private void RegisterGame()
        {
            Container.Bind<Game>().AsSingle().NonLazy();
        }

        private void RegisterServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<IMessageService>().To<GameMessageService>().AsSingle();
        }
    }
}