using UnityEngine;

namespace Core.Tower
{
    public interface ITowerPlacementValidator
    {
        bool IsValid(Vector3 newCubePosition);
    }
}