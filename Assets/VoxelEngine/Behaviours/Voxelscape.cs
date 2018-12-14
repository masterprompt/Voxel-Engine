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
            
            
            var filePath = UnityEngine.Application.dataPath + "/voxmap.json";
            var voxelmap = new Chunk(size, size, size);
            var loader = new FillLoader();
            //var loader = new FileLoader(filePath);
            //var loader = new PerlinNoise();
            loader.Load(voxelmap);
            var chunk = new ChunkMesh();
            var renderer = new BlockRenderer(chunk);
            renderer.Render(voxelmap);
            UnityEngine.Mesh mesh = GetComponent<MeshFilter>().mesh;
            mesh.Clear();
            mesh.vertices = chunk.vertices.ToArray();
            mesh.triangles = chunk.triangles.ToArray();
            Debug.Log("Triangles:" + chunk.triangles.Count);
            //mesh.RecalculateNormals();
            if (optimize) MeshUtility.Optimize(mesh);
            
        }
    }
}