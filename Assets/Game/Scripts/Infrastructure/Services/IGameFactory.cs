using Core.Cube;
using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services
{
    public interface IGameFactory : IService
    {
        List<IReadablePlayerProgress> ProgressReaders { get; }
        List<ISavedPlayerProgress> ProgressWriters { get; }

        void Cleanup();

        CubeItem[] CreateItems();

        GameObject CreateLevel();
    }
}