using AnyColorBall.Services.Data;
using System;
using UnityEngine;

namespace AnyColorBall.Infrastructure
{
    public class LoadLevelState : IPayloadableState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingUI _loadingUI;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        private GameStateMachine _stateMachine;

        public LoadLevelState(SceneLoader sceneLoader, LoadingUI loadingUI, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _sceneLoader = sceneLoader;
            _loadingUI = loadingUI;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Init(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(string payload)
        {
            _loadingUI.Show();
            _sceneLoader.LoadScene(payload, OnLoaded);
        }

        public void Exit()
        {
            _loadingUI.Hide();
        }

        private void OnLoaded()
        {
            CreateGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (IReadablePlayerProgress reader in _gameFactory.ProgressReaders)
            {
                reader.ReadProgress(_progressService.Progress);
            }
        }

        private void CreateGameWorld()
        {
            _gameFactory.CreateLevel();
        }
    }
}