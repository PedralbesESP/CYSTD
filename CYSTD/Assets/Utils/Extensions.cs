using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static void SetX(this Vector3 vector, float x) => vector.x = x;
    public static void SetY(this Vector3 vector, float y) => vector.y = y;
    public static void SetZ(this Vector3 vector, float z) => vector.z = z;
    public static void SetX(this Quaternion quaternion, float x) => quaternion = Quaternion.Euler(x, quaternion.eulerAngles.y, quaternion.eulerAngles.z);
    public static void SetY(this Quaternion quaternion, float y) => quaternion = Quaternion.Euler(quaternion.eulerAngles.x, y, quaternion.eulerAngles.z);
    public static void SetZ(this Quaternion quaternion, float z) => quaternion = Quaternion.Euler(quaternion.eulerAngles.x, quaternion.eulerAngles.y, z);
}