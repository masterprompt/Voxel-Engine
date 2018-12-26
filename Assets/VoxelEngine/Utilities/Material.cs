using System;
using UnityEngine;

namespace VoxelEngine.Utilities
{
    public class Material
    {
        public static int RandomMaterial()
        {
            var r = (byte)Mathf.FloorToInt(UnityEngine.Random.Range(0, 255));
            var g = (byte)Mathf.FloorToInt(UnityEngine.Random.Range(0, 255));
            var b = (byte)Mathf.FloorToInt(UnityEngine.Random.Range(0, 255));
            var material = BitConverter.ToInt32(new byte[] {r, g, b, 255}, 0);
            return material;
        }
    }
}