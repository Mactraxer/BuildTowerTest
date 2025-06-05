using Core.Cube;
using UnityEngine;
using Zenject;

namespace Core.Tower
{
    public class SimpleTowerPlacementValidator : ITowerPlacementValidator
    {
        private readonly float _maxHorizontalOffsetRatio;
        private readonly float _minVerticalOffset;
        [Inject] private ITowerState _towerState;

        public SimpleTowerPlacementValidator(float maxHorizontalOffsetRatio, float minVerticalOffset)
        {
            _maxHorizontalOffsetRatio = maxHorizontalOffsetRatio;
            _minVerticalOffset = minVerticalOffset;
        }

        public bool IsValid(Vector3 newCubePosition)
        {
            if (_towerState.CubeCount == 0)
                return true;

            CubeItem lastCube = _towerState.Cubes[_towerState.CubeCount - 1];
            float halfWidth = lastCube.Width / 2f;
            float maxOffset = halfWidth * _maxHorizontalOffsetRatio;

            float deltaX = Mathf.Abs(newCubePosition.x - lastCube.Position.x);
            float deltaY = newCubePosition.y - lastCube.Position.y;

            bool isHorizontallyAligned = deltaX <= maxOffset;
            bool isAboveEnough = deltaY >= _minVerticalOffset;

            return isHorizontallyAligned && isAboveEnough;
        }
    }
}