using Core.Cube;
using System;

namespace Data
{
    [Serializable]
    public class ItemData
    {
        public PositionOnLevel PositionOnLevel;
        public int ID;
        public CubeState State;
        public int SpriteId;

        public ItemData()
        {
            PositionOnLevel = new PositionOnLevel("DemoLevel");
            ID = 0;
            State = CubeState.Disposed;
            SpriteId = 0;
        }
    }
}