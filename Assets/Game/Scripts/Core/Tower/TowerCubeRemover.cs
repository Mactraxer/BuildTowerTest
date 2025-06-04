using Core.Cube;
using DG.Tweening;
using UnityEngine;

public class TowerCubeRemover : ICubeRemover
{
    private readonly ITowerState _towerState;

    public TowerCubeRemover(ITowerState state)
    {
        _towerState = state;
    }

    public void Remove(CubeItem cubeItem)
    {
        int removedIndex = _towerState.RemoveCube(cubeItem);

        for (int i = removedIndex; i < _towerState.CubeCount; i++)
        {
            _towerState.Cubes[i].AnimateFallDown();
        }
    }
}
