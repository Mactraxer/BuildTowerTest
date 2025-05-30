using UnityEngine;

public interface ITrashZoneValidator
{
    bool IsInside(Vector2 screenPosition);
}
