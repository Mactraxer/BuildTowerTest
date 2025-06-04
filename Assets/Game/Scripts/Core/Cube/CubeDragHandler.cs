using Core.Cube;
using R3;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Zenject;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CubeItem))]
public class CubeDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private const int MaxSortingOrder = 1000;

    public readonly Subject<CubeItem> OnBeginDragStream = new();
    public readonly Subject<Unit> OnDragStream = new();
    public readonly Subject<CubeItem> OnDropStream = new();

    private Transform _dragLayer;
    private CubeItem _cubeItem;
    private Canvas _canvas;
    private RectTransform _rectTransform;
    private Transform _origineParent;
    private int _baseSortingOrder;
    private Vector2 _dragOffset;
    private Vector2 _startPosition;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _cubeItem = GetComponent<CubeItem>();
        _baseSortingOrder = _canvas != null ? _canvas.sortingOrder : 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        _origineParent = transform.parent;
        _startPosition = _rectTransform.anchoredPosition;
        transform.SetParent(_dragLayer);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_dragLayer as RectTransform, eventData.position, null, out var localPoint);

        _dragOffset = _rectTransform.anchoredPosition - localPoint;
        SetSorting(onTop: true);
        OnBeginDragStream.OnNext(_cubeItem);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_dragLayer as RectTransform, eventData.position, null, out var localPoint))
        {
            Vector2 dragPosition = localPoint + _dragOffset;
            _rectTransform.anchoredPosition = dragPosition;
            OnDragStream.OnNext(Unit.Default);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetSorting(onTop: false);
        OnDropStream.OnNext(_cubeItem);
    }

    public void Setup(RectTransform dragLayer)
    {
        _dragLayer = dragLayer;
    }

    private void SetSorting(bool onTop)
    {
        if (_canvas == null) return;
        _canvas.overrideSorting = true;
        _canvas.sortingOrder = onTop ? MaxSortingOrder : _baseSortingOrder;
    }

}
