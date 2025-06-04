using Core.Cube;
using UnityEngine;
using Zenject;

public class CubeItemPool : MonoMemoryPool<CubeItem>
{
    protected override void Reinitialize(CubeItem item)
    {
        item.gameObject.SetActive(true);
    }

    protected override void OnDespawned(CubeItem item)
    {
        item.gameObject.SetActive(false);
    }
}
