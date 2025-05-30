using DG.Tweening;
using System;
using UnityEngine;

public class CubeAnimator : MonoBehaviour
{
    [SerializeField] private float _jumpHeight = 50f;
    [SerializeField] private float _jumpDuration = 0.3f;
    [SerializeField] private float _missDuration = 0.5f;

    public void AnimateMiss(Action callback)
    {
        transform.DOScale(Vector3.zero, _missDuration).SetEase(Ease.InBack)
            .OnComplete(() => callback?.Invoke());
    }

    public void AnimatePlace(Vector3 targetPos)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(transform.position.y + _jumpHeight, _jumpDuration / 2f).SetEase(Ease.OutQuad));
        seq.Append(transform.DOMoveY(targetPos.y, _jumpDuration / 2f).SetEase(Ease.InQuad));
        seq.Join(transform.DOMoveX(targetPos.x, _jumpDuration));
    }

    public void AnimateFallDown(float height)
    {
        transform.DOMoveY(transform.position.y - height, 0.25f).SetEase(Ease.InOutQuad);
    }
}