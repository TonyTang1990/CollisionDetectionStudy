/*
 * Description:             Sector2DDraw.cs
 * Author:                  TangHuan
 * Create Date:             2021/03/23
 */

using System.Collections.Generic;
using TH.Module.Collision2D;
using UnityEngine;

namespace TH.Module.Collision2D
{
    /// <summary>
    /// 2D扇形可视化绘制
    /// </summary>
    public class Sector2DDraw : MonoBehaviour
    {
        /// <summary>
        /// 扇形半径
        /// </summary>
        [Header("扇形半径")]
        public float Radius = 1;

        /// <summary>
        /// 扇形角度
        /// </summary>
        [Header("扇形角度")]
        public float Angle = 60f;

        /// <summary>
        /// 顶点数量
        /// </summary>
        [Header("顶点数量")]
        public int VerticesCount = 100;

        /// <summary>
        /// 顶点Color
        /// </summary>
        [Header("顶点Color")]
        public Color MeshColor = Color.red;

        /// <summary>
        /// 2D扇形信息
        /// </summary>
        public Sector2D Sector2D;

        /// <summary>
        /// 需要绘制的Mesh
        /// </summary>
        private Mesh mDrawMesh;

        /// <summary>
        /// Mesh顶点列表
        /// </summary>
        private List<Vector3> mMeshVerticesList;

        void Update()
        {
            var forward2d = new Vector2(transform.forward.x, transform.forward.z);
            Sector2D.StartPoint = new Vector2(transform.position.x, transform.position.z);
            Sector2D.Radius = Radius;
            Sector2D.Direction = forward2d;
            Sector2D.Angle = Angle;
        }

        void OnDrawGizmos()
        {
            if (mDrawMesh == null || (mDrawMesh != null && mMeshVerticesList.Count != VerticesCount))
            {
                mMeshVerticesList = new List<Vector3>(VerticesCount);
                for (int i = 0; i < VerticesCount; i++)
                {
                    mMeshVerticesList.Add(Vector3.zero);
                }
                mDrawMesh = MeshUtilities.CreateMesh(mMeshVerticesList);
            }
            updateMeshData();
            Gizmos.color = MeshColor;
            Gizmos.DrawWireMesh(mDrawMesh);
        }

        /// <summary>
        /// 更新Mesh数据
        /// </summary>
        private void updateMeshData()
        {
            // -2是因为要排除原点和扇形第一个边的点
            float eachangle = Angle / (VerticesCount - 2);
            Vector3 forward = transform.forward;
            mMeshVerticesList[0] = transform.position;
            for (int i = 1; i < VerticesCount; i++)
            {
                Vector3 pos = Quaternion.Euler(0f, -Angle / 2 + eachangle * (i - 1), 0f) * forward * Radius + transform.position;
                mMeshVerticesList[i] = pos;
            }
            mDrawMesh.SetVertices(mMeshVerticesList);
            mDrawMesh.RecalculateNormals();
        }
    }
}