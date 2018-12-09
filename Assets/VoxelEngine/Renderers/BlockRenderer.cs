using System.Collections;
using UnityEngine;


namespace VoxelEngine
{
    public class BlockRenderer : IVoxmapRenderer
    {
        private Chunk chunk;

        public BlockRenderer(Chunk chunk)
        {
            this.chunk = chunk;
        }

        
        private class Cube
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

            public static void RenderQuad(Chunk chunk, Bounds bounds, Sides side)
            {
                var quadVertices = GetQuad(side);
                chunk.vertices.Add(GetPoint(bounds, quadVertices[0]));
                chunk.vertices.Add(GetPoint(bounds, quadVertices[1]));
                chunk.vertices.Add(GetPoint(bounds, quadVertices[2]));
                chunk.vertices.Add(GetPoint(bounds, quadVertices[3]));
                var vertIndex = chunk.vertices.Count - 4;
                chunk.triangles.AddRange(new int[]
                {
                    vertIndex,
                    vertIndex + 1,
                    vertIndex + 2
                });
                chunk.triangles.AddRange(new int[]
                {
                    vertIndex + 2,
                    vertIndex + 3,
                    vertIndex
                });
            }
        }


        private bool HasVoxel(Voxmap voxmap, int x, int y, int z)
        {
            if (x < 0 || y < 0 || z < 0) return true;
            if (x >= voxmap.Width || y >= voxmap.Height || z >= voxmap.Depth) return true;
            return voxmap[x, y, z].m > 0;
        }
        
        public void Render(Voxmap voxmap)
        {
            //    Look at each voxel
            var voxelSize = voxmap.VoxelSize;
            var bounds = new Bounds(Vector3.zero, voxmap.VoxelSize);
            
            var voxmapStart = voxmap.scale * -0.5f;
            //    For each voxel, render all sides of it
            for (var x=0; x<voxmap.Width; x++)
            {
                
                var xmin = voxmapStart.x + voxelSize.x * x;
                for (var y=0; y<voxmap.Height; y++)
                {
                    var ymin = voxmapStart.y + voxelSize.y * y;
                    for (var z=0; z<voxmap.Depth; z++)
                    {
                        // look at each surrounding voxel side to see if it needs to be rendered.
                        
                        if(voxmap[x,y,z].m==0) continue;
                        var zmin = voxmapStart.z + voxelSize.z * z;
                        bounds.min = new Vector3(xmin, ymin, zmin);
                        
                        //    Back Voxel
                        if (HasVoxel(voxmap, x, y, z - 1))
                        {
                            Debug.Log(bounds.ToString());
                            Cube.RenderQuad(chunk, bounds, Cube.Sides.Back);
                        }
                            

                    }
                }
            }
        }
    }
}