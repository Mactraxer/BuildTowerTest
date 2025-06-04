using Core.Cube;
using UnityEngine;

public interface ICubePlacer
{
    DropResult TryPlaceCube(CubeItem cube, RectTransform inContainer, RectTransform canvasRect);
    void Warp(CubeItem cubeItem, Vector2 to, RectTransform inContainer);
}
