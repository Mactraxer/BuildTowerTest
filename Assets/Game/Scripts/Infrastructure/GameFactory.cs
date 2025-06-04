using Core.Cube;
using Infrastructure;
using System.Collections.Generic;
using UnityEngine;

namespace AnyColorBall.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ICubeFactory _cubeFactory;

        public List<IReadablePlayerProgress> ProgressReaders { get; } = new List<IReadablePlayerProgress>();
        public List<ISavedPlayerProgress> ProgressWriters { get; } = new List<ISavedPlayerProgress>();

        public GameFactory(IAssetProvider assetProvider, ICubeFactory cubeFactory)
        {
            _assetProvider = assetProvider;
            _cubeFactory = cubeFactory;
        }

        public GameObject CreateLevel()
        {
            return _assetProvider.Instantiate(AssetsPath.LevelDemoPath);
        }

        public CubeItem[] CreateItems()
        {
            CubeItem[] cubeItems = _cubeFactory.CreateCubeItems();
            foreach (CubeItem cubeItem in cubeItems)
            {
                RegisterSaveable(cubeItem);
            }

            return cubeItems;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private void RegisterSaveable(ISavedPlayerProgress progressWriter)
        {
            ProgressWriters.Add(progressWriter);
        }
    }
}