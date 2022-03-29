/*
 * Description:             Capsule2DVisible.cs
 * Author:                  TONYTANG
 * Create Date:             2022/03/30
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Module.Collision2D
{
    /// <summary>
    /// Capsule2DVisible.cs
    /// Capsule 2D可视化绘制
    /// </summary>
    public class Capsule2DVisible : MonoBehaviour
    {
        /// <summary>
        /// 半圆顶点数量
        /// </summary>
        [Header("半圆顶点数量")]
        public int VerticesCount = 25;

        /// <summary>
        /// 顶点Color
        /// </summary>
        [Header("顶点Color")]
        public Color MeshColor = Color.red;

        /// <summary>
        /// 2D胶囊体信息
        /// </summary>
        [Header("2D胶囊体信息")]
        public Capsule2D Capsule2D;

        /// <summary>
        /// 需要绘制的中间Mesh
        /// </summary>
        private Mesh mCenterDrawMesh;

        /// <summary>
        /// 需要绘制的顶部半圆Mesh
        /// </summary>
        private Mesh mTopCircleDrawMesh;

        /// <summary>
        /// 需要绘制的底部半圆Mesh
        /// </summary>
        private Mesh mBobbleCircleDrawMesh;

        /// <summary>
        /// 中间Mesh顶点列表
        /// </summary>
        private List<Vector3> mCenterMeshVerticesList;

        /// <summary>
        /// 顶部半圆Mesh顶点列表
        /// </summary>
        private List<Vector3> mTopCircleMeshVerticesList;

        /// <summary>
        /// 底部半圆Mesh顶点列表
        /// </summary>
        private List<Vector3> mBottleCircleMeshVerticesList;

        void OnDrawGizmos()
        {
            if (mCenterDrawMesh == null || (mCenterDrawMesh != null && mTopCircleMeshVerticesList.Count != VerticesCount))
            {
                mCenterMeshVerticesList = new List<Vector3>(4);
                mTopCircleMeshVerticesList = new List<Vector3>(VerticesCount);
                mBottleCircleMeshVerticesList = new List<Vector3>(VerticesCount);
                for (int i = 0; i < 4; i++)
                {
                    mCenterMeshVerticesList.Add(Vector3.zero);
                }
                for (int i = 0; i < VerticesCount; i++)
                {
                    mTopCircleMeshVerticesList.Add(Vector3.zero);
                }
                for (int i = 0; i < VerticesCount; i++)
                {
                    mBottleCircleMeshVerticesList.Add(Vector3.zero);
                }
                mCenterDrawMesh = MeshUtilities.CreateMesh(mCenterMeshVerticesList);
                mTopCircleDrawMesh = MeshUtilities.CreateMesh(mTopCircleMeshVerticesList);
                mBobbleCircleDrawMesh = MeshUtilities.CreateMesh(mTopCircleMeshVerticesList);
            }
            updateMeshData();
            Gizmos.color = MeshColor;
            Gizmos.DrawWireMesh(mCenterDrawMesh);
            Gizmos.DrawWireMesh(mTopCircleDrawMesh);
            Gizmos.DrawWireMesh(mBobbleCircleDrawMesh);
        }

        /// <summary>
        /// 更新Mesh数据
        /// </summary>
        private void updateMeshData()
        {
            // 分两部分绘制
            // 1. 胶囊体中心部分(4个顶点)
            // 2. 胶囊体两头部分(各25个顶点)
            Vector3 forward = new Vector3(Capsule2D.PointLine.x, 0, Capsule2D.PointLine.y).normalized;
            var center = Capsule2D.StartPoint;
            var halfextents = new Vector2(Capsule2D.Radius, Capsule2D.PointLine.magnitude / 2);
            mCenterMeshVerticesList[0] = new Vector3(center.x - halfextents.x, 0, center.y - halfextents.y);
            mCenterMeshVerticesList[1] = new Vector3(center.x - halfextents.x, 0, center.y + halfextents.y);
            mCenterMeshVerticesList[2] = new Vector3(center.x + halfextents.x, 0, center.y + halfextents.y);
            mCenterMeshVerticesList[3] = new Vector3(center.x + halfextents.x, 0, center.y - halfextents.y);
            mCenterDrawMesh.SetVertices(mCenterMeshVerticesList);
            mCenterDrawMesh.RecalculateNormals();

            if (VerticesCount > 0)
            {
                var right = Quaternion.Euler(0, 90, 0) * forward;
                var tophalfcirclecenter = new Vector3(center.x, 0, center.y + halfextents.y);
                var bottlehalfcirclecenter = new Vector3(center.x, 0, center.y - halfextents.y);
                float eachangle = 180f / (VerticesCount - 2);
                mTopCircleMeshVerticesList[0] = tophalfcirclecenter;
                for (int i = 1, length = VerticesCount; i < length; i++)
                {
                    Vector3 pos = Quaternion.Euler(0f, eachangle * (i - 1), 0f) * -right * halfextents.x + tophalfcirclecenter;
                    mTopCircleMeshVerticesList[i] = pos;
                }
                mBottleCircleMeshVerticesList[0] = bottlehalfcirclecenter;
                for (int i = 1, length = VerticesCount; i < length; i++)
                {
                    Vector3 pos = Quaternion.Euler(0f, eachangle * (i - 1), 0f) * right * halfextents.x + bottlehalfcirclecenter;
                    mBottleCircleMeshVerticesList[i] = pos;
                }
                mTopCircleDrawMesh.SetVertices(mTopCircleMeshVerticesList);
                mTopCircleDrawMesh.RecalculateNormals();
                mBobbleCircleDrawMesh.SetVertices(mBottleCircleMeshVerticesList);
                mBobbleCircleDrawMesh.RecalculateNormals();
            }
        }
    }
}