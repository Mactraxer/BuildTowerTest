using Core.Cube;
using UnityEngine;

namespace Core.Tower
{
    public interface ICubePlacer
    {
        DropResult TryPlaceCube(CubeItem cube, RectTransform inContainer, RectTransform canvasRect);

        void Warp(CubeItem cubeItem, Vector2 to, RectTransform inContainer);
    }
}