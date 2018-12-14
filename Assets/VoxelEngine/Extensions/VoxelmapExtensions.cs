using UnityEngine;

namespace VoxelEngine
{
    public static class VoxelmapExtensions
    {
        public static Chunk ToChunk(this Voxelmap voxelmap)
        {
            var chunk = new Chunk(voxelmap.width, voxelmap.height, voxelmap.depth);
            //    Assemble slices
            for (var y = 0; y < chunk.Height; y++)
            {
                var slice = voxelmap.slices[y];
                var image = new Texture2D(voxelmap.width, voxelmap.depth);
                image.LoadImage(slice.bytes);
                for(var x=0; x<chunk.Width; x++)
                for (var z = 0; z < chunk.Depth; z++)
                    chunk[x, y, z] = Voxel.Create(Mathf.FloorToInt(image.GetPixel(x, z).r * 255));
            }

            return chunk;
        }
    }
}