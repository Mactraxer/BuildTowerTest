using UnityEngine;

public class CubeItem : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private CubeAnimator _animator;

    public Vector2 Position => _rectTransform.position;
    public float Width => _rectTransform.sizeDelta.x;
    public float Height => _rectTransform.sizeDelta.y;

    public void AnimateMiss()
    {
        _animator.AnimateMiss(() => Destroy(gameObject));
    }

    public void AnimatePlace(Vector3 targetPos)
    {
        _animator.AnimatePlace(targetPos);
    }

    public void AnimateFallDown()
    {
        _animator.AnimateFallDown(Height);
    }
}
