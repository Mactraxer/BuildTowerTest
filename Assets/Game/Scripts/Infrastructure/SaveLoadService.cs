using AnyColorBall.Services.Data;
using UnityEngine;

namespace AnyColorBall.Infrastructure
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public SaveLoadService(IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?.ToDesearialized<PlayerProgress>();
        }

        public void SaveProgress()
        {
            foreach (ISavedPlayerProgress writer in _gameFactory.ProgressWriters)
            {
                writer.UpdateProgress(_progressService.Progress);
            }

            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
            PlayerPrefs.Save();
        }
    }
}