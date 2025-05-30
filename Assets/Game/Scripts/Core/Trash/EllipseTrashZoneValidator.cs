using UnityEngine;

public class EllipseTrashZoneValidator : ITrashZoneValidator
{
    private readonly RectTransform _ellipse;

    public EllipseTrashZoneValidator(RectTransform ellipse)
    {
        _ellipse = ellipse;
    }

    public bool IsInside(Vector2 pos)
    {
        Vector2 center = _ellipse.position;
        Vector2 size = _ellipse.rect.size * _ellipse.lossyScale;
        float dx = (pos.x - center.x) / (size.x / 2);
        float dy = (pos.y - center.y) / (size.y / 2);
        return dx * dx + dy * dy <= 1;
    }
}
