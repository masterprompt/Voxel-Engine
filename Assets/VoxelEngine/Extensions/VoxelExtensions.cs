using System;
using UnityEngine;

namespace VoxelEngine
{
    public static class VoxelExtensions
    {
        public static Color32 ToColor32(this Voxel voxel)
        {
            var colorBytes = BitConverter.GetBytes(voxel.material);
            return new Color32(colorBytes[0], colorBytes[1], colorBytes[2], colorBytes[3]);
        }

        public static Voxel ToVoxel(this Color32 color)
        {
            var material = BitConverter.ToInt32(new [] {color.r, color.g, color.b, color.a }, 0);
            return Voxel.Create(material);
        }
        
        public static Voxel ToVoxel(this Color color)
        {
            var material = BitConverter.ToInt32(new [] {(byte)Mathf.FloorToInt(color.r * 255), (byte)Mathf.FloorToInt(color.g * 255), (byte)Mathf.FloorToInt(color.b * 255), (byte)255 }, 0);
            return Voxel.Create(material);
        }
    }
}