/*
 * Description:             GameLauncher.cs
 * Author:                  TangHuan
 * Create Date:             2021/03/23
 */

using System.Collections.Generic;
using TH.Module.Collision2D;
using TH.Module.Collision3D;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 启动脚本
/// </summary>
[ExecuteInEditMode]
public class GameLauncher : MonoBehaviour
{
    [Header("圆形绘制对象脚本1")]
    public Circle2DVisible Circle2DVisible1;

    [Header("2D点对象")]
    public Transform Circle2DPoint;

    [Header("圆形绘制对象脚本2")]
    public Circle2DVisible Circle2DVisible2;

    [Header("AABB绘制对象脚本")]
    public AABB2DVisible AABB2DVisible;

    [Header("AABB绘制对象脚本2")]
    public AABB2DVisible AABB2DVisible2;

    [Header("OBB绘制对象脚本")]
    public OBB2DVisible OBB2DVisible;

    [Header("胶囊体绘制对象脚本")]
    public Capsule2DVisible Capsule2DVisible;

    [Header("扇形绘制对象脚本")]
    public Sector2DVisible Sector2DVisible;

    [Header("多边形绘制对象脚本")]
    public Polygon2DVisible Polygon2DVisible;

    [Header("凸多边形绘制对象脚本")]
    public Polygon2DVisible Polygon2DVisible2;

    [Header("凸多边形绘制对象脚本")]
    public Polygon2DVisible Polygon2DVisible3;

    [Header("球体绘制对象脚本1")]
    public Sphere3DVisible Sphere3DVisible1;

    [Header("3D点对象")]
    public Transform Circle3DPoint;

    [Header("球体绘制对象脚本2")]
    public Sphere3DVisible Sphere3DVisible2;

    [Header("可视化原点对象")]
    public Transform VisiblePoint;

