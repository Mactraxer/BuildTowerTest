using DG.Tweening;
using UnityEngine;

public class TowerCubeRemover : ICubeRemover
{
    private readonly ITowerState _towerState;
    private readonly IMessageService _messageService;

    public TowerCubeRemover(ITowerState state, IMessageService messages)
    {
        _towerState = state;
        _messageService = messages;
    }

    public void Remove(CubeItem cubeItem)
    {
        int removedIndex = _towerState.RemoveCube(cubeItem);
        Object.Destroy(cubeItem.gameObject);
        _messageService.ShowMessage(GameEventType.Removed);

        for (int i = removedIndex; i < _towerState.CubeCount; i++)
        {
            _towerState.Cubes[i].AnimateFallDown();
        }
    }
}
