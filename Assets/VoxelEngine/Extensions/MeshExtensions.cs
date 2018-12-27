using UnityEngine;

namespace VoxelEngine
{
    public static class MeshExtensions
    {
        public static Vector3[] OffsetVertices(this Mesh mesh, Vector3 offset)
        {
            var vertices = new Vector3[mesh.vertexCount];
            for (var i = 0; i < mesh.vertexCount; i++)
                vertices[i] = mesh.vertices[i] + offset;
            return vertices;
        }
    }
}