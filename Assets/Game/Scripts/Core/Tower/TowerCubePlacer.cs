﻿using Core.Cube;
using UnityEngine;

namespace Core.Tower
{
    public class TowerCubePlacer : ICubePlacer
    {
        private readonly ITowerState _towerState;
        private readonly ITowerPlacementValidator _placementValidator;

        public TowerCubePlacer(ITowerState state, ITowerPlacementValidator placementValidator)
        {
            _towerState = state;
            _placementValidator = placementValidator;
        }

        public DropResult TryPlaceCube(CubeItem cube, RectTransform inContainer, RectTransform canvasRect)
        {
            if (!_towerState.IsCanPlaceByHeight(cube))
                return new DropResult(false, DropError.HeightLimit);

            if (_towerState.Contain(cube))
                return new DropResult(false, DropError.MissFromTower);

            if (!_placementValidator.IsValid(cube.Position))
                return new DropResult(false, DropError.MissToTower);

            Vector3 newPosition = _towerState.GetNextPosition(inContainer, cube, canvasRect);
            cube.transform.SetParent(inContainer);
            cube.AnimatePlaceInTower(newPosition);

            _towerState.AddCube(cube);
            cube.ChangeState(CubeState.InTower);
            return new DropResult(true);
        }

        public void Warp(CubeItem cubeItem, Vector2 to, RectTransform inContainer)
        {
            cubeItem.transform.SetParent(inContainer);
            cubeItem.RectTransform.anchoredPosition = to;
            _towerState.AddCube(cubeItem);
        }
    }
}