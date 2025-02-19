﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Battlehub.RTCommon
{
    public static class GraphicsUtility
    {
        public static float GetScreenScale(Vector3 position, Camera camera)
        {
            float h = camera.pixelHeight;
      
            if (camera.orthographic)
            {

                return camera.orthographicSize * 2f / h * 90;
            }

            Transform transform = camera.transform;
            float distance = camera.stereoEnabled ?
                (position - transform.position).magnitude :
                Vector3.Dot(position - transform.position, transform.forward);

            float scale = 2.0f * distance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            return scale / h * 90;
        }

        public static Mesh CreateCube(Color color, Vector3 center, float scale, float cubeLength = 1, float cubeWidth = 1, float cubeHeight = 1)
        {
            cubeHeight *= scale;
            cubeWidth *= scale;
            cubeLength *= scale;

            Vector3 vertice_0 = center + new Vector3(-cubeLength * .5f, -cubeWidth * .5f, cubeHeight * .5f);
            Vector3 vertice_1 = center + new Vector3(cubeLength * .5f, -cubeWidth * .5f, cubeHeight * .5f);
            Vector3 vertice_2 = center + new Vector3(cubeLength * .5f, -cubeWidth * .5f, -cubeHeight * .5f);
            Vector3 vertice_3 = center + new Vector3(-cubeLength * .5f, -cubeWidth * .5f, -cubeHeight * .5f);
            Vector3 vertice_4 = center + new Vector3(-cubeLength * .5f, cubeWidth * .5f, cubeHeight * .5f);
            Vector3 vertice_5 = center + new Vector3(cubeLength * .5f, cubeWidth * .5f, cubeHeight * .5f);
            Vector3 vertice_6 = center + new Vector3(cubeLength * .5f, cubeWidth * .5f, -cubeHeight * .5f);
            Vector3 vertice_7 = center + new Vector3(-cubeLength * .5f, cubeWidth * .5f, -cubeHeight * .5f);
            Vector3[] vertices = new[]
            {
                // Bottom Polygon
                vertice_0, vertice_1, vertice_2, vertice_3,
                // Left Polygon
                vertice_7, vertice_4, vertice_0, vertice_3,
                // Front Polygon
                vertice_4, vertice_5, vertice_1, vertice_0,
                // Back Polygon
                vertice_6, vertice_7, vertice_3, vertice_2,
                // Right Polygon
                vertice_5, vertice_6, vertice_2, vertice_1,
                // Top Polygon
                vertice_7, vertice_6, vertice_5, vertice_4
            };

            int[] triangles = new[]
            {
                // Cube Bottom Side Triangles
                3, 1, 0,
                3, 2, 1,    
                // Cube Left Side Triangles
                3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
                3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
                // Cube Front Side Triangles
                3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
                3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
                // Cube Back Side Triangles
                3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
                3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
                // Cube Rigth Side Triangles
                3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
                3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
                // Cube Top Side Triangles
                3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
                3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,
            };

            Color[] colors = new Color[vertices.Length];
            for (int i = 0; i < colors.Length; ++i)
            {
                colors[i] = color;
            }

            Mesh cubeMesh = new Mesh();
            cubeMesh.name = "cube";
            cubeMesh.vertices = vertices;
            cubeMesh.triangles = triangles;
            cubeMesh.colors = colors;
            cubeMesh.RecalculateNormals();
            return cubeMesh;
        }

        public static Mesh CreateQuad(float quadWidth = 1, float quadHeight = 1)
        {
            Vector3 vertice_0 = new Vector3(-quadWidth * .5f, -quadHeight * .5f, 0);
            Vector3 vertice_1 = new Vector3(quadWidth * .5f, -quadHeight * .5f, 0);
            Vector3 vertice_2 = new Vector3(-quadWidth * .5f, quadHeight * .5f, 0);
            Vector3 vertice_3 = new Vector3(quadWidth * .5f, quadHeight * .5f, 0);

            Vector3[] vertices = new[]
            {
                vertice_2, vertice_3, vertice_1, vertice_0,
            };

            int[] triangles = new[]
            {
                // Cube Bottom Side Triangles
                3, 1, 0,
                3, 2, 1,
            };

            Vector2[] uvs =
            {
                new Vector2(1, 0),
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 1),
            };

            Mesh quadMesh = new Mesh();
            quadMesh.name = "quad";
            quadMesh.vertices = vertices;
            quadMesh.triangles = triangles;
            quadMesh.uv = uvs;
            quadMesh.RecalculateNormals();
            return quadMesh;
        }

        public static Mesh CreateWireQuad(float width = 1, float height = 1)
        {
            Vector3 vertice_0 = new Vector3(-width * .5f, -height * .5f, 0);
            Vector3 vertice_1 = new Vector3(width * .5f, -height * .5f, 0);
            Vector3 vertice_2 = new Vector3(width * .5f, height * .5f, 0);
            Vector3 vertice_3 = new Vector3(-width * .5f, height * .5f, 0);
            
            Vector3[] vertices = new[]
            {
                vertice_0, vertice_1, vertice_2, vertice_3
            };

            Mesh quadMesh = new Mesh();
            quadMesh.vertices = vertices;
            quadMesh.SetIndices(new[] { 0, 1, 1, 2, 2, 3, 3, 0 }, MeshTopology.Lines, 0);
            
            return quadMesh;
        }

        public static Mesh CreateWireCubeMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = new[]
            {
                new Vector3(-1, -1, -1),
                new Vector3(-1, -1,  1),
                new Vector3(-1,  1, -1),
                new Vector3(-1,  1,  1),
                new Vector3( 1, -1, -1),
                new Vector3( 1, -1,  1),
                new Vector3( 1,  1, -1),
                new Vector3( 1,  1,  1),
            };
            mesh.SetIndices(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 0, 2, 1, 3, 4, 6, 5, 7, 0, 4, 1, 5, 2, 6, 3, 7 }, MeshTopology.Lines, 0);
            return mesh;
        }

        public static Mesh CreateCircle(float radius = 1, int pointsCount = 64)
        {
            return CreateArc(Vector3.zero, radius, pointsCount, 0, Mathf.PI * 2);
        }

        public static Mesh CreateArc(Vector3 offset, float radius = 1, int pointsCount = 64, float fromAngle = 0, float toAngle = Mathf.PI * 2)
        {
            Vector3[] vertices = new Vector3[pointsCount + 2];
            Vector2[] uv = new Vector2[pointsCount + 2];

            List<int> indices = new List<int>();
            for (int i = 1; i <= pointsCount; ++i)
            {
                indices.Add(0);
                indices.Add(i);
                indices.Add(i + 1);
            }

            float currentAngle = fromAngle;
            float deltaAngle = toAngle - fromAngle;
            float z = 0.0f;
            float x = Mathf.Cos(currentAngle);
            float y = Mathf.Sin(currentAngle);
            vertices[0] = offset;
            uv[0] = Vector2.one * 0.5f;

            Vector3 prevPoint = new Vector3(x * radius, y * radius, z) + offset;
            Vector2 prevUv = (Vector2.one + new Vector2(x, y)) * 0.5f;
            prevUv.y = 1 - prevUv.y;

            for (int i = 1; i <= pointsCount; i++)
            {
                vertices[i] = prevPoint;
                uv[i] = prevUv;

                currentAngle += deltaAngle / pointsCount;
                x = Mathf.Cos(currentAngle);
                y = Mathf.Sin(currentAngle);

                prevPoint = new Vector3(x * radius, y * radius, z) + offset;
                prevUv = (Vector2.one + new Vector2(x, y)) * 0.5f;
                prevUv.y = 1 - prevUv.y;
            }

            vertices[pointsCount + 1] = prevPoint;
            uv[pointsCount + 1] = prevUv;

            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);
            mesh.uv = uv;
            return mesh;
        }

        public static Mesh CreateWireCircle(float radius = 1, int pointsCount = 64)
        {
            return CreateWireArc(Vector3.zero, radius, pointsCount, 0, Mathf.PI * 2);
        }

        public static Mesh CreateWireArc(Vector3 offset, float radius = 1, int pointsCount = 64, float fromAngle = 0, float toAngle = Mathf.PI * 2)
        {   
            Vector3[] vertices = new Vector3[pointsCount + 1];

            List<int> indices = new List<int>();
            for(int i = 0; i < pointsCount; ++i)
            {
                indices.Add(i);
                indices.Add(i + 1);
            }

            float currentAngle = fromAngle;
            float deltaAngle = toAngle - fromAngle;
            float z = 0.0f;
            float x = Mathf.Cos(currentAngle) * radius;
            float y = Mathf.Sin(currentAngle) * radius;

            Vector3 prevPoint = new Vector3(x, y, z) + offset;
            for (int i = 0; i < pointsCount; i++)
            {
                vertices[i] = prevPoint;
                currentAngle += deltaAngle / pointsCount;
                x = Mathf.Cos(currentAngle) * radius;
                y = Mathf.Sin(currentAngle) * radius;
                Vector3 point = new Vector3(x, y, z) + offset;
                vertices[i + 1] = point;
                prevPoint = point;
            }

            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.SetIndices(indices.ToArray(), MeshTopology.Lines, 0);
            return mesh;
        }

        public static Mesh CreateWireCylinder(float radius = 1.0f, float length = 1.0f, int pointsCount = 8, float fromAngle = 0, float toAngle = Mathf.PI * 2)
        {
            Vector3[] vertices = new Vector3[pointsCount * 2];
            List<int> indices = new List<int>();
            for (int i = 0; i < vertices.Length; i += 2)
            {
                indices.Add(i);
                indices.Add(i + 1);
            }

            float currentAngle = fromAngle;
            float deltaAngle = toAngle - fromAngle;
            float z = 0.0f;

            for (int i = 0; i < vertices.Length; i += 2)
            {
                float x = radius * Mathf.Cos(currentAngle);
                float y = radius * Mathf.Sin(currentAngle);
                Vector3 point = new Vector3(x, y, z);
                Vector3 point2 = new Vector3(x, y, z) + Vector3.forward * length;
                vertices[i] = point;
                vertices[i + 1] = point2;
                currentAngle += deltaAngle / pointsCount;
            }

            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.SetIndices(indices.ToArray(), MeshTopology.Lines, 0);
            return mesh;
        }

        public static void DrawMesh(IRTECommandBuffer commandBuffer, Mesh mesh, Matrix4x4 transform, Material material, MaterialPropertyBlock propertyBlock)
        {
            commandBuffer.DrawMesh(mesh, transform, material, 0, 0, propertyBlock);
        }
      
        public static Mesh CreateCone(Color color, float scale)
        {
            int segmentsCount = 12;
            float size = 1.0f / 5;
            size *= scale;

            Vector3[] vertices = new Vector3[segmentsCount * 3 + 1];
            int[] triangles = new int[segmentsCount * 6];
            Color[] colors = new Color[vertices.Length];
            for (int i = 0; i < colors.Length; ++i)
            {
                colors[i] = color;
            }

            float radius = size / 2.6f;
            float height = size;
            float deltaAngle = Mathf.PI * 2.0f / segmentsCount;

            float y = -height;

            vertices[vertices.Length - 1] = new Vector3(0, -height, 0);
            for (int i = 0; i < segmentsCount; i++)
            {
                float angle = i * deltaAngle;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;

                vertices[i] = new Vector3(x, y, z);
                vertices[segmentsCount + i] = new Vector3(0, 0.01f, 0);
                vertices[2 * segmentsCount + i] = vertices[i];
            }

            for (int i = 0; i < segmentsCount; i++)
            {
                triangles[i * 6] = i;
                triangles[i * 6 + 1] = segmentsCount + i;
                triangles[i * 6 + 2] = (i + 1) % segmentsCount;

                triangles[i * 6 + 3] = vertices.Length - 1;
                triangles[i * 6 + 4] = 2 * segmentsCount + i;
                triangles[i * 6 + 5] = 2 * segmentsCount + (i + 1) % segmentsCount;
            }

            Mesh cone = new Mesh();
            cone.name = "Cone";
            cone.vertices = vertices;
            cone.triangles = triangles;
            cone.colors = colors;

            return cone;
        }

        private static readonly List<Vector2> s_vector2List = new List<Vector2>();
        private static readonly List<Vector3> s_vector3List = new List<Vector3>();
        private static readonly List<Color> s_colorList = new List<Color>();
        private static readonly List<int> s_indexList = new List<int>();
        private static readonly Vector2 k_Billboard0 = new Vector2(-1f, -1f);
        private static readonly Vector2 k_Billboard1 = new Vector2(-1f, 1f);
        private static readonly Vector2 k_Billboard2 = new Vector2(1f, -1f);
        private static readonly Vector2 k_Billboard3 = new Vector2(1f, 1f);

        public static void CreatePointBillboardMesh(IList<Vector3> positions, IList<int> indexes, IList<Color> colors, Mesh target)
        {
            var pointCount = indexes.Count;
            var vertexCount = pointCount * 4;

            s_vector2List.Clear();
            s_vector3List.Clear();
            s_colorList.Clear();
            s_indexList.Clear();
            s_vector2List.Capacity = vertexCount;
            s_vector3List.Capacity = vertexCount;
            s_colorList.Capacity = vertexCount;
            s_indexList.Capacity = vertexCount;

            for (int i = 0; i < pointCount; i++)
            {
                var index = indexes[i];

                s_vector3List.Add(positions[index]);
                s_vector3List.Add(positions[index]);
                s_vector3List.Add(positions[index]);
                s_vector3List.Add(positions[index]);

                s_vector2List.Add(k_Billboard0);
                s_vector2List.Add(k_Billboard1);
                s_vector2List.Add(k_Billboard2);
                s_vector2List.Add(k_Billboard3);

                if(colors != null)
                {
                    s_colorList.Add(colors[index]);
                    s_colorList.Add(colors[index]);
                    s_colorList.Add(colors[index]);
                    s_colorList.Add(colors[index]);
                }

                s_indexList.Add(i * 4 + 0);
                s_indexList.Add(i * 4 + 1);
                s_indexList.Add(i * 4 + 3);
                s_indexList.Add(i * 4 + 2);
            }

            target.Clear();
            target.indexFormat = vertexCount > ushort.MaxValue ? IndexFormat.UInt32 : IndexFormat.UInt16;
            target.SetVertices(s_vector3List);
            target.SetUVs(0, s_vector2List);
            if(colors != null)
            {
                target.SetColors(s_colorList);
            }
       
            target.subMeshCount = 1;
#if UNITY_2019_3_OR_NEWER
            target.SetIndices(s_indexList, MeshTopology.Quads, 0);
#else
            target.SetIndices(s_IndexList.ToArray(), MeshTopology.Quads, 0);
#endif
        }

        public static void UpdatePointBillboardMeshVertices(IList<Vector3> positions, Mesh target)
        {
            s_vector3List.Clear();
            s_vector3List.Capacity = positions.Count * 4;
         
            for (int i = 0; i < positions.Count; i++)
            {
                s_vector3List.Add(positions[i]);
                s_vector3List.Add(positions[i]);
                s_vector3List.Add(positions[i]);
                s_vector3List.Add(positions[i]);         
            }

            target.SetVertices(s_vector3List);
        }

        #region Legacy
        private static readonly RTECommandBuffer s_commandBuffer = new RTECommandBuffer(null);
        public static void DrawMesh(CommandBuffer commandBuffer, Mesh mesh, Matrix4x4 transform, Material material, MaterialPropertyBlock propertyBlock)
        {
            s_commandBuffer.WrappedCommandBuffer = commandBuffer;
            DrawMesh(s_commandBuffer, mesh, transform, material, propertyBlock);
        }
        #endregion

    }
}
