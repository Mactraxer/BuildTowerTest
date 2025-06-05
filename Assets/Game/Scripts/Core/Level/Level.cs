using UnityEngine;

namespace Core.Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private CubeTowerCoordinator _cubeTowerCoordinator;

        public CubeTowerCoordinator CubeTowerCoordinator => _cubeTowerCoordinator;
    }
}