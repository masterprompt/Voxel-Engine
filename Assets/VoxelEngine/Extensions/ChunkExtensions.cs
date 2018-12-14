using System.Collections.Generic;
using UnityEngine;

namespace VoxelEngine
{
    public static class ChunkExtensions
    {
        public static Voxelmap ToVoxelmap(this Chunk chunk)
        {
            var slices = new List<Voxelmap.Slice>();
            //    Add the png slices
            var image = new Texture2D(chunk.Width, chunk.Height, TextureFormat.Alpha8, false);
            for (var y = 0; y < chunk.Height; y++)
            {
                
                for(var x=0; x<chunk.Width; x++)
                for (var z = 0; z < chunk.Depth; z++)
                {
                    var color = new Color(chunk[x,y,z].m / 255f, 0, 0, 0);
                    if(chunk[x,y,z].m>0) Debug.Log($"Color:{color}");
                    image.SetPixel(x, z, color);
                    
                }
                var slice = new Voxelmap.Slice()
                {
                    bytes = image.EncodeToPNG()
                };
                slices.Add(slice);
            }
            Debug.Log($"Slices:{slices.Count}");
            return new Voxelmap()
            {
                slices = slices.ToArray(),
                width = chunk.Width,
                height = chunk.Height,
                depth = chunk.Depth
            };
        }
    }
}