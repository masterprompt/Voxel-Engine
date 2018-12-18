using System;
using UnityEditor;
using UnityEngine;
using VoxelEngine.Loaders;
using VoxmapUtility;
using System.Diagnostics;

namespace VoxelEngine
{
    /// <summary>
    /// A 3D object containing Voxelchunks
    /// </summary>
    public class Voxelscape : MonoBehaviour
    {
        public int size = 10;
        public bool optimize;
        
        public void Start()
        {
            
            
            //var filePath = UnityEngine.Application.dataPath + "/voxmap.json";
            var chunk = new Chunk(size, size, size);
            //var loader = new FillLoader();
            //var loader = new FileLoader(filePath);
            var loader = new PerlinNoise();
            loader.Load(chunk);


            var voxmap = chunk.ToVoxmap();
            var bytes = voxmap.Export();
            UnityEngine.Debug.Log($"Voxelmap Size:{bytes.Length}");
            voxmap = new Voxmap();
            voxmap.Import(bytes);
            chunk = voxmap.ToChunk();
            
            var chunkMesh = new ChunkMesh();
            var renderer = new BlockRenderer(chunkMesh);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            renderer.Render(chunk);
            stopwatch.Stop();
            UnityEngine.Debug.Log($"Build Time:{stopwatch.ElapsedMilliseconds} ms");
            var mesh = GetComponent<MeshFilter>().mesh;
            mesh.Clear();
            mesh.vertices = chunkMesh.vertices.ToArray();
            mesh.triangles = chunkMesh.triangles.ToArray();
            UnityEngine.Debug.Log("Triangles:" + chunkMesh.triangles.Count);
            //mesh.RecalculateNormals();
            if (optimize) MeshUtility.Optimize(mesh);
            
        }
    }
}