    /// <summary>
    /// 输出文本
    /// </summary>
    [Header("输出文本")]
    public Text OutPutText;

    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        CheckCollisionDetection();
    }

    /// <summary>
    /// 更新坐标
    /// </summary>
    private void UpdatePosition()
    {
        Circle2DVisible1.Circle2D.Center.x = Circle2DVisible1.transform.position.x;
        Circle2DVisible1.Circle2D.Center.y = Circle2DVisible1.transform.position.z;

        Circle2DVisible2.Circle2D.Center.x = Circle2DVisible2.transform.position.x;
        Circle2DVisible2.Circle2D.Center.y = Circle2DVisible2.transform.position.z;

        AABB2DVisible.AABB2D.Center.x = AABB2DVisible.transform.position.x;
        AABB2DVisible.AABB2D.Center.y = AABB2DVisible.transform.position.z;

        AABB2DVisible2.AABB2D.Center.x = AABB2DVisible2.transform.position.x;
        AABB2DVisible2.AABB2D.Center.y = AABB2DVisible2.transform.position.z;

        OBB2DVisible.OBB2D.Center.x = OBB2DVisible.transform.position.x;
        OBB2DVisible.OBB2D.Center.y = OBB2DVisible.transform.position.z;

        Capsule2DVisible.Capsule2D.StartPoint.x = Capsule2DVisible.transform.position.x - Capsule2DVisible.Capsule2D.PointLine.x / 2;
        Capsule2DVisible.Capsule2D.StartPoint.y = Capsule2DVisible.transform.position.z - Capsule2DVisible.Capsule2D.PointLine.y / 2;

        Sector2DVisible.Sector2D.StartPoint.x = Sector2DVisible.transform.position.x;
        Sector2DVisible.Sector2D.StartPoint.y = Sector2DVisible.transform.position.z;

        Sphere3DVisible1.Sphere3D.Center = Sphere3DVisible1.transform.position;

        Sphere3DVisible2.Sphere3D.Center = Sphere3DVisible2.transform.position;
    }

    /// <summary>
    /// 检查相交检测
    /// </summary>
    private void CheckCollisionDetection()
    {
        var outputtext = string.Empty;
        if ((Circle2DVisible1 != null && Circle2DPoint != null) &&
            Collision2DUtilities.CircleAndPointIntersection(Circle2DVisible1.Circle2D, new Vector2(Circle2DPoint.position.x, Circle2DPoint.position.z)))
        {
            outputtext += $"{Circle2DVisible1.name}和{Circle2DPoint.name}相交!\n";
        }
        if ((AABB2DVisible != null && Circle2DPoint != null) &&
            Collision2DUtilities.PointInAABB(AABB2DVisible.AABB2D, new Vector2(Circle2DPoint.position.x, Circle2DPoint.position.z)))
        {
            outputtext += $"{AABB2DVisible.name}和{Circle2DPoint.name}相交!\n";
        }
        if ((Circle2DVisible1 != null && Circle2DVisible2 != null) &&
            Collision2DUtilities.CircleAndCircleIntersection(Circle2DVisible1.Circle2D, Circle2DVisible2.Circle2D))
        {
            outputtext += $"{Circle2DVisible1.name}和{Circle2DVisible2.name}相交!\n";
        }
        if ((Circle2DVisible1 != null && Capsule2DVisible != null) &&
            Collision2DUtilities.CircleAndCapsuleIntersection(Circle2DVisible1.Circle2D, Capsule2DVisible.Capsule2D))
        {
            outputtext += $"{Circle2DVisible1.name}和{Capsule2DVisible.name}相交!\n";
        }
        if ((Circle2DVisible1 != null && Sector2DVisible != null) &&
            Collision2DUtilities.CircleAndSectorIntersection(Circle2DVisible1.Circle2D, Sector2DVisible.Sector2D))
        {
            outputtext += $"{Circle2DVisible1.name}和{Sector2DVisible.name}相交!\n";
        }
        if ((Circle2DVisible1 != null && AABB2DVisible != null) &&
            Collision2DUtilities.CircleAndAABBIntersection(Circle2DVisible1.Circle2D, AABB2DVisible.AABB2D))
        {
            outputtext += $"{Circle2DVisible1.name}和{AABB2DVisible.name}相交!\n";
        }
        if ((Circle2DVisible1 != null && OBB2DVisible != null) &&
            Collision2DUtilities.CircleAndOBBIntersection(Circle2DVisible1.Circle2D, OBB2DVisible.OBB2D))
        {
            outputtext += $"{Circle2DVisible1.name}和{OBB2DVisible.name}相交!\n";
        }
        if ((Circle2DVisible1 != null && Polygon2DVisible != null) &&
    Collision2DUtilities.CircleAndPolygonIntersection(Circle2DVisible1.Circle2D, Polygon2DVisible.Polygon2D))
        {
            outputtext += $"{Circle2DVisible1.name}和{Polygon2DVisible.name}相交!\n";
        }
        if ((Polygon2DVisible != null && Circle2DPoint != null) &&
            Collision2DUtilities.PointInPolygon(Polygon2DVisible.Polygon2D, new Vector2(Circle2DPoint.position.x, Circle2DPoint.position.z)))
        {
            outputtext += $"{Polygon2DVisible.name}和{Circle2DPoint.name}相交!\n";
        }
        if ((Polygon2DVisible2 != null && Circle2DPoint != null) &&
            Collision2DUtilities.PointInConvexPolygon(Polygon2DVisible2.Polygon2D, new Vector2(Circle2DPoint.position.x, Circle2DPoint.position.z)))
        {
            outputtext += $"{Polygon2DVisible2.name}和{Circle2DPoint.name}相交!\n";
        }
        if ((AABB2DVisible != null && AABB2DVisible2 != null) &&
    Collision2DUtilities.AABBAndAABBIntersection(AABB2DVisible.AABB2D, AABB2DVisible2.AABB2D))
        {
            outputtext += $"{AABB2DVisible.name}和{AABB2DVisible2.name}相交!\n";
        }
        if ((Polygon2DVisible3 != null && Circle2DPoint != null) &&
            Collision2DUtilities.BetterPointInConvexPolygon(Polygon2DVisible3.Polygon2D, new Vector2(Circle2DPoint.position.x, Circle2DPoint.position.z)))
        {
            outputtext += $"{Polygon2DVisible3.name}和{Circle2DPoint.name}相交!\n";
        }
        if ((Sphere3DVisible1 != null && Circle3DPoint != null) &&
            Collision3DUtilities.SphereAndPointIntersection3D(Sphere3DVisible1.Sphere3D, Circle3DPoint.position))
        {
            outputtext += $"{Sphere3DVisible1.name}和{Circle3DPoint.name}相交!\n";
        }
        if ((Sphere3DVisible1 != null && Sphere3DVisible2 != null) &&
            Collision3DUtilities.SphereAndSphereIntersection3D(Sphere3DVisible1.Sphere3D, Sphere3DVisible2.Sphere3D))
        {
            outputtext += $"{Sphere3DVisible1.name}和{Sphere3DVisible2.name}相交!\n";
        }
        if (string.IsNullOrEmpty(outputtext))
        {
            outputtext = $"无任何对象相交!";
        }
        OutPutText.text = outputtext;
    }
}