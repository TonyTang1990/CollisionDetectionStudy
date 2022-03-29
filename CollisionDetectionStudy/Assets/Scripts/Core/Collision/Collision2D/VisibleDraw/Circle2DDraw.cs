/*
 * Description:             Circle2DDraw.cs
 * Author:                  TangHuan
 * Create Date:             2021/03/23
 */

using System.Collections.Generic;
using TH.Module.Collision2D;
using UnityEngine;

namespace TH.Module.Collision2D
{
    /// <summary>
    /// 2D圆形可视化绘制
    /// </summary>
    public class Circle2DDraw : MonoBehaviour
    {

        [Header("圆半径")]
        public float Radius = 1f;

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
        /// 2D圆形信息
        /// </summary>
        public Circle2D Circle2D;

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
            Circle2D.Center = new Vector2(transform.position.x, transform.position.z);
            Circle2D.Radius = Radius;
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
        /// 更新Circle Mesh数据
        /// </summary>
        private void updateMeshData()
        {
            if (VerticesCount > 1)
            {
                float eachangle = 360f / (VerticesCount - 2);
                Vector3 forward = transform.forward;
                mMeshVerticesList[0] = transform.position;
                for (int i = 1, length = mMeshVerticesList.Count; i < length; i++)
                {
                    Vector3 pos = Quaternion.Euler(0f, eachangle * (i - 1), 0f) * forward * Radius + transform.position;
                    mMeshVerticesList[i] = pos;
                }
                mDrawMesh.SetVertices(mMeshVerticesList);
                mDrawMesh.RecalculateNormals();
            }
        }
    }
}