using Core.Cube;
using Zenject;

namespace Infrastructure.Pool
{
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
}