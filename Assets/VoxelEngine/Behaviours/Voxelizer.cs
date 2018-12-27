using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using VoxelEngine.Resizers;
using VoxelEngine.Utilities;
using Material = UnityEngine.Material;

namespace VoxelEngine
{
    [ExecuteInEditMode]
    public class Voxelizer : MonoBehaviour
    {
        [Range(1,5)]
        public int levelOfDetail = 1;

        public ScriptableChunk scriptableChunk;
        public Chunk chunk => scriptableChunk != null ? scriptableChunk.chunk : null;
        public Vector3 meshOffset;

        public void Update()
        {
            if (chunk == null) return;
            
        }

        private void DrawChunkGizmo()
        {
            if (chunk == null) return;
            for(var x=0; x<chunk.Width; x++)
            for(var y=0; y<chunk.Height; y++)
            for (var z = 0; z < chunk.Depth; z++)
            {
                var bounds = chunk.GetVoxelBounds(x, y, z); // Already scaled to chunk scale
                Gizmos.color = chunk[x, y, z].material == 0 ? Color.black : Color.white;
                Gizmos.DrawWireCube(transform.TransformPoint(bounds.center) + meshOffset,bounds.size);
            }
        }

        public void OnDrawGizmos()
        {
            //    Get mesh
            var meshFilter = GetComponentInChildren<MeshFilter>();
            if (meshFilter == null) return;
            var mesh = meshFilter.sharedMesh;
            if (mesh == null) return;

            var lod = Mathf.FloorToInt(levelOfDetail);
            
            //    normalize the bounds
            var maxBounds = Mathf.Max(mesh.bounds.size.x, mesh.bounds.size.y, mesh.bounds.size.z);
            var bounds = new Bounds(mesh.bounds.center, new Vector3(maxBounds, maxBounds, maxBounds));

            //DrawOctree(bounds, 0, lod, meshFilter);
            
            DrawChunkGizmo();
        }

        private void DrawOctree(Bounds bounds, int level, int max, MeshFilter meshFilter)
        {
            
            if (level >= max) return;
            for(var childIndex=0; childIndex<8; childIndex++)
                DrawOctree(bounds.OctreeChild(childIndex), level+1, max, meshFilter);
            
            //    Test each triangle line to see if it intersects these bounds, if so, draw it
            Gizmos.color = Utilities.ColorUtility.GetDistinctColor(level);
            Gizmos.DrawWireCube(meshFilter.transform.TransformPoint(bounds.center), meshFilter.transform.TransformVector(bounds.size));
            
            
        }

        private static bool IsLineInBounds(Vector3 LB1, Vector3 LB2, Bounds bounds)
        {
            var m_Extent = (bounds.max - bounds.min) * 0.5f;
            var LMid = (LB1 + LB2) * 0.5f;
            var L = LB1 - LMid;
            var LExt = new Vector3(Mathf.Abs(L.x), Mathf.Abs(L.y), Mathf.Abs(L.z));
            // Use Separating Axis Test
            // Separation vector from box center to line center is LMid, since the line is in box space
            if ( Mathf.Abs( LMid.x ) > m_Extent.x + LExt.x ) return false;
            if ( Mathf.Abs( LMid.y ) > m_Extent.y + LExt.y ) return false;
            if ( Mathf.Abs( LMid.z ) > m_Extent.z + LExt.z ) return false;
            // Crossproducts of line and each axis
            if ( Mathf.Abs( LMid.y * L.z - LMid.z * L.y)  >  (m_Extent.y * LExt.z + m_Extent.z * LExt.y) ) return false;
            if ( Mathf.Abs( LMid.x * L.z - LMid.z * L.x)  >  (m_Extent.x * LExt.z + m_Extent.z * LExt.x) ) return false;
            if ( Mathf.Abs( LMid.x * L.y - LMid.y * L.x)  >  (m_Extent.x * LExt.y + m_Extent.y * LExt.x) ) return false;
            // No separating axis, the line intersects
            return true;
        }

