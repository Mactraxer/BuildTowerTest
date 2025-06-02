using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    [SerializeField] private CubeTowerCoordinator _coordinator;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private RectTransform _scrollContent;

    public void SpawnCubes(int count, Color[] colors)
    {
        for (int i = 0; i < count; i++)
        {
            var gameObject = Instantiate(_cubePrefab, _scrollContent);
            var cube = gameObject.GetComponent<CubeItem>();
            cube.SetColor(colors[i % colors.Length]);

            _coordinator.RegisterCube(cube.DragHandler);
        }
    }
}

