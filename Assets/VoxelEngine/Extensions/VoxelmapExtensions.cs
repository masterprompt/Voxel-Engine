using UnityEngine;

namespace VoxelEngine
{
    public static class VoxelmapExtensions
    {
        public static Chunk ToChunk(this Voxelmap voxelmap)
        {
            Debug.Log("============== Loading ================");
            var chunk = new Chunk(voxelmap.width, voxelmap.height, voxelmap.depth);
            //    Assemble slices
            for (var y = 0; y < chunk.Height; y++)
            {
                
                var slice = voxelmap.slices[y];
                Debug.Log($"Slice {y} at {slice.bytes.Length} bytes");
                var image = new Texture2D(voxelmap.width, voxelmap.depth);
                
                var loaded = image.LoadImage(slice.bytes);
                Debug.Log($"Loaded:{loaded}");
                for(var x=0; x<chunk.Width; x++)
                for (var z = 0; z < chunk.Depth; z++)
                {
                    var color = image.GetPixel(x, z);
                    Debug.Log($"GetPixel:{x},{z}:{color}");
                    chunk[x, y, z] = Voxel.Create(Mathf.FloorToInt(color.r * 255));
                }
                    
            }

            return chunk;
        }
    }
}