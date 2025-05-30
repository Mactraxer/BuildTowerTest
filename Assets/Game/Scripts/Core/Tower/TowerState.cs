using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerState : ITowerState
{
    private readonly List<CubeItem> _cubes = new();
    private float _maxHeight;

    public TowerState(float maxHeight)
    {
        _maxHeight = maxHeight;
    }

    public IReadOnlyList<CubeItem> Cubes => _cubes;
    public int CubeCount => _cubes.Count;

    public void AddCube(CubeItem cube)
    {
        _cubes.Add(cube);
    }

    public bool CanAddCube(CubeItem newCube)
    {
        float totalHeight = _cubes.Sum(cubeItem => cubeItem.Height);
        return totalHeight + newCube.Height <= _maxHeight;
    }

    public Vector3 GetNextPosition(Vector3 basePosition, CubeItem forCube)
    {
        float yOffset = 0f;
        foreach (var cube in _cubes)
        {
            yOffset += cube.Height;
        }

        float randomXOffset = Random.Range(-0.5f, 0.5f) * forCube.Width;
        return basePosition + new Vector3(randomXOffset, yOffset, 0f);
    }

    public bool IsCanPlaceByHeight(CubeItem cubeItem)
    {
        return CanAddCube(cubeItem);
    }

    public int RemoveCube(CubeItem cube)
    {
        int index = _cubes.IndexOf(cube);
        if (index >= 0)
            _cubes.RemoveAt(index);
        return index;
    }
}