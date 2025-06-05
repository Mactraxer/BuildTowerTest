using Data;
using UnityEngine;

namespace Extensions
{
    public static class DataExtensions
    {
        public static Vector3 AsUnityVector(this Vector2Data vector3Data)
        {
            return new Vector3(vector3Data.X, vector3Data.Y);
        }

        public static Vector2Data AsVectorData(this Vector2 vector)
        {
            return new Vector2Data(vector.x, vector.y);
        }

        public static string ToJson(this object obj)
        {
            return JsonUtility.ToJson(obj);
        }

        public static T ToDesearialized<T>(this string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public static Color AsUnityColor(this ColorData colorData)
        {
            return new Color(colorData.Red, colorData.Green, colorData.Blue, colorData.Alpha);
        }

        public static ColorData AsColorData(this Color color)
        {
            return new ColorData(color.r, color.g, color.b, color.a);
        }
    }
}