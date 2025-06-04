using Core.Cube;
using R3;

public interface IItemCollectionState
{
    Subject<Unit> OnItemsEndedSteam { get; }

    void Add(CubeItem cubeItem);
    bool Contain(CubeItem cubeItem);
    void Remove(CubeItem cubeItem);
}