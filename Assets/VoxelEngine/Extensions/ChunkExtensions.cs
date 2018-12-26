using System.Collections.Generic;
using UnityEngine;
using VoxmapUtility;

namespace VoxelEngine
{
    public static class ChunkExtensions
    {
        
        public static Voxmap ToVoxmap(this Chunk chunk)
        {
            var slices = new List<byte[]>();
            //    Add the png slices
            var image = new Texture2D(chunk.Width, chunk.Height, TextureFormat.ARGB32, false);
            for (var y = 0; y < chunk.Height; y++)
            {
                for(var x=0; x<chunk.Width; x++)
                for (var z = 0; z < chunk.Depth; z++)
                {
                    var color = chunk[x, y, z].ToColor32();
                    image.SetPixel(x, z, color);

                }
                slices.Add(image.EncodeToPNG());
            }
            return new Voxmap
            {
                slices = slices.ToArray(),
                width = chunk.Width,
                height = chunk.Height,
                depth = chunk.Depth
            };
        }

        public static bool Save(this Chunk chunk, string filename)
        {
            return chunk.ToVoxmap().Save(filename);
        }
    }
}