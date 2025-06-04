using AnyColorBall.Infrastructure;
using AnyColorBall.Services.Data;

namespace Infrastructure
{
    public class LoadProgressState : IState
    {
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        private GameStateMachine _stateMachine;

        public LoadProgressState(IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService)
        {
            _progressService = persistentProgressService;
            _saveLoadService = saveLoadService;
        }

        public void Init(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            return new PlayerProgress("DemoLevel");
        }
    }
}