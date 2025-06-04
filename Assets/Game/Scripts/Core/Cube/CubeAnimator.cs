using Core.Cube;
using DG.Tweening;
using System;
using UnityEngine;

public class CubeAnimator : MonoBehaviour
{
    [SerializeField] private float _jumpHeight = 50f;
    [SerializeField] private float _jumpDuration = 0.3f;
    [SerializeField] private float _missDuration = 0.5f;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    public void AnimateMiss(Action callback)
    {
        transform.DOScale(Vector3.zero, _missDuration).SetEase(Ease.InBack)
            .OnComplete(() => callback?.Invoke());
    }

    public void AnimatePlaceWithHorizontalOffset(Vector3 targetPos, Action callback)
    {
        Sequence sequence = DOTween.Sequence().OnComplete(() =>
        {
            /*_rectTransform.pivot = new Vector2(0.5f, 0.5f);
            _rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            _rectTransform.anchorMax = new Vector2(0.5f, 0.5f);*/
            callback?.Invoke();
        });
        sequence.Append(_rectTransform.DOAnchorPosY(_rectTransform.anchoredPosition.y + _jumpHeight, _jumpDuration / 2f).SetEase(Ease.OutQuad));
        sequence.Append(_rectTransform.DOAnchorPosY(targetPos.y, _jumpDuration / 2f).SetEase(Ease.InQuad));
        sequence.Join(_rectTransform.DOAnchorPosX(targetPos.x, _jumpDuration));
    }

    public void AnimateFallDown(float height)
    {
        _rectTransform.DOAnchorPosY(_rectTransform.anchoredPosition.y - height, 0.25f).SetEase(Ease.InOutQuad);
    }

    public void AnimatePlace(Vector2 position)
    {
        _rectTransform.DOAnchorPos(position, 0.25f).SetEase(Ease.InOutQuad);
    }
}