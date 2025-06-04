using UnityEngine;

public interface ITrashZoneValidator
{
    bool IsInside(RectTransform cubeRect, RectTransform ellipse);
}
