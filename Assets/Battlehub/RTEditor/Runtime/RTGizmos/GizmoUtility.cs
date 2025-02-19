﻿using Battlehub.RTCommon;
using UnityEngine;
using UnityEngine.Rendering;

namespace Battlehub.RTGizmos
{
    public class GizmoUtility 
    {
        private static Material LinesMaterial;
        private static Material HandlesMaterial;
        private static Material SelectionMaterial;
        private static Mesh RectHandles;
        private static Mesh CubeHandles;
        private static Mesh ConeHandles;
        private static Mesh Selection;
        private static Mesh WireCircle;
        private static Mesh WireCube;
        private static Mesh WireCone;
        private static Mesh WireCylinder;
        private static Mesh WireArc1;
        private static Mesh WireArc2;
        private static Mesh WireCapsule;

        private static float s_handleScale = 1.0f;
        public static float HandleScale
        {
            get { return s_handleScale; }
            set 
            {
                if(s_handleScale != value)
                {
                    s_handleScale = value;

                    UnityEngine.Object.Destroy(CubeHandles);
                    UnityEngine.Object.Destroy(RectHandles);
                    UnityEngine.Object.Destroy(ConeHandles);
                    UnityEngine.Object.Destroy(Selection);

                    CubeHandles = CreateCubeHandles(HandleScale * 3);
                    RectHandles = CreateRectHandles(HandleScale * 3);
                    ConeHandles = CreateConeHandles(HandleScale * 3);
                    Selection = CreateHandlesMesh(HandleScale * 3, new[] { Vector3.zero }, new[] { Vector3.back });
                }
                
            }
        }

        private static float s_lineScale = 1.0f;
        public static float LineScale
        {
            get { return s_lineScale; }
            set 
            {
                if(s_lineScale != value)
                {
                    s_lineScale = value;
                    if (LinesMaterial != null)
                    {
                        LinesMaterial.SetFloat("_Scale", LineScale);
                    }
                }   
            }
        }

        static GizmoUtility()
        {
            Initialize();
        }

        public static void Initialize()
        {
            if (HandlesMaterial != null)
            {
                return;
            }

            Cleanup();

            HandlesMaterial = new Material(Shader.Find("Battlehub/RTGizmos/Handles"));
            HandlesMaterial.enableInstancing = true;
            LinesMaterial = new Material(Shader.Find("Battlehub/RTCommon/LineBillboard"));
            LinesMaterial.SetFloat("_Scale", LineScale);
            LinesMaterial.enableInstancing = true;
            SelectionMaterial = new Material(Shader.Find("Battlehub/RTGizmos/Handles"));
            SelectionMaterial.SetFloat("_Offset", 1);
            SelectionMaterial.SetFloat("_MinAlpha", 1);
            SelectionMaterial.enableInstancing = true;

            CubeHandles = CreateCubeHandles(HandleScale * 3);
            RectHandles = CreateRectHandles(HandleScale * 3);
            ConeHandles = CreateConeHandles(HandleScale * 3);
            Selection = CreateHandlesMesh(HandleScale * 3, new[] { Vector3.zero }, new[] { Vector3.back });

            WireCircle = GraphicsUtility.CreateWireCircle();
            WireArc1 = GraphicsUtility.CreateWireArc(Vector3.zero, 1, 32, 0, Mathf.PI);
            WireArc2 = GraphicsUtility.CreateWireArc(Vector3.zero, 1, 32, Mathf.PI, Mathf.PI * 2);
            WireCube = GraphicsUtility.CreateWireCubeMesh();
            WireCylinder = GraphicsUtility.CreateWireCylinder();
            WireCone = CreateWireConeMesh();
            WireCapsule = CreateWireCapsuleMesh();
        }

