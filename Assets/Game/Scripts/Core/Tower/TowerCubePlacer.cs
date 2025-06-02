using UnityEngine;

public class TowerCubePlacer : ICubePlacer
{
    private readonly RectTransform _towerParent;
    private readonly ITowerState _towerState;
    private readonly ITowerPlacementValidator _placementValidator;

    public TowerCubePlacer(RectTransform towerParent, ITowerState state, ITowerPlacementValidator placementValidator)
    {
        _towerParent = towerParent;
        _towerState = state;
        _placementValidator = placementValidator;
    }

    public DropResult TryPlaceCube(CubeItem cube)
    {
        if (!_towerState.IsCanPlaceByHeight(cube))
        {
            return new DropResult(false, DropError.HeightLimit);
        }

        if (!_placementValidator.IsValid(cube.Position))
        {
            return new DropResult(false, DropError.MissToTower);
        }

        Vector3 newPosition = _towerState.GetNextPosition(_towerParent.anchoredPosition, cube);
        cube.transform.SetParent(_towerParent);
        cube.AnimatePlaceInTower(newPosition);

        _towerState.AddCube(cube);
        return new DropResult(true);
    }
}