        private static bool IsTriangleInBounds(Vector3 v1, Vector3 v2, Vector3 v3, Bounds bounds)
        {
            return IsLineInBounds(v1, v2, bounds) || IsLineInBounds(v2, v3, bounds) || IsLineInBounds(v3, v1, bounds);
        }
        
        private static bool IsTrianglesInBounds(Vector3[] vertices, int[] triangles, Bounds bounds)
        {
            for (var t = 0; t < triangles.Length; t += 3)
            {
                var v1 = vertices[triangles[t]];
                var v2 = vertices[triangles[t + 1]];
                var v3 = vertices[triangles[t + 2]];
                if (IsTriangleInBounds(v1, v2, v3, bounds)) return true;
            }
            Debug.Log($"Tested {triangles.Length} triangles against {bounds}");
            return false;
        }

        public void Voxelize()
        {
            //    Get mesh
            var meshFilter = GetComponentInChildren<MeshFilter>();
            if (meshFilter == null) return;
            var mesh = meshFilter.sharedMesh;
            if (mesh == null) return;
            if (scriptableChunk == null) return;
            var bounds = new Bounds(meshFilter.transform.TransformPoint(mesh.bounds.center), meshFilter.transform.TransformVector(mesh.bounds.size));
            var maxBounds = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
            var scale = new Vector3(maxBounds, maxBounds, maxBounds);
            bounds.size = scale;
            var vertices = TransformVertices(mesh.vertices, meshFilter.transform);
            vertices = OffsetVertices(mesh.vertices, bounds.center);
            var triangles = mesh.triangles;
            
            var lod = Mathf.FloorToInt(levelOfDetail);
            var size = 1 << lod - 1;
            scriptableChunk.chunk = new Chunk(size, size, size);
            scriptableChunk.chunk.scale = bounds.size;
            Debug.Log($"Chunk Width:{size}");
            Debug.Log($"Mesh Bounds:{bounds}");
            
            Voxelize(scriptableChunk.chunk, vertices, triangles);
            Debug.Log($"{scriptableChunk.chunk.VoxelCount()}/{scriptableChunk.chunk.voxels.Length}");
        }

        public static void Voxelize(Chunk chunk, Vector3[] vertices, int[] triangles)
        {
         
            //    Go through each Voxel
            for(var x=0; x<chunk.Width; x++)
            for (var y = 0; y < chunk.Height; y++)
            for (var z = 0; z < chunk.Depth; z++)
            {
                var chunkBounds = chunk.GetVoxelBounds(x, y, z);
                if (!IsTrianglesInBounds(vertices, triangles, chunkBounds)) continue;
                chunk[x, y, z] = (new Color32(255,255,255,255)).ToVoxel();
            }
        }
        
        public static Vector3[] OffsetVertices(Vector3[] vertices, Vector3 offset)
        {
            var v = new Vector3[vertices.Length];
            for (var i = 0; i < vertices.Length; i++)
                v[i] = vertices[i] + offset;
            return v;
        }
        
        public static Vector3[] TransformVertices(Vector3[] vertices, Transform transform)
        {
            var v = new Vector3[vertices.Length];
            for (var i = 0; i < vertices.Length; i++)
                v[i] = transform.TransformPoint(vertices[i]);
            return v;
        }

        public static Bounds BoundsOfVertices(Vector3[] vertices)
        {
            var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            var max = new Vector3(-float.MaxValue, -float.MaxValue, -float.MaxValue);
            foreach (var vertex in vertices)
            {
                min.x = Mathf.Min(vertex.x, min.x);
                min.y = Mathf.Min(vertex.x, min.x);
                min.z = Mathf.Min(vertex.x, min.x);
                max.x = Mathf.Max(vertex.x, min.x);
                max.y = Mathf.Max(vertex.x, min.x);
                max.z = Mathf.Max(vertex.x, min.x);
            }

            var bounds = new Bounds();
            bounds.SetMinMax(min, max);
            return bounds;
        }
    }
}