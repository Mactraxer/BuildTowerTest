using Infrastructure.Services;

namespace Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";

        private readonly SceneLoader _sceneLoader;

        private GameStateMachine _stateMachine;

        public BootstrapState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Init(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(InitialSceneName, EnterLoadProgress);
        }

        public void Exit()
        {
        }

        private void EnterLoadProgress()
        {
            _stateMachine.Enter<LoadProgressState>();
        }
    }
}