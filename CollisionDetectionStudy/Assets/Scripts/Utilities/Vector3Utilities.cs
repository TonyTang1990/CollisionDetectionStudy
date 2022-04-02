/*
 * Description:             Vector3Utilities.cs
 * Author:                  TONYTANG
 * Create Date:             2022/04/02
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Vector3Utilities.cs
/// Vector3工具类
/// </summary>
public static class Vector3Utilities
{
    /// <summary>
    /// 4个点是否共面
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    public static bool IsCoplanarVector(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        // 通过叉乘计算CA和CD的法线向量
        var ab = b - a;
        var ca = a - c;
        var cd = d - c;
        var normal = Vector3.Cross(ca, cd);
        // 通过ab向量和CA和CD法线向量的点乘是否为0来判定是否垂直
        // a,b,c,d从而判定出是否共面
        if(Mathf.Approximately(Vector3.Dot(normal, ab), Mathf.Epsilon))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 4个点是否ab和cd线段是否相交
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    public static bool IsCross(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        // 通过叉乘计算CA和CD的法线向量
        var ab = b - a;
        var ac = c - a;
        var ad = d - a;
        // 叉乘判定两个向量的方向
        // 判定ab是否在cd两侧
        // 以ab为基准，如果ac和ad叉乘结果再点乘>0则表示c和d在ab同一侧
        // 3D向量叉乘是向量，通过计算两个叉乘向量的点乘判定是否同方向从而判定是否在一侧
        if (Vector3.Dot(Vector3.Cross(ab, ac), Vector3.Cross(ab, ad)) <= 0)
        {
            return false;
        }
        // 同理以cb为基准，如果ca和cb叉乘结果再点乘>0表示a和b在cd同一侧
        var ca = a - c;
        var cb = b - c;
        var cd = d - c;
        if (Vector3.Dot(Vector3.Cross(cd, ca), Vector3.Cross(cd, cb)) <= 0)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 计算AB与CD两条线段的交点.
    /// </summary>
    /// <param name="a">A点</param>
    /// <param name="b">B点</param>
    /// <param name="c">C点</param>
    /// <param name="d">D点</param>
    /// <param name="intersectPos">AB与CD的交点</param>
    /// <returns>是否相交 true:相交 false:未相交</returns>
    public static bool GetIntersectPoint(Vector2 a, Vector2 b, Vector2 c, Vector2 d, out Vector2 intersectPos)
    {
        intersectPos = Vector2.zero;
        // Note:
        // 1. 3D向量需要判定共面
        if(!IsCoplanarVector(a, b, c, d))
        {
            return false;
        }

        if (!IsCross(a, b, c, d))
        {
            return false;
        }
        // 根据推到结论
        // AO / AB = Cross(CD, CA) / Cross(CD, AB)
        // O = A + Cross(CD, CA) / Cross(CD, AB) * AB
        var ab = b - a;
        var ca = a - c;
        var cd = d - c;
        Vector3 v1 = Vector3.Cross(cd, ca);
        Vector3 v2 = Vector3.Cross(cd, ab);
        float ratio = Vector3.Dot(v1, v2) / v2.sqrMagnitude;
        intersectPos = a + ab * ratio;
        return true;
    }
}