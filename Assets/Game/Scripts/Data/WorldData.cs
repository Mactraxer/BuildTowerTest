using System;
using System.Collections.Generic;

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
