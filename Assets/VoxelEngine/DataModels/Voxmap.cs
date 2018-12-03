using UnityEngine;
using System;

namespace VoxelEngine
{
    /// <summary>
    /// A data object containing Voxels
    /// </summary>
    [Serializable]
    public class Voxmap
    {
        public Voxel[] voxels;
        public int Width { get; }
        public int Height { get; }
        public int Depth { get; }
        public Vector3 scale;

        public Vector3 VoxelSize => new Vector3(scale.x / Width, scale.y / Height, scale.z / Depth);

        public Voxmap(int width, int height, int depth)
        {
            Width = width;
            Height = height;
            Depth = depth;
            voxels = new Voxel[width * height * depth];
            scale = Vector3.one;
        }

        public Voxel this[int x, int y, int z]
        {
            get { return voxels[GetIndex(x, y, z)]; }
            set { voxels[GetIndex(x, y, z)] = value; }
        }

        public int GetIndex(int x, int y, int z)
        {
            //Debug.Log($"{x}, {y}, {z} -> {(x + Width * (y + Depth * z))}");
            return x + Width * (y + Depth * z);
        }
    }
}