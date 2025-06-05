using UnityEngine;

namespace Core.Trash
{
    public interface ITrashZoneValidator
    {
        bool IsInside(RectTransform cubeRect, RectTransform ellipse);
    }
}