        public static void Cleanup()
        {
            s_handleScale = 1.0f;
            s_lineScale = 1.0f;

            if (HandlesMaterial != null)
            {
                Object.Destroy(HandlesMaterial);
                HandlesMaterial = null;
            }

            if (LinesMaterial != null)
            {
                Object.Destroy(LinesMaterial);
                LinesMaterial = null;
            }
            
            if (SelectionMaterial != null)
            {
                Object.Destroy(SelectionMaterial);
                SelectionMaterial = null;
            }

            if (CubeHandles != null)
            {
                Object.Destroy(CubeHandles);
                CubeHandles = null;
            }

            if (RectHandles != null)
            {
                Object.Destroy(RectHandles);
                RectHandles = null;
            }

            if (ConeHandles != null)
            {
                Object.Destroy(ConeHandles);
                ConeHandles = null;
            }
            
            if (Selection != null)
            {
                Object.Destroy(Selection);
                Selection = null;
            }
            
            if (WireCircle != null)
            {
                Object.Destroy(WireCircle);
                WireCircle = null;
            }
            
            if (WireArc1 != null)
            {
                Object.Destroy(WireArc1);
                WireArc1 = null;
            }
            
            if (WireArc2 != null)
            {
                Object.Destroy(WireArc2);
                WireArc2 = null;
            }

            if (WireCube != null)
            {
                Object.Destroy(WireCube);
                WireCube = null;
            }

            if (WireCylinder != null)
            {
                Object.Destroy(WireCylinder);
                WireCylinder = null;
            }

            if (WireCone != null)
            {
                Object.Destroy(WireCone);
                WireCone = null;
            }

            if (WireCapsule != null)
            {
                Object.Destroy(WireCapsule);
                WireCapsule = null;
            }
        }

        private static Mesh CreateWireConeMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = new[]
            {
                Vector3.zero,
                Vector3.forward + new Vector3(1, 1, 0).normalized,
                Vector3.zero,
                Vector3.forward + new Vector3(-1, 1, 0).normalized,
                Vector3.zero,
                Vector3.forward + new Vector3(-1, -1, 0).normalized,
                Vector3.zero,
                Vector3.forward + new Vector3(1, -1, 0).normalized
            };
            mesh.SetIndices(new[] { 0, 1, 2, 3, 4, 5, 6, 7 }, MeshTopology.Lines, 0);
            return mesh;
        }

