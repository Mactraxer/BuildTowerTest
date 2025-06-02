using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CubeTowerCoordinator : MonoBehaviour
{
    [Inject] private readonly ICubePlacer _cubePlacer;
    [Inject] private readonly ICubeRemover _cubeRemover;
    [Inject] private readonly ITrashZoneValidator _trashValidator;
    [Inject] private readonly ITowerState _towerState;
    [Inject] private readonly IMessageService _messageService;

    private readonly List<CompositeDisposable> _cubeSubscriptions = new();

    private Vector2 _selectedCubeStartPosition;

    public void RegisterCube(CubeDragHandler handler)
    {
        var disposable = new CompositeDisposable();

        handler.OnBeginDragStream
            .Subscribe(startPosition => _selectedCubeStartPosition = startPosition)
            .AddTo(disposable);

        handler.OnDragStream
            .Subscribe()
            .AddTo(disposable);

        handler.OnDropStream
            .Subscribe(cubeItem => HandleDrop(cubeItem))
            .AddTo(disposable);

        _cubeSubscriptions.Add(disposable);
    }

    private void HandleDrop(CubeItem cube)
    {
        if (_trashValidator.IsInside(cube.RectTransform.anchoredPosition))
        {
            if (_towerState.Contain(cube))
            {
                _cubeRemover.Remove(cube);
                _messageService.ShowMessage(GameEventType.Removed);
            }
            else
            {
                cube.AnimateMiss();
                _messageService.ShowMessage(GameEventType.RemovedFailed);
            }
        }

        DropResult dropResult = _cubePlacer.TryPlaceCube(cube);
        if (dropResult.Success)
        {
            _messageService.ShowMessage(GameEventType.Placed);
            return;
        }

        switch (dropResult.DropError)
        {
            case DropError.HeightLimit:
                _messageService.ShowMessage(GameEventType.HeightLimit);
                break;
            case DropError.MissToTower:
                _messageService.ShowMessage(GameEventType.Missed);
                break;
            case DropError.MissFromTower:
                cube.AnimatePlace(_selectedCubeStartPosition);
                break;
        }
    }

    private void OnDestroy()
    {
        foreach (var disposable in _cubeSubscriptions)
            disposable.Dispose();
    }
}
