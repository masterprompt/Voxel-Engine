using UnityEngine;

namespace VoxelEngine
{
    public class Cube
    {
        public enum Sides
        {
            Back,
            Left,
            Forward,
            Right,
            Down,
            Up
        }
        //    http://ilkinulas.github.io/development/unity/2016/04/30/cube-mesh-in-unity3d.html
        public static Vector3 GetPoint(Bounds bounds, int index)
        {
            switch (index)
            {
                case 0: return bounds.min;
                case 1: return new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
                case 2: return new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
                case 3: return new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
                case 4: return new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
                case 5: return bounds.max;
                case 6: return new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
                case 7: return new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
                default: return Vector3.zero;
            }
        }

        public static int[] GetQuad(Sides side)
        {
            switch (side)
            {
                case Sides.Back: return new int[] {0, 3, 2, 1}; // back
                case Sides.Left: return new int[] {7, 4, 3, 0}; // left
                case Sides.Forward: return new int[] {6, 5, 4, 7}; // forward
                case Sides.Right: return new int[] {1, 2, 5, 6}; // right
                case Sides.Down: return new int[] {7, 0, 1, 6}; // down
                case Sides.Up: return new int[] {3, 4, 5, 2}; // up
                default: return new int[]{};
            }
        }

        public static void RenderQuad(ChunkMesh chunkMesh, Bounds bounds, Sides side, Color32 color)
        {
            var quadVertices = GetQuad(side);
            chunkMesh.vertices.Add(GetPoint(bounds, quadVertices[0]));
            chunkMesh.vertices.Add(GetPoint(bounds, quadVertices[1]));
            chunkMesh.vertices.Add(GetPoint(bounds, quadVertices[2]));
            chunkMesh.vertices.Add(GetPoint(bounds, quadVertices[3]));
            chunkMesh.colors.Add(color);
            chunkMesh.colors.Add(color);
            chunkMesh.colors.Add(color);
            chunkMesh.colors.Add(color);
            var vertIndex = chunkMesh.vertices.Count - 4;
            chunkMesh.triangles.AddRange(new int[]
            {
                vertIndex,
                vertIndex + 1,
                vertIndex + 2
            });
            chunkMesh.triangles.AddRange(new int[]
            {
                vertIndex + 2,
                vertIndex + 3,
                vertIndex
            });
        }
    }
}