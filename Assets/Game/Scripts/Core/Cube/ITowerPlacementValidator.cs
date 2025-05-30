using UnityEngine;

public interface ITowerPlacementValidator
{
    bool IsValid(Vector3 newCubePosition);
}