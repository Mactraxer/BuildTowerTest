using Core.Cube;
using R3;

namespace Core.Stock
{
    public interface IItemCollectionState
    {
        Subject<Unit> OnItemsEndedSteam { get; }
        int Count { get; }

        void Add(CubeItem cubeItem);

        bool Contain(CubeItem cubeItem);

        void Remove(CubeItem cubeItem);
    }
}