using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using VoxelEngine.Loaders;
using VoxmapUtility;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace VoxelEngine
{
    /// <summary>
    /// A 3D object containing a single Voxmap worth of data
    /// </summary>
    public class Voxelchunk : MonoBehaviour
    {
        public ScriptableChunk chunk;
        public bool optimize;

        public void Start()
        {
            StartCoroutine(Build());

        }

        public void OnDrawGizmos()
        {
            if (chunk == null) return;
            chunk.chunk.DrawBoundsGizmo(transform, Color.blue);
        }
        
        private IEnumerator Build()
        {
            var chunk = this.chunk.chunk;
            UnityEngine.Debug.Log($"Building chunk size {chunk.Width}");
            if (chunk.Width > 60) throw new Exception($"{chunk.Width} is larger than the allowd 60");
            
            var chunkMesh = new ChunkMesh();
            var renderer = new BlockRenderer(chunkMesh);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            renderer.Render(chunk);
            //if (optimize) chunkMesh.Optimize();
            if (optimize)
            {
                var task = Task.Run(() => chunkMesh.Optimize());
                while (!task.IsCompleted) yield return new WaitForEndOfFrame();
            }
            //Task<bool> task = DoStuff();
            //while (!task.IsCompleted) yield return new WaitForEndOfFrame();
            
            stopwatch.Stop();
            UnityEngine.Debug.Log($"Build Time:{stopwatch.ElapsedMilliseconds} ms");
            var meshFilter = GetComponent<MeshFilter>();
            if (meshFilter == null) yield return null;
            var mesh = GetComponent<MeshFilter>().mesh;
            mesh.Clear();
            mesh.vertices = chunkMesh.vertices.ToArray();
            mesh.triangles = chunkMesh.triangles.ToArray();
            mesh.colors32 = chunkMesh.colors.ToArray();
            mesh.normals = chunkMesh.normals.ToArray();
            mesh.RecalculateNormals();
            UnityEngine.Debug.Log($"Vertices:{chunkMesh.vertices.Count}");
            UnityEngine.Debug.Log($"Triangles:{chunkMesh.triangles.Count}");
            UnityEngine.Debug.Log($"Normals:{mesh.normals.Length}");
            
            yield return null;
        }
    }
}