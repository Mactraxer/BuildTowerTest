using System.Collections.Generic;
using UnityEngine;

namespace AnyColorBall.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public List<IReadablePlayerProgress> ProgressReaders { get; } = new List<IReadablePlayerProgress>();
        public List<ISavedPlayerProgress> ProgressWriters { get; } = new List<ISavedPlayerProgress>();

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreatePlayer(Vector3 position)
        {
            return InstantiateRegistered(AssetsPath.PlayerPath, position);
        }

        public GameObject CreateLevel()
        {
            return _assetProvider.Instantiate(AssetsPath.LevelDemoPath);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            GameObject gameObject = _assetProvider.Instantiate(prefabPath, position);

            foreach (ISavedPlayerProgress progressReader in gameObject.GetComponentsInChildren<ISavedPlayerProgress>())
            {
                Register(progressReader);
            }

            return gameObject;
        }

        private void Register(ISavedPlayerProgress progressWriter)
        {
            if (progressWriter is IReadablePlayerProgress reader)
            {
                ProgressReaders.Add(reader);
            }

            ProgressWriters.Add(progressWriter);
        }
    }
}