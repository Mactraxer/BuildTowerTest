using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure.Services
{
    public class AssetProvider : IAssetProvider
    {
        [Inject] private DiContainer _diContainer;

        public GameObject Instantiate(string path, Vector3 position)
        {
            var prefab = Resources.Load<GameObject>(path);
            return _diContainer.InstantiatePrefab(prefab, position, Quaternion.identity, null);
        }

        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            GameObject gameObject = _diContainer.InstantiatePrefab(prefab);
            return gameObject;
        }
    }
}