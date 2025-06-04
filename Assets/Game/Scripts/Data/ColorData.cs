using System;

[Serializable]
public class ColorData
{
    public float Red;
    public float Green;
    public float Blue;
    public float Alpha;

    public ColorData(float r, float g, float b, float a)
    {
        Red = r;
        Green = g;
        Blue = b;
        Alpha = a;
    }
}