        private static Mesh CreateWireCapsuleMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = new[]
            {
                new Vector3(1, 1, 0),
                new Vector3(1, -1, 0),
                new Vector3(-1, 1, 0),
                new Vector3(-1, -1, 0)
            };
            mesh.SetIndices(new[] { 0, 1, 2, 3 }, MeshTopology.Lines, 0);
            return mesh;
        }

        public static Vector3[] GetRectHandlesPositions()
        {
            Vector3[] vertices = new[] { Vector3.up, Vector3.down, Vector3.right, Vector3.left };
            return vertices;
        }

        public static Vector3[] GetRectHandlesNormals()
        {
            Vector3[] vertices = new[] { Vector3.up, Vector3.down, Vector3.right, Vector3.left  };
            return vertices;
        }


        public static Vector3[] GetHandlesPositions()
        {
            Vector3[] vertices = new[] { Vector3.up, Vector3.down, Vector3.right, Vector3.left, Vector3.forward, Vector3.back };
            return vertices;
        }

        public static Vector3[] GetHandlesNormals()
        {
            Vector3[] vertices = new[] { Vector3.up, Vector3.down, Vector3.right, Vector3.left, Vector3.forward, Vector3.back };
            return vertices;
        }

        public static Vector3[] GetConeHandlesPositions()
        {
            return new[]
            {
                Vector3.zero,
                new Vector3(1, 1, 0).normalized,
                new Vector3(-1, 1, 0).normalized,
                new Vector3(-1, -1, 0).normalized,
                new Vector3(1, -1, 0).normalized
            };
        }

        public static Vector3[] GetConeHandlesNormals()
        {
            return new[]
            {
                Vector3.forward,
                new Vector3(1, 1, 0).normalized,
                new Vector3(-1, 1, 0).normalized,
                new Vector3(-1, -1, 0).normalized,
                new Vector3(1, -1, 0).normalized
            };
        }

        private static Mesh CreateConeHandles(float size)
        {
            Vector3[] vertices = GetConeHandlesPositions();
            Vector3[] normals = GetConeHandlesNormals();

            return CreateHandlesMesh(size, vertices, normals);
        }

        private static Mesh CreateRectHandles (float size)
        {
            Vector3[] vertices = GetRectHandlesPositions();
            Vector3[] normals = GetRectHandlesNormals();

            return CreateHandlesMesh(size, vertices, normals);
        }

        private static Mesh CreateCubeHandles(float size)
        {
            Vector3[] vertices = GetHandlesPositions();
            Vector3[] normals = GetHandlesNormals();

            return CreateHandlesMesh(size, vertices, normals);
        }

        public static Mesh CreateHandlesMesh(float size, Vector3[] vertices, Vector3[] normals)
        {
            Vector2[] offsets = new Vector2[vertices.Length * 4];
            Vector3[] resultVertices = new Vector3[vertices.Length * 4];
            Vector3[] resultNormals = new Vector3[normals.Length * 4];

            for (int i = 0; i < vertices.Length; ++i)
            {
                Vector3 vert = vertices[i];
                Vector3 norm = normals[i];
                SetVertex(i, resultVertices, vert);
                SetVertex(i, resultNormals, norm);
                SetOffset(i, offsets, size);
            }

            int[] triangles = new int[resultVertices.Length + resultVertices.Length / 2];
            int index = 0;
            for (int i = 0; i < triangles.Length; i += 6)
            {
                triangles[i] = index;
                triangles[i + 1] = index + 1;
                triangles[i + 2] = index + 2;

                triangles[i + 3] = index;
                triangles[i + 4] = index + 2;
                triangles[i + 5] = index + 3;

                index += 4;
            }

            Mesh result = new Mesh();
            result.vertices = resultVertices;
            result.triangles = triangles;
            result.normals = resultNormals;
            result.uv = offsets;
            return result;
        }

        private static void SetVertex(int index, Vector3[] vertices, Vector3 vert)
        {
            for (int i = 0; i < 4; ++i)
            {
                vertices[index * 4 + i] = vert;
            }
        }

        private static void SetOffset(int index, Vector2[] offsets, float size)
        {
            float halfSize = size / 2;
            offsets[index * 4] = new Vector2(-halfSize, -halfSize);
            offsets[index * 4 + 1] = new Vector2(-halfSize, halfSize);
            offsets[index * 4 + 2] = new Vector2(halfSize, halfSize);
            offsets[index * 4 + 3] = new Vector2(halfSize, -halfSize);
        }

        public static void DrawSelection(IRTECommandBuffer commandBuffer, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            Matrix4x4 transform = Matrix4x4.TRS(position, rotation, scale);
            commandBuffer.DrawMesh(Selection, transform, SelectionMaterial, 0, 0, properties);
        }

        public static void DrawCubeHandles(IRTECommandBuffer commandBuffer, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            Matrix4x4 transform = Matrix4x4.TRS(position, rotation, scale);
            commandBuffer.DrawMesh(CubeHandles, transform, HandlesMaterial, 0, 0, properties);
        }

        public static void DrawRectHandles(IRTECommandBuffer commandBuffer, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            Matrix4x4 transform = Matrix4x4.TRS(position, rotation, scale);
            commandBuffer.DrawMesh(RectHandles, transform, HandlesMaterial, 0, 0, properties);
        }

        public static void DrawConeHandles(IRTECommandBuffer commandBuffer, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            Matrix4x4 transform = Matrix4x4.TRS(position, rotation, scale);
            commandBuffer.DrawMesh(ConeHandles, transform, HandlesMaterial, 0, 0, properties);
        }

        public static void DrawWireCube(IRTECommandBuffer commandBuffer, Bounds bounds, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            Matrix4x4 transform = Matrix4x4.TRS(position, rotation, Vector3.Scale(scale, bounds.extents));
            commandBuffer.DrawMesh(WireCube, transform, LinesMaterial, 0, 0, properties);
        }

        public static void DrawWireCircle(IRTECommandBuffer commandBuffer, Camera camera, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            Matrix4x4 zTranform = Matrix4x4.TRS(Vector3.zero, rotation * Quaternion.identity, Vector3.one);
            Matrix4x4 objToWorld = Matrix4x4.TRS(position, Quaternion.identity, Vector3.one * Mathf.Max(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z)));
            GraphicsUtility.DrawMesh(commandBuffer, WireCircle, objToWorld * zTranform, LinesMaterial, properties);
        }

        public static void DrawWireSphere(IRTECommandBuffer commandBuffer, Camera camera, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            Matrix4x4 xTranform = Matrix4x4.TRS(Vector3.zero, rotation * Quaternion.AngleAxis(-90, Vector3.up), Vector3.one);
            Matrix4x4 yTranform = Matrix4x4.TRS(Vector3.zero, rotation * Quaternion.AngleAxis(-90, Vector3.right), Vector3.one);
            Matrix4x4 zTranform = Matrix4x4.TRS(Vector3.zero, rotation * Quaternion.identity, Vector3.one);
            Matrix4x4 objToWorld = Matrix4x4.TRS(position, Quaternion.identity, Vector3.one * Mathf.Max(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z)));

            GraphicsUtility.DrawMesh(commandBuffer, WireCircle, objToWorld * xTranform, LinesMaterial, properties);
            GraphicsUtility.DrawMesh(commandBuffer, WireCircle, objToWorld * yTranform, LinesMaterial, properties);
            GraphicsUtility.DrawMesh(commandBuffer, WireCircle, objToWorld * zTranform, LinesMaterial, properties);
            if (camera.orthographic)
            {
                Matrix4x4 outTransform = Matrix4x4.TRS(Vector3.zero, camera.transform.rotation, Vector3.one);
                GraphicsUtility.DrawMesh(commandBuffer, WireCircle, objToWorld * outTransform, LinesMaterial, properties);
            }
            else
            {
                Vector3 toCam = camera.transform.position - position;
                Vector3 toCamNorm = toCam.normalized;
                if (Vector3.Dot(toCamNorm, camera.transform.forward) < 0)
                {
                    float m = toCam.magnitude;
                    Matrix4x4 outTransform = Matrix4x4.TRS(toCamNorm * 0.56f * scale.x / m, Quaternion.LookRotation(toCamNorm, camera.transform.up), Vector3.one);
                    GraphicsUtility.DrawMesh(commandBuffer, WireCircle, objToWorld * outTransform, LinesMaterial, properties);
                }
            }
        }

        public static void DrawWireCone(IRTECommandBuffer commandBuffer, float height, float radius, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            Matrix4x4 circleTransform = Matrix4x4.TRS(height * Vector3.forward, Quaternion.identity, Vector3.one * radius);
            Matrix4x4 coneTransform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(radius, radius, height));
            Matrix4x4 objToWorld = Matrix4x4.TRS(position, rotation, scale);

            GraphicsUtility.DrawMesh(commandBuffer, WireCircle, objToWorld * circleTransform, LinesMaterial, properties);
            GraphicsUtility.DrawMesh(commandBuffer, WireCone, objToWorld * coneTransform, LinesMaterial, properties);
        }

        public static void DrawCapsule2DGL(IRTECommandBuffer commandBuffer, Matrix4x4 transform, float radius, float height, MaterialPropertyBlock properties)
        {
            GraphicsUtility.DrawMesh(commandBuffer, WireArc1, transform * Matrix4x4.TRS(Vector3.up * height / 2, Quaternion.identity, Vector3.one * radius), LinesMaterial, properties);
            GraphicsUtility.DrawMesh(commandBuffer, WireArc2, transform * Matrix4x4.TRS(Vector3.down * height / 2, Quaternion.identity, Vector3.one * radius), LinesMaterial, properties);
            GraphicsUtility.DrawMesh(commandBuffer, WireCapsule, transform * Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(radius, height / 2, 0)), LinesMaterial, properties);
        }

        public static void DrawWireCapsule(IRTECommandBuffer commandBuffer, int axis, float height, float radius, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            Matrix4x4 topCircleTransform;
            Matrix4x4 bottomCircleTransform;
            Matrix4x4 capsule2DTransform;
            Matrix4x4 capsule2DTransform2;

            radius = Mathf.Abs(radius);

            if (Mathf.Abs(height) < 2 * radius)
            {
                height = 0;
            }
            else
            {
                height = Mathf.Abs(height) - 2 * radius;
            }

            if (axis == 1)
            {
                topCircleTransform = Matrix4x4.TRS(Vector3.up * height / 2, Quaternion.AngleAxis(-90, Vector3.right), Vector3.one * radius);
                bottomCircleTransform = Matrix4x4.TRS(Vector3.down * height / 2, Quaternion.AngleAxis(-90, Vector3.right), Vector3.one * radius);
                capsule2DTransform = Matrix4x4.identity;
                capsule2DTransform2 = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-90, Vector3.up), Vector3.one);
            }
            else if (axis == 0)
            {
                topCircleTransform = Matrix4x4.TRS(Vector3.right * height / 2, Quaternion.AngleAxis(-90, Vector3.up), Vector3.one * radius);
                bottomCircleTransform = Matrix4x4.TRS(Vector3.left * height / 2, Quaternion.AngleAxis(-90, Vector3.up), Vector3.one * radius);
                capsule2DTransform = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-90, Vector3.forward), Vector3.one);
                capsule2DTransform2 = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-90, Vector3.forward) * Quaternion.AngleAxis(-90, Vector3.up), Vector3.one);
            }
            else
            {
                topCircleTransform = Matrix4x4.TRS(Vector3.forward * height / 2, Quaternion.identity, Vector3.one * radius);
                bottomCircleTransform = Matrix4x4.TRS(Vector3.back * height / 2, Quaternion.identity, Vector3.one * radius);
                capsule2DTransform = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-90, Vector3.right), Vector3.one);
                capsule2DTransform2 = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(-90, Vector3.right) * Quaternion.AngleAxis(-90, Vector3.up), Vector3.one);
            }

            Matrix4x4 objToWorld = Matrix4x4.TRS(position, rotation, scale);

            GraphicsUtility.DrawMesh(commandBuffer, WireCircle, objToWorld * topCircleTransform, LinesMaterial, properties);
            GraphicsUtility.DrawMesh(commandBuffer, WireCircle, objToWorld * bottomCircleTransform, LinesMaterial, properties);

            DrawCapsule2DGL(commandBuffer, objToWorld * capsule2DTransform, radius, height, properties);
            DrawCapsule2DGL(commandBuffer, objToWorld * capsule2DTransform2, radius, height, properties);
        }

        public static void DrawDirectionalLight(IRTECommandBuffer commandBuffer, Camera camera, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            float sScale = GraphicsUtility.GetScreenScale(position, camera);

            float radius = 0.25f;
            float length = 1.25f;

            Matrix4x4 circleTransform = Matrix4x4.TRS(Vector3.zero, rotation, Vector3.one * radius);
            Matrix4x4 cylinderTransform = Matrix4x4.TRS(Vector3.zero, rotation, new Vector3(radius, radius, length));
            Matrix4x4 objToWorld = Matrix4x4.TRS(position, Quaternion.identity, scale * sScale);

            GraphicsUtility.DrawMesh(commandBuffer, WireCircle, objToWorld * circleTransform, LinesMaterial, properties);
            GraphicsUtility.DrawMesh(commandBuffer, WireCylinder, objToWorld * cylinderTransform, LinesMaterial, properties);
        }


        #region Legacy
        private static RTECommandBuffer s_commandBufferWrapper = new RTECommandBuffer(null);
        public static void DrawSelection(CommandBuffer commandBuffer, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            s_commandBufferWrapper.WrappedCommandBuffer = commandBuffer;
            DrawSelection(s_commandBufferWrapper, position, rotation, scale, properties);
        }

        public static void DrawCubeHandles(CommandBuffer commandBuffer, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            s_commandBufferWrapper.WrappedCommandBuffer = commandBuffer;
            DrawCubeHandles(s_commandBufferWrapper, position, rotation, scale, properties);
        }

        public static void DrawConeHandles(CommandBuffer commandBuffer, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            s_commandBufferWrapper.WrappedCommandBuffer = commandBuffer;
            DrawConeHandles(s_commandBufferWrapper, position, rotation, scale, properties);
        }

        public static void DrawWireCube(CommandBuffer commandBuffer, Bounds bounds, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            s_commandBufferWrapper.WrappedCommandBuffer = commandBuffer;
            DrawWireCube(s_commandBufferWrapper, bounds, position, rotation, scale, properties);
        }

        public static void DrawWireSphere(CommandBuffer commandBuffer, Camera camera, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            s_commandBufferWrapper.WrappedCommandBuffer = commandBuffer;
            DrawWireSphere(s_commandBufferWrapper, camera, position, rotation, scale, properties);
        }

        public static void DrawWireCone(CommandBuffer commandBuffer, float height, float radius, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            s_commandBufferWrapper.WrappedCommandBuffer = commandBuffer;
            DrawWireCone(s_commandBufferWrapper, height, radius, position, rotation, scale, properties);
        }

        public static void DrawCapsule2DGL(CommandBuffer commandBuffer, Matrix4x4 transform, float radius, float height, MaterialPropertyBlock properties)
        {
            s_commandBufferWrapper.WrappedCommandBuffer = commandBuffer;
            DrawCapsule2DGL(s_commandBufferWrapper, transform, radius, height, properties);
        }

        public static void DrawWireCapsule(CommandBuffer commandBuffer, int axis, float height, float radius, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            s_commandBufferWrapper.WrappedCommandBuffer = commandBuffer;
            DrawWireCapsule(s_commandBufferWrapper, axis, height, radius, position, rotation, scale, properties);
        }

        public static void DrawDirectionalLight(CommandBuffer commandBuffer, Camera camera, Vector3 position, Quaternion rotation, Vector3 scale, MaterialPropertyBlock properties)
        {
            s_commandBufferWrapper.WrappedCommandBuffer = commandBuffer;
            DrawDirectionalLight(s_commandBufferWrapper, camera, position, rotation, scale, properties);
        }

        #endregion


    }
}
