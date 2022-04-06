/*
 * Description:             Vector2Utilities.cs
 * Author:                  TONYTANG
 * Create Date:             2022/04/02
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Vector2Utilities.cs
/// Vector2工具类
/// </summary>
public static class Vector2Utilities
{
    /// <summary>
    /// 2D向量叉乘
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static float Cross(Vector2 a, Vector2 b)
    {
        return a.x * b.y - b.x * a.y;
    }

    /// <summary>
    /// 4个点是否ab和cd线段是否平行
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    public static bool IsParallel(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
    {
        // 通过叉乘计算AB和CD是否为0判定是否平行
        var ab = b - a;
        var cd = d - c;
        return Mathf.Approximately(Vector2Utilities.Cross(ab, cd), Mathf.Epsilon);
    }

    /// <summary>
    /// 4个点是否ab和cd线段是否相交
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    public static bool IsCross(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
    {
        // 通过叉乘计算CA和CD的法线向量
        var ab = b - a;
        var ac = c - a;
        var ad = d - a;
        // 叉乘判定两个向量的方向
        // 判定ab是否在cd两侧
        // 以ab为基准，如果ab叉乘ac或ad大于0表示逆时针反之顺时针,0表示两者方向相同
        // 那么c和d要在ab同一侧，则ab叉乘ac的结果和ab叉乘ad的结果相乘需要>0
        if (Vector2Utilities.Cross(ab, ac) * Vector2Utilities.Cross(ab, ad) >= 0)
        {
            return false;
        }
        // 同理以cd为基准，如果ca和cb叉乘结果相乘>0则表示a和b在cd同一侧
        var ca = a - c;
        var cb = b - c;
        var cd = d - c;
        if (Vector2Utilities.Cross(cd, ca) * Vector2Utilities.Cross(cd, cb) >= 0)
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
        // 1. 2D向量不需要判定共面
        if (!IsCross(a, b, c, d))
        {
            return false;
        }
        // 根据推到结论
        // AO / AB = Cross(CA, CD) / Cross(CD, AB)
        // O = A + Cross(CA, CD) / Cross(CD, AB) * AB
        var ab = b - a;
        var ca = a - c;
        var cd = d - c;
        Vector3 v1 = Vector3.Cross(ca, cd);
        Vector3 v2 = Vector3.Cross(cd, ab);
        // 这一步计算比例无视正负
        float ratio = Vector3.Dot(v1, v2) / v2.sqrMagnitude;
        intersectPos = a + ab * ratio;
        return true;
    }

    /// <summary>
    /// 计算AB与CD两条线段的交点(包含衍生直线)
    /// </summary>
    /// <param name="a">A点</param>
    /// <param name="b">B点</param>
    /// <param name="c">C点</param>
    /// <param name="d">D点</param>
    /// <param name="intersectPos">AB与CD的交点</param>
    /// <returns>是否相交 true:相交 false:未相交</returns>
    public static bool GetLineIntersectPoint(Vector2 a, Vector2 b, Vector2 c, Vector2 d, out Vector2 intersectPos)
    {
        intersectPos = Vector2.zero;
        // Note:
        // 1. 2D向量不需要判定共面
        if (IsParallel(a, b, c, d))
        {
            return false;
        }
        // 根据推到结论
        // AO / AB = Cross(CA, CD) / Cross(CD, AB)
        // O = A + Cross(CA, CD) / Cross(CD, AB) * AB
        var ab = b - a;
        var ca = a - c;
        var cd = d - c;
        Vector3 v1 = Vector3.Cross(ca, cd);
        Vector3 v2 = Vector3.Cross(cd, ab);
        float ratio = Vector3.Dot(v1, v2) / v2.sqrMagnitude;
        intersectPos = a + ab * ratio;
        return true;
    }
}