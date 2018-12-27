using UnityEngine;

namespace VoxelEngine
{
    public static class BoundsExtensions
    {
        /// <summary>
        /// Returns the child bounds of this bounds as an octree
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="childIndex">0-7</param>
        /// <returns>Bounds</returns>
        public static Bounds OctreeChild(this Bounds bounds, int childIndex)
        {
            var size = bounds.extents;
            var half = size * 0.5f;
            switch (childIndex)
            {
                case 0: return new Bounds(bounds.center - new Vector3(-half.x, half.y, -half.z),size);
                case 1: return new Bounds(bounds.center - new Vector3(-half.x, half.y, half.z),size);
                case 2: return new Bounds(bounds.center - new Vector3(-half.x, -half.y, -half.z),size);
                case 3: return new Bounds(bounds.center - new Vector3(-half.x, -half.y, half.z),size);
                case 4: return new Bounds(bounds.center - new Vector3(half.x, half.y, -half.z),size);
                case 5: return new Bounds(bounds.center - new Vector3(half.x, half.y, half.z),size);
                case 6: return new Bounds(bounds.center - new Vector3(half.x, -half.y, -half.z),size);
                case 7: return new Bounds(bounds.center - new Vector3(half.x, -half.y, half.z),size);
                default: return new Bounds(bounds.center - new Vector3(-half.x, half.y, -half.z),size);
            }
        }
    }
}