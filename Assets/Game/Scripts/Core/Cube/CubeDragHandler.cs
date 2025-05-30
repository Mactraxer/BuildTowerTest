using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class CubeDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private const int MaxSortingOrder = 1000;

    [Inject] private ICubePlacer _cubePlacer;
    [Inject] private ICubeRemover _cubeRemover;
    [Inject] private ITrashZoneValidator _trashValidator;
    [Inject] private ITowerPlacementValidator _towerPlacementValidator;

    private CubeAnimator _animator;
    private CubeItem _cubeItem;
    private Vector2 _startPosition;
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private int _baseSortingOrder;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _animator = GetComponent<CubeAnimator>();
        _cubeItem = GetComponent<CubeItem>();
        _baseSortingOrder = _canvas != null ? _canvas.sortingOrder : 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = _rectTransform.anchoredPosition;

        SetSorting(onTop: true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetSorting(onTop: false);

        if (_trashValidator.IsInside(transform.position))
        {
            _cubeRemover.Remove(_cubeItem);
        } 
        else if (_towerPlacementValidator.IsValid(_cubeItem.Position))
        {
            _cubePlacer.TryPlaceCube(_cubeItem);
        }
        else
        {
            _cubeItem.AnimateMiss();
        }
    }

    private void SetSorting(bool onTop)
    {
        if (_canvas == null) return;
        _canvas.overrideSorting = true;
        _canvas.sortingOrder = onTop ? MaxSortingOrder : _baseSortingOrder;
    }
}
