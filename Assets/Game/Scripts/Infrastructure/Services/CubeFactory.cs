using Config;
using Core.Cube;
using Data;
using Extensions;
using Infrastructure.Pool;
using Services.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure.Services
{
    public class CubeFactory : ICubeFactory
    {
        private readonly CubeItemPool _pool;
        private readonly GameConfigData _gameConfig;
        private readonly Queue<int> _reservedIds;
        private readonly IPersistentProgressService _progressService;

        public List<ISavedPlayerProgress> ProgressWriters { get; } = new List<ISavedPlayerProgress>();

        public CubeFactory(GameConfigData gameConfig, CubeItemPool pool, IPersistentProgressService progressService)
        {
            _gameConfig = gameConfig;
            _pool = pool;
            _reservedIds = new Queue<int>(_gameConfig.IsInfiniteStock ? _gameConfig.CubeCount + 20 : _gameConfig.CubeCount);
            _progressService = progressService;
        }

        public CubeItem CreateCubeItem()
        {
            CubeItem cube = _pool.Spawn();
            int id = GetId();
            cube.ChangeState(CubeState.InStock);
            cube.SetSprite(_gameConfig.CubeSprites[id % _gameConfig.CubeSprites.Length], id % _gameConfig.CubeSprites.Length);
            cube.SetID(id);
            RegisterSaveable(cube);
            return cube;
        }

        public CubeItem[] CreateCubeItems()
        {
            List<ItemData> itemDatas = _progressService.Progress.WorldData.Items;
            int countInTower = itemDatas.Count((item) => item.State == CubeState.InTower);
            int countInStock = itemDatas.Count((item) => item.State == CubeState.InStock);
            countInStock = _gameConfig.IsInfiniteStock && countInStock < _gameConfig.CubeCount ? _gameConfig.CubeCount : countInStock;

            var cubeItems = new List<CubeItem>();

            if (itemDatas.Count > 0)
            {
                foreach (ItemData itemData in itemDatas)
                {
                    if (itemData.State == CubeState.Disposed) continue;

                    ReserveId(itemData.ID);
                    CubeItem cubeItem = CreateCubeItem();
                    cubeItem.RectTransform.anchoredPosition = itemData.PositionOnLevel.Vector3Data.AsUnityVector();
                    cubeItem.SetID(itemData.ID);
                    cubeItem.ChangeState(itemData.State);
                    cubeItem.SetSprite(GetSpriteById(itemData.SpriteId), itemData.SpriteId);
                    cubeItems.Add(cubeItem);
                }
            }
            else
            {
                for (int i = 0; i < _gameConfig.CubeCount; i++)
                {
                    ReserveId(i);
                    cubeItems.Add(CreateCubeItem());
                }
            }

            return cubeItems.ToArray();
        }

        public void DeleteCubeItem(CubeItem cubeItem)
        {
            _pool.Despawn(cubeItem);
            //UnregisterSaveable(cubeItem);
        }

        public Sprite GetSpriteById(int spriteId)
        {
            return _gameConfig.CubeSprites[spriteId];
        }

        public bool NeedAddNewItem(int stockCount)
        {
            return _gameConfig.IsInfiniteStock && stockCount < _gameConfig.CubeCount;
        }

        private int GetId()
        {
            int newId = _reservedIds.Count;
            ReserveId(newId);
            return newId;
        }

        private void ReserveId(int i)
        {
            _reservedIds.Enqueue(i);
        }

        private void UnregisterSaveable(ISavedPlayerProgress progressWriter)
        {
            ProgressWriters.Remove(progressWriter);
        }

        private void RegisterSaveable(ISavedPlayerProgress progressWriter)
        {
            ProgressWriters.Add(progressWriter);
        }
    }
}