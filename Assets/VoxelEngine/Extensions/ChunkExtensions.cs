using System.Collections.Generic;
using System.Linq;
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

        public static Bounds GetVoxelBounds(this Chunk chunk, int x, int y, int z)
        {
            var bounds = new Bounds(Vector3.zero, chunk.VoxelSize);
            var voxmapStart = chunk.scale * -0.5f;
            var voxelSize = chunk.VoxelSize;
            var xmin = voxmapStart.x + voxelSize.x * x;
            var ymin = voxmapStart.y + voxelSize.y * y;
            var zmin = voxmapStart.z + voxelSize.z * z;
            var min = new Vector3(xmin, ymin, zmin);
            var max = min + voxelSize;
            bounds.SetMinMax(min, max);
            return bounds;
        }

        public static void DrawBoundsGizmo(this Chunk chunk, Transform transform, Color color)
        {
            var bounds = new Bounds(Vector3.zero, chunk.scale);
            Gizmos.color = color;
            Gizmos.DrawWireCube(transform.TransformPoint(bounds.center), transform.TransformVector(bounds.size));
        }

        public static void DrawChunkGizmo(this Chunk chunk, Transform transform)
        {
            for(var x=0; x<chunk.Width; x++)
                for(var y=0; y<chunk.Height; y++)
                for (var z = 0; z < chunk.Depth; z++)
                {
                    var bounds = chunk.GetVoxelBounds(x, y, z);
                    Gizmos.color = chunk[x, y, z].material == 0 ? Color.black : Color.white;
                    Gizmos.DrawWireCube(transform.TransformPoint(bounds.center), transform.TransformVector(bounds.size));
                }
        }

        public static int VoxelCount(this Chunk chunk)
        {
            return chunk.voxels.Select(v => v.material != 0).Count();
        }
    }
}