using Core.Cube;
using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services
{
    public interface ICubeFactory
    {
        List<ISavedPlayerProgress> ProgressWriters { get; }

        CubeItem CreateCubeItem();

        CubeItem[] CreateCubeItems();

        void DeleteCubeItem(CubeItem cubeItem);

        Sprite GetSpriteById(int spriteId);

        bool NeedAddNewItem(int stockCount);
    }
}