using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class Extensions
{
    public static void SetX(this Vector3 vector, float x) => vector.x = x;
    public static void SetY(this Vector3 vector, float y) => vector.y = y;
    public static void SetZ(this Vector3 vector, float z) => vector.z = z;
    public static Quaternion SetX(this Quaternion quaternion, float x) => Quaternion.Euler(x, quaternion.eulerAngles.y, quaternion.eulerAngles.z);
    public static Quaternion SetY(this Quaternion quaternion, float y) => Quaternion.Euler(quaternion.eulerAngles.x, y, quaternion.eulerAngles.z);
    public static Quaternion SetZ(this Quaternion quaternion, float z) => Quaternion.Euler(quaternion.eulerAngles.x, quaternion.eulerAngles.y, z);
    public static Quaternion ClampRotationX(this Quaternion rotation, float minXAngle, float maxXAngle)
    {
        Vector3 eulerAngles = rotation.eulerAngles;
        float x = Mathf.Clamp(eulerAngles.x.ConvertTo180Range(), minXAngle, maxXAngle);
        eulerAngles.x = x.ConvertTo360Range();
        return Quaternion.Euler(eulerAngles);
    }
    public static float ConvertTo180Range(this float value)
    {
        if (value > 180)
        {
            return value - 360; 
        }
        return value;
    }
    public static float ConvertTo360Range(this float value)
    {
        if (value < 0)
        {
            return value + 360;
        }
        return value;
    }
    public static string Vector3ToString(this Vector3 vector)
    {
        return vector.x.ToString() + '/' + vector.y.ToString() + '/' + vector.z.ToString();
    }
    public static Vector3 Vector3FromString(this string vector)
    {
        string[] values = vector.Split('/');
        if (values == null || values?.Length < 3)
        {
            return Vector3.zero;
        }
        float[] finalValues = new float[3];
        for (int i = 0; i < 3; i++)
        {
            finalValues[i] = float.TryParse(values[i], out float f) ? f : 0;
        }
        return new Vector3(finalValues[0], finalValues[1], finalValues[2]);
    }
}