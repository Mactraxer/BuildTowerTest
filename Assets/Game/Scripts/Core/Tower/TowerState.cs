using Core.Cube;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerState : ITowerState
{
    private readonly List<CubeItem> _cubes = new();
    private readonly float _maxHorizontalOffset;
    private float _screenHeight;

    public TowerState(float maxHorizontalOffset)
    {
        this._maxHorizontalOffset = maxHorizontalOffset;
    }

    //TODO: Убрать если сильно не нужно
    public IReadOnlyList<CubeItem> Cubes => _cubes;
    public int CubeCount => _cubes.Count;

    public void SetMaxY(float maxY)
    {
        _screenHeight = maxY;
    }

    public void AddCube(CubeItem cube)
    {
        _cubes.Add(cube);
    }


    public bool Contain(CubeItem cubeItem)
    {
        return _cubes.Contains(cubeItem);
    }

    public Vector3 GetNextPosition(RectTransform towerRect, CubeItem forCube, RectTransform canvasRect)
    {
        Vector2 position;

        if (_cubes.Count == 0)
        {
            position = Vector2.zero;
        }
        else
        {
            var last = _cubes[_cubes.Count - 1];
            float lastHeight = last.Height;
            float lastY = last.RectTransform.anchoredPosition.y;
            float lastX = last.RectTransform.anchoredPosition.x;

            float offsetX = Random.Range(-_maxHorizontalOffset, _maxHorizontalOffset);
            position = new Vector2(lastX + offsetX, lastY + lastHeight);
        }

        return position;
    }

    public bool IsCanPlaceByHeight(CubeItem cubeItem)
    {
        if (_cubes.Count == 0) return true;

        float totalHeight = _cubes.Sum(cubeItem => cubeItem.Height);
        float firstCubeY = _cubes[0].RectTransform.position.y;
        float maxY = (_screenHeight - firstCubeY);
        return totalHeight + cubeItem.Height <= maxY;
    }

    public int RemoveCube(CubeItem cube)
    {
        int index = _cubes.IndexOf(cube);
        if (index >= 0)
            _cubes.RemoveAt(index);
        return index;
    }
}