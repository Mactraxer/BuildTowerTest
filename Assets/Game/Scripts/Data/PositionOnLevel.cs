using System;

[Serializable]
public class PositionOnLevel
{
    public string Level;
    public Vector2Data Vector3Data;

    public PositionOnLevel(string initialLevel)
    {
        Level = initialLevel;
    }
}
