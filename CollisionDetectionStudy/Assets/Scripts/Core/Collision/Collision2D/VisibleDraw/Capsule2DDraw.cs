/*
 * Description:             Capsule2DDraw.cs
 * Author:                  TangHuan
 * Create Date:             2021/03/23
 */

using System.Collections.Generic;
using TH.Module.Collision2D;
using UnityEngine;

namespace TH.Module.Collision2D
{
    /// <summary>
    /// 2D胶囊体可视化绘制
    /// </summary>
    public class Capsule2DDraw : MonoBehaviour
    {

        [Header("胶囊体长度")]
        public float LineLength = 1;

        [Header("胶囊体半径")]
        public float Radius = 1f;

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

        void Update()
        {
            var forward2d = new Vector2(transform.forward.x, transform.forward.z);
            var pointline = forward2d * LineLength;
            Capsule2D.StartPoint = new Vector2(transform.position.x, transform.position.z - LineLength / 2);
            Capsule2D.PointLine = pointline;
            Capsule2D.Radius = Radius;
        }

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
            Vector3 forward = transform.forward;
            var center = transform.position;
            var halfextents = new Vector2(Radius, LineLength / 2);
            mCenterMeshVerticesList[0] = new Vector3(center.x - halfextents.x, center.y, center.z - halfextents.y);
            mCenterMeshVerticesList[1] = new Vector3(center.x - halfextents.x, center.y, center.z + halfextents.y);
            mCenterMeshVerticesList[2] = new Vector3(center.x + halfextents.x, center.y, center.z + halfextents.y);
            mCenterMeshVerticesList[3] = new Vector3(center.x + halfextents.x, center.y, center.z - halfextents.y);
            mCenterDrawMesh.SetVertices(mCenterMeshVerticesList);
            mCenterDrawMesh.RecalculateNormals();

            if (VerticesCount > 0)
            {
                var right = transform.right;
                var tophalfcirclecenter = new Vector3(center.x, center.y, center.z + halfextents.y);
                var bottlehalfcirclecenter = new Vector3(center.x, center.y, center.z - halfextents.y);
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