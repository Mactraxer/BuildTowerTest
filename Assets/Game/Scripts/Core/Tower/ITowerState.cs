using Core.Cube;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tower
{
    public interface ITowerState
    {
        int CubeCount { get; }
        IReadOnlyList<CubeItem> Cubes { get; }

        void AddCube(CubeItem cube);

        bool Contain(CubeItem cubeItem);

        Vector3 GetNextPosition(RectTransform towerRect, CubeItem forCube, RectTransform canvasRect);

        bool IsCanPlaceByHeight(CubeItem cubeItem);

        int RemoveCube(CubeItem cube);

        void SetMaxY(float height);
    }
}