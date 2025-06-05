using Core.Cube;
using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services
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
            return cubeItems;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
    }
}