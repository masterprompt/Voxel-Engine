using System;
using UnityEngine;
using VoxmapUtility;

namespace VoxelEngine
{
    public static class VoxmapExtensions
    {

        public static Chunk ToChunk(this Voxmap voxmap)
        {
            var chunk = new Chunk(voxmap.width, voxmap.height, voxmap.depth);
            //    Assemble slices
            for (var y = 0; y < chunk.Height; y++)
            {
                var slice = voxmap.slices[y];
                var image = new Texture2D(voxmap.width, voxmap.depth);

                if (!image.LoadImage(slice)) throw new Exception($"Unable to load slice {y}");
                for (var x = 0; x < chunk.Width; x++)
                for (var z = 0; z < chunk.Depth; z++)
                {
                    var color = image.GetPixel(x, z);
                    chunk[x, y, z] = Voxel.Create(Mathf.FloorToInt(color.r * 255));
                }
            }
            return chunk;
        }
    }
}