using System.Collections.Generic;
using UnityEngine;

namespace VoxelEngine
{
    /// <summary>
    /// Data representation of a mesh, a chunk is always in the scale of 1,1,1
    /// </summary>
    public class ChunkMesh
    {
        public List<Vector3> vertices = new List<Vector3>();
        public List<int> triangles = new List<int>();
        public List<Vector3> normals = new List<Vector3>();
        public List<Color32> colors = new List<Color32>();
    }
}