/*
 * Description:             Polygon2DDraw.cs
 * Author:                  TONYTANG
 * Create Date:             2022/03/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Module.Collision2D
{
    /// <summary>
    /// 2D多边形可视化绘制
    /// Polygon2DDraw.cs
    /// </summary>
    public class Polygon2DDraw : MonoBehaviour
    {
        /// <summary>
        /// 顶点数组
        /// </summary>
        [Header("顶点数组")]
        public Vector2[] Vertexes;

        /// <summary>
        /// 顶点Color
        /// </summary>
        [Header("顶点Color")]
        public Color MeshColor = Color.red;

        /// <summary>
        /// 2D多边形信息
        /// </summary>
        public Polygon2D Polygon2D;

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
            Polygon2D.Vertexes = Vertexes;
        }

        void OnDrawGizmos()
        {
            Polygon2D.Vertexes = Vertexes;
            var vertexesNumber = Polygon2D.Vertexes.Length;
            if (mDrawMesh == null || (mDrawMesh != null && mMeshVerticesList.Count != vertexesNumber))
            {
                mMeshVerticesList = new List<Vector3>(vertexesNumber);
                for (int i = 0; i < vertexesNumber; i++)
                {
                    mMeshVerticesList.Add(new Vector3(Polygon2D.Vertexes[i].x, 0, Polygon2D.Vertexes[i].y));
                }
                mDrawMesh = MeshUtilities.CreateMesh(mMeshVerticesList);
            }
            mDrawMesh.SetVertices(mMeshVerticesList);
            mDrawMesh.RecalculateNormals();
            Gizmos.color = MeshColor;
            Gizmos.DrawWireMesh(mDrawMesh);
        }
    }
}