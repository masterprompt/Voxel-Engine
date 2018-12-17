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
            var image = new Texture2D(chunk.Width, chunk.Height, TextureFormat.ARGB32, false);
            for (var y = 0; y < chunk.Height; y++)
            {
                Debug.Log($"Slice {y}");
                
                for(var x=0; x<chunk.Width; x++)
                for (var z = 0; z < chunk.Depth; z++)
                {
                    var color = new Color32((byte)chunk[x,y,z].m, 0, 0, 255);
                    Debug.Log($"Set Color:{x},{z}:{color} from {(byte)chunk[x,y,z].m}");
                    image.SetPixel(x, z, color);
                    Debug.Log($"Get Color:{x},{z}:{image.GetPixel(x,z)}");
                    
                }
                //image.Apply();
                var slice = new Voxelmap.Slice
                {
                    bytes = image.EncodeToPNG()
                };
                Debug.Log($"Encoding slice at {slice.bytes.Length} bytes");

                /*
                image.LoadImage(slice.bytes);
                for(var x=0; x<chunk.Width; x++)
                for (var z = 0; z < chunk.Depth; z++)
                {
                    Debug.Log($"Validating Color:{x},{z}:{image.GetPixel(x,z)}");
                    
                }
                */
                
                slices.Add(slice);
            }
            Debug.Log($"Slices:{slices.Count}");
            return new Voxelmap
            {
                slices = slices.ToArray(),
                width = chunk.Width,
                height = chunk.Height,
                depth = chunk.Depth
            };
        }
    }
}