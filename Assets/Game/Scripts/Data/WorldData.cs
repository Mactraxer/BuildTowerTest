using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class WorldData
    {
        public List<ItemData> Items;
        public string Level;

        public WorldData(string initialLevel)
        {
            Items = new List<ItemData>();
            Level = initialLevel;
        }
    }
}