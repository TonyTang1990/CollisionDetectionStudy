/*
 * Description:             OBB2DDraw.cs
 * Author:                  #AUTHOR#
 * Create Date:             #CREATEDATE#
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Module.Collision2D
{
    /// <summary>
    /// OBB可视化绘制
    /// OBB2DDraw.cs
    /// </summary>
    public class OBB2DDraw : MonoBehaviour
    {
        /// <summary>
        /// 宽和高
        /// </summary>
        [Header("宽和高")]
        public Vector2 Extents = Vector2.one;

        /// <summary>
        /// 旋转角度
        /// </summary>
        [Header("旋转角度")]
        public float Angle = 0f;

        /// <summary>
        /// 顶点Color
        /// </summary>
        [Header("顶点Color")]
        public Color MeshColor = Color.red;

        /// <summary>
        /// 2D OBB信息
        /// </summary>
        public OBB2D OBB2D;

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
            OBB2D.Center = new Vector2(transform.position.x, transform.position.z);
            OBB2D.Extents = Extents;
            OBB2D.Angle = Angle;
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
            mMeshVerticesList[0] = Quaternion.AngleAxis(OBB2D.Angle, Vector3.up) * new Vector3(-halfextents.x, 0, -halfextents.y) + center;
            mMeshVerticesList[1] = Quaternion.AngleAxis(OBB2D.Angle, Vector3.up) * new Vector3(-halfextents.x, 0, halfextents.y) + center;
            mMeshVerticesList[2] = Quaternion.AngleAxis(OBB2D.Angle, Vector3.up) * new Vector3(halfextents.x, 0, halfextents.y) + center;
            mMeshVerticesList[3] = Quaternion.AngleAxis(OBB2D.Angle, Vector3.up) * new Vector3(halfextents.x, 0, -halfextents.y) + center;
            mDrawMesh.SetVertices(mMeshVerticesList);
            mDrawMesh.RecalculateNormals();
        }
    }
}