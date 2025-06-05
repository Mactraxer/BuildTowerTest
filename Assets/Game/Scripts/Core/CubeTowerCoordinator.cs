using Core.Cube;
using Core.Stock;
using Core.Tower;
using Core.Trash;
using Infrastructure.Services;
using R3;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core
{
    public class CubeTowerCoordinator : MonoBehaviour
    {
        [Inject] private readonly ICubePlacer _cubePlacer;
        [Inject] private readonly ICubeRemover _cubeRemover;
        [Inject] private readonly ITrashZoneValidator _trashValidator;
        [Inject] private readonly ITowerState _towerState;
        [Inject] private readonly IMessageService _messageService;
        [Inject] private readonly ICubeFactory _cubeFactory;
        [Inject] private readonly IItemCollectionState _itemCollectionState;
        [Inject] private readonly ISaveLoadService _saveLoadService;

        private readonly List<CompositeDisposable> _cubeSubscriptions = new();
        [SerializeField] private RectTransform _scrollContent;
        [SerializeField] private RectTransform _dragLayer;
        [SerializeField] private RectTransform _towerParent;
        [SerializeField] private RectTransform _trashArea;
        private Vector2 _selectedCubeStartPosition;

        public void RegisterCube(CubeItem cubeItem, CubeDragHandler cubeDragHandler)
        {
            var disposable = new CompositeDisposable();

            cubeDragHandler.Setup(_dragLayer);
            cubeDragHandler.OnBeginDragStream.Subscribe(cubeItem => HandleStartDrag(cubeItem)).AddTo(disposable);
            cubeDragHandler.OnDragStream.Subscribe().AddTo(disposable);
            cubeDragHandler.OnDropStream.Subscribe(cubeItem => HandleDrop(cubeItem)).AddTo(disposable);

            cubeItem.OnItemPlaced.Subscribe(_ => ItemPlaced()).AddTo(disposable);

            _cubeSubscriptions.Add(disposable);

            if (cubeItem.State == CubeState.InTower)
            {
                _cubePlacer.Warp(cubeItem, to: cubeItem.RectTransform.anchoredPosition, inContainer: _towerParent);
                cubeItem.RectTransform.localScale = Vector2.one * 1.2f;
            }
            else if (cubeItem.State == CubeState.InStock)
            {
                _itemCollectionState.Add(cubeItem);
                cubeItem.transform.SetParent(_scrollContent);
                cubeItem.RectTransform.localScale = Vector2.one * 1.2f;
            }
        }

        private void ItemPlaced()
        {
            _saveLoadService.SaveProgress();
        }

        private void HandleStartDrag(CubeItem cubeItem)
        {
            _selectedCubeStartPosition = cubeItem.RectTransform.anchoredPosition;
            if (_itemCollectionState.Contain(cubeItem))
            {
                _itemCollectionState.Remove(cubeItem);
                AddNewItemIfNeed();
            }
        }

        private void AddNewItemIfNeed()
        {
            if (_cubeFactory.NeedAddNewItem(_itemCollectionState.Count))
            {
                CubeItem cubeItem = _cubeFactory.CreateCubeItem();
                RegisterCube(cubeItem, cubeItem.GetComponent<CubeDragHandler>());
            }
        }

        private void HandleDrop(CubeItem cube)
        {
            if (_trashValidator.IsInside(cube.RectTransform, _trashArea))
            {
                if (_towerState.Contain(cube))
                {
                    _cubeRemover.Remove(cube);
                    DeleteCube(cube);
                    _messageService.ShowMessage(GameEventType.Removed);
                }
                else
                {
                    cube.AnimatePlace(_selectedCubeStartPosition);
                    _messageService.ShowMessage(GameEventType.RemovedFailed);
                }

                return;
            }

            DropResult dropResult = _cubePlacer.TryPlaceCube(cube, inContainer: _towerParent, _dragLayer);
            if (dropResult.Success)
            {
                _messageService.ShowMessage(GameEventType.Placed);
                return;
            }

            switch (dropResult.DropError)
            {
                case DropError.HeightLimit:
                    DeleteCube(cube);
                    _messageService.ShowMessage(GameEventType.HeightLimit);
                    break;

                case DropError.MissToTower:
                    DeleteCube(cube);
                    _messageService.ShowMessage(GameEventType.Missed);
                    break;

                case DropError.MissFromTower:
                    cube.AnimatePlace(_selectedCubeStartPosition);
                    break;
            }
        }

        private void DeleteCube(CubeItem cube)
        {
            cube.ChangeState(CubeState.Disposed);
            cube.AnimateDelete(() =>
            {
                _cubeFactory.DeleteCubeItem(cube);
                _saveLoadService.SaveProgress();
            });
        }

        private void HandleItemEnded()
        {
        }

        private void Start()
        {
            var disposable = new CompositeDisposable();
            _itemCollectionState.OnItemsEndedSteam.Subscribe(_ => HandleItemEnded()).AddTo(disposable);
            _cubeSubscriptions.Add(disposable);

            _towerState.SetMaxY(_dragLayer.rect.height);
        }

        private void OnDestroy()
        {
            foreach (var disposable in _cubeSubscriptions)
                disposable.Dispose();
        }
    }
}