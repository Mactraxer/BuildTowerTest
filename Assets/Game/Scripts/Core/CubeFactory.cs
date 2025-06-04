using Core.Cube;
using System.Collections.Generic;
using UnityEngine;

public class CubeFactory : ICubeFactory
{
    private readonly CubeItemPool _pool;
    private readonly GameConfigData _gameConfig;

    public CubeFactory(GameConfigData gameConfig, CubeItemPool pool)
    {
        _gameConfig = gameConfig;
        _pool = pool;
    }

    public CubeItem[] CreateCubeItems(int count)
    {
        List<CubeItem> cubeItems = new List<CubeItem>(count);

        for (int i = 0; i < count; i++)
        {
            CubeItem cube = _pool.Spawn();
            cube.ChangeState(CubeState.InStock);
            cube.SetSprite(_gameConfig.CubeSprites[i % _gameConfig.CubeSprites.Length], i % _gameConfig.CubeSprites.Length);
            cubeItems.Add(cube);
        }

        return cubeItems.ToArray();
    }

    public CubeItem[] CreateCubeItems()
    {
        List<CubeItem> cubeItems = new List<CubeItem>(_gameConfig.CubeCount);

        for (int i = 0; i < _gameConfig.CubeCount; i++)
        {
            CubeItem cube = _pool.Spawn();
            cube.ChangeState(CubeState.InStock);
            cube.SetSprite(_gameConfig.CubeSprites[i % _gameConfig.CubeSprites.Length], i % _gameConfig.CubeSprites.Length);
            cube.SetID(i);
            cubeItems.Add(cube);
        }

        return cubeItems.ToArray();
    }

    public void DeleteCubeItem(CubeItem cubeItem)
    {
        _pool.Despawn(cubeItem);
    }

    public Sprite GetSpriteById(int spriteId)
    {
        return _gameConfig.CubeSprites[spriteId];
    }
}

