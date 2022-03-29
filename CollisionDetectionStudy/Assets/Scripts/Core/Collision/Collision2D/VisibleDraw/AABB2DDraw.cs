/*
 * Description:             AABB2DDraw.cs
 * Author:                  TangHuan
 * Create Date:             2021/03/23
 */

using System.Collections.Generic;
using TH.Module.Collision2D;
using UnityEngine;

namespace TH.Module.Collision2D
{
    /// <summary>
    /// AABB可视化绘制
    /// </summary>
    public class AABB2DDraw : MonoBehaviour
    {
        /// <summary>
        /// 宽和高
        /// </summary>
        [Header("宽和高")]
        public Vector2 Extents = Vector2.one;

        /// <summary>
        /// 顶点Color
        /// </summary>
        [Header("顶点Color")]
        public Color MeshColor = Color.red;

        /// <summary>
        /// 2D AABB信息
        /// </summary>
        [Header("AABB2D信息")]
        public AABB2D AABB2D;

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
            AABB2D.Center = new Vector2(transform.position.x, transform.position.z);
            AABB2D.Extents = Extents;
        }

        void OnDrawGizmos()
        {
            if (mDrawMesh == null)
            {
                mMeshVerticesList = new List<Vector3>(4);
                for (int i = 0; i < 4; i++)
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
            var center = transform.position;
            var halfextents = Extents / 2;
            mMeshVerticesList[0] = new Vector3(center.x - halfextents.x, center.y, center.z - halfextents.y);
            mMeshVerticesList[1] = new Vector3(center.x - halfextents.x, center.y, center.z + halfextents.y);
            mMeshVerticesList[2] = new Vector3(center.x + halfextents.x, center.y, center.z + halfextents.y);
            mMeshVerticesList[3] = new Vector3(center.x + halfextents.x, center.y, center.z - halfextents.y);
            mDrawMesh.SetVertices(mMeshVerticesList);
            mDrawMesh.RecalculateNormals();
        }
    }
}