﻿using System;

namespace Data
{
    [Serializable]
    public class Vector2Data
    {
        public float X;
        public float Y;

        public Vector2Data(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}