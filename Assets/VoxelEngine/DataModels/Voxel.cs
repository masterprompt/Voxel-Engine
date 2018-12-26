using System;

namespace VoxelEngine
{
    /// <summary>
    /// A data object continaining a Volumetrix pixel
    /// </summary>
    [Serializable]
    public struct Voxel
    {
        /// <summary>
        /// Material
        /// </summary>
        public int material;

        public static Voxel Create(int material)
        {
            return new Voxel
            {
                material = material
            };
        }
    }
}