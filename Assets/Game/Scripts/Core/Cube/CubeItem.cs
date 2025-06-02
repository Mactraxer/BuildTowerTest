using System;
using UnityEngine;

public class CubeItem : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private CubeAnimator _animator;
    [SerializeField] private CubeDragHandler _dragHandler;

    public Vector2 Position => RectTransform.position;
    public float Width => RectTransform.rect.width;
    public float Height => RectTransform.rect.height;

    public RectTransform RectTransform => _rectTransform;

    public CubeDragHandler DragHandler => _dragHandler;

    public void AnimateMiss()
    {
        _animator.AnimateMiss(() => Destroy(gameObject));
    }

    public void AnimatePlaceInTower(Vector3 targetPos)
    {
        _animator.AnimatePlaceWithHorizontalOffset(targetPos);
    }

    public void AnimateFallDown()
    {
        _animator.AnimateFallDown(Height);
    }

    public void AnimateDelete()
    {
        AnimateMiss();
    }

    public void AnimatePlace(Vector2 position)
    {
        _animator.AnimatePlace(position);
    }

    public void SetColor(Color color)
    {
        throw new NotImplementedException();
    }
}
