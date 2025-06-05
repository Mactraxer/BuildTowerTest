using Data;
using Extensions;
using Services.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IGameFactory _gameFactory;
        private readonly ICubeFactory _cubeFactory;
        private readonly IPersistentProgressService _progressService;

        public SaveLoadService(IGameFactory gameFactory, IPersistentProgressService progressService, ICubeFactory cubeFactory)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
            _cubeFactory = cubeFactory;
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?.ToDesearialized<PlayerProgress>();
        }

        public void SaveProgress()
        {
            UpdateProgressForSaveables(_gameFactory.ProgressWriters);
            UpdateProgressForSaveables(_cubeFactory.ProgressWriters);

            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
            PlayerPrefs.Save();
        }

        private void UpdateProgressForSaveables(List<ISavedPlayerProgress> writers)
        {
            foreach (ISavedPlayerProgress writer in writers)
            {
                writer.UpdateProgress(_progressService.Progress);
            }
        }
    }
}