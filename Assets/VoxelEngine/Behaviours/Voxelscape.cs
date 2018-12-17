using UnityEditor;
using UnityEngine;
using VoxelEngine.Loaders;

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
            var loader = new FillLoader();
            //var loader = new FileLoader(filePath);
            //var loader = new PerlinNoise();
            loader.Load(chunk);


            var voxelmap = chunk.ToVoxelmap();
            chunk = voxelmap.ToChunk();
            
            var chunkMesh = new ChunkMesh();
            var renderer = new BlockRenderer(chunkMesh);
            renderer.Render(chunk);
            UnityEngine.Mesh mesh = GetComponent<MeshFilter>().mesh;
            mesh.Clear();
            mesh.vertices = chunkMesh.vertices.ToArray();
            mesh.triangles = chunkMesh.triangles.ToArray();
            Debug.Log("Triangles:" + chunkMesh.triangles.Count);
            //mesh.RecalculateNormals();
            if (optimize) MeshUtility.Optimize(mesh);
            
        }
    }
}