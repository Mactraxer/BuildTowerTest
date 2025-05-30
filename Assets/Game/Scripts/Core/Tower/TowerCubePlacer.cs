using UnityEngine;

public class TowerCubePlacer : ICubePlacer
{
    private readonly RectTransform _towerParent;
    private readonly ITowerState _towerState;
    private readonly ITowerPlacementValidator _placementValidator;
    private readonly IMessageService _messageService;

    public TowerCubePlacer(RectTransform towerParent, ITowerState state, ITowerPlacementValidator placementValidator, IMessageService messages)
    {
        _towerParent = towerParent;
        _towerState = state;
        _placementValidator = placementValidator;
        _messageService = messages;
    }

    public bool TryPlaceCube(CubeItem cubeItem)
    {
        //TODO: Проверки нужно объяденить в одно место
        if (!_towerState.IsCanPlaceByHeight(cubeItem))
        {
            _messageService.ShowMessage(GameEventType.HeightLimit);
            return false;
        }

        if (!_placementValidator.IsValid(cubeItem.Position))
        {
            _messageService.ShowMessage(GameEventType.Missed);
            return false;
        }

        Vector3 newPosition = _towerState.GetNextPosition(_towerParent.position, cubeItem);
        cubeItem.transform.SetParent(_towerParent);
        cubeItem.AnimatePlace(newPosition);

        _towerState.AddCube(cubeItem);
        _messageService.ShowMessage(GameEventType.Placed);
        return true;
    }
}
