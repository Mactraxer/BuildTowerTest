using System.Collections.Generic;
using UnityEngine;

namespace AnyColorBall.Infrastructure
{
    public interface IGameFactory : IService
    {
        List<IReadablePlayerProgress> ProgressReaders { get; }
        List<ISavedPlayerProgress> ProgressWriters { get; }

        void Cleanup();
        GameObject CreateLevel();
        GameObject CreatePlayer(Vector3 position);
    }
}