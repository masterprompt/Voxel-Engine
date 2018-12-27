using UnityEngine;
using System;

namespace VoxelEngine
{
    /// <summary>
    /// A data object containing Voxels
    /// </summary>
    [Serializable]
    public class Chunk
    {
        public Voxel[] voxels;
        [SerializeField]
        private int _width;
        [SerializeField]
        private int _height;
        [SerializeField]
        private int _depth;
        public int Width => _width;
        public int Height => _height;
        public int Depth => _depth;
        public Vector3 scale;

        public Vector3 VoxelSize => new Vector3(scale.x / Width, scale.y / Height, scale.z / Depth);

        public Chunk(int width, int height, int depth)
        {
            _width = width;
            _height = height;
            _depth = depth;
            voxels = new Voxel[width * height * depth];
            scale = Vector3.one;
        }

        public Voxel this[int x, int y, int z]
        {
            get { return voxels[GetIndex(x, y, z)]; }
            set { voxels[GetIndex(x, y, z)] = value; }
        }

        private int GetIndex(int x, int y, int z)
        {
            return x * Width * Height + y * Height + z;
        }
    }
}