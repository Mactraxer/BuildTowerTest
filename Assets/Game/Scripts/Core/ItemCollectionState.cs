using Core.Cube;
using R3;
using System.Collections.Generic;

public class ItemCollectionState : IItemCollectionState
{
    public Subject<Unit> OnItemsEndedSteam => onItemsEndedSteam;

    public Subject<Unit> onItemsEndedSteam = new();
    private readonly List<CubeItem> _cubeItems = new();

    public void Add(CubeItem cubeItem)
    {
        _cubeItems.Add(cubeItem);
    }

    public bool Contain(CubeItem cubeItem)
    {
        return _cubeItems.Contains(cubeItem);
    }

    public void Remove(CubeItem cubeItem)
    {
        _cubeItems.Remove(cubeItem);
        if (_cubeItems.Count == 0)
        {
            onItemsEndedSteam.OnNext(Unit.Default);
        }
    }
}