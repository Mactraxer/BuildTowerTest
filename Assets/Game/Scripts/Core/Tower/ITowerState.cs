using System.Collections.Generic;
using UnityEngine;

public interface ITowerState
{
    int CubeCount { get; }
    IReadOnlyList<CubeItem> Cubes { get; }

    void AddCube(CubeItem cube);
    bool CanAddCube(CubeItem cube);
    bool Contain(CubeItem cubeItem);
    Vector3 GetNextPosition(Vector3 basePosition, CubeItem forCube);
    bool IsCanPlaceByHeight(CubeItem cubeItem);
    int RemoveCube(CubeItem cube);
}