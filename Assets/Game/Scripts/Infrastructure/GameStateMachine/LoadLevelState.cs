using Core.Cube;
using Core.Level;
using Infrastructure.Services;
using Services.Data;

namespace Infrastructure.StateMachine
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

            _stateMachine.Enter<GameLoopState>();
        }

        private void CreateGameWorld()
        {
            Level level = _gameFactory.CreateLevel().GetComponent<Level>();
            CubeItem[] cubeItems = _gameFactory.CreateItems();

            for (int i = 0; i < cubeItems.Length; i++)
            {
                CubeItem cubeItem = cubeItems[i];
                CubeDragHandler cubeDragHandler = cubeItem.GetComponent<CubeDragHandler>();

                level.CubeTowerCoordinator.RegisterCube(cubeItem, cubeDragHandler);
            }
        }
    }
}