using Core.Cube;
using UnityEngine;

public interface ICubeFactory
{
    CubeItem[] CreateCubeItems(int count);
    CubeItem[] CreateCubeItems();
    void DeleteCubeItem(CubeItem cubeItem);
    Sprite GetSpriteById(int spriteId);
}