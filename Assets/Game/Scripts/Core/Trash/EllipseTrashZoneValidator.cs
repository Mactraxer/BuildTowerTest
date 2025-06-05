using UnityEngine;

namespace Core.Trash
{
    public class EllipseTrashZoneValidator : ITrashZoneValidator
    {
        public bool IsInside(RectTransform cubeRect, RectTransform ellipse)
        {
            Vector2 ellipseCenter = GetEllipseCenterWorld(ellipse);
            Vector2 cubeCenter = GetCubeCenterWorld(cubeRect);

            Vector2 localPos = cubeCenter - ellipseCenter;

            float a = ellipse.rect.width * ellipse.lossyScale.x / 2;
            float b = ellipse.rect.height * ellipse.lossyScale.y / 2;

            float equationValue = Mathf.Pow(localPos.x / a, 2) + Mathf.Pow(localPos.y / b, 2);
            return equationValue <= 1f;
        }

        private Vector2 GetEllipseCenterWorld(RectTransform ellipseRect)
        {
            return ellipseRect.TransformPoint(ellipseRect.rect.center);
        }

        private Vector2 GetCubeCenterWorld(RectTransform cube)
        {
            return cube.TransformPoint(cube.rect.center);
        }
    }
}