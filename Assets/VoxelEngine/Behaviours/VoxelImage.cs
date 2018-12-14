using UnityEngine;

namespace VoxelEngine
{
    public class VoxelImage : MonoBehaviour
    {
        public Texture2D texture;

        public void Start()
        {
            var width = texture.width;
            var height = texture.height;
            var chunk = new Chunk(width, height, height);
            for(var x=0; x<width; x++)
            for (var z = 0; z < height; z++)
            {
                var pixel = texture.GetPixel(x, z);
                //Debug.Log($"{x},{z}: {pixel}");
                var zMax = Mathf.FloorToInt(pixel.r * height);
                for (var y = 0; y < zMax; y++)
                    chunk[x, y, z] = Voxel.Create(1);
            }
            
            var chunkMesh = new ChunkMesh();
            var renderer = new BlockRenderer(chunkMesh);
            renderer.Render(chunk);
            var mesh = GetComponent<MeshFilter>().mesh;
            mesh.Clear();
            mesh.vertices = chunkMesh.vertices.ToArray();
            mesh.triangles = chunkMesh.triangles.ToArray();
        }
    }
}