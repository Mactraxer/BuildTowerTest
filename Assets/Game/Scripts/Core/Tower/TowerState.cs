using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerState : ITowerState
{
    private readonly List<CubeItem> _cubes = new();
    private readonly float maxHorizontalOffset = 50f;
    private float _maxHeight;

    public TowerState(float maxHeight)
    {
        _maxHeight = maxHeight;
    }
    //TODO: Убрать если сильно не нужно
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

    public bool Contain(CubeItem cubeItem)
    {
        return _cubes.Contains(cubeItem);
    }

    public Vector3 GetNextPosition(Vector3 basePosition, CubeItem forCube)
    {
        Vector2 position;

        if (_cubes.Count == 0)
        {
            position = basePosition;
        }
        else
        {
            var last = _cubes[_cubes.Count - 1];
            float lastHeight = last.Height;
            float lastY = last.RectTransform.anchoredPosition.y;

            float offsetX = Random.Range(-maxHorizontalOffset, maxHorizontalOffset);
            position = new Vector2(offsetX, lastY + lastHeight);
        }

        return position;
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