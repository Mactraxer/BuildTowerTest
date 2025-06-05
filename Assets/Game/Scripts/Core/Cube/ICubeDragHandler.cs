using UnityEngine;

namespace Core.Cube
{
    public interface ICubeDragHandler
    {
        void BeginDrag(CubeItem cube);
        void Drag(CubeItem cube, Vector2 delta);
        void EndDrag(CubeItem cube);
    }
}