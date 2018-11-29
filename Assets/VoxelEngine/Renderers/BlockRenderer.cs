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
            private const float p = 0.5f;
            //    http://ilkinulas.github.io/development/unity/2016/04/30/cube-mesh-in-unity3d.html
            public static Vector3 GetPoint(int index)
            {
                switch (index)
                {
                    case 0: return new Vector3(-p, -p, -p);
                    case 1: return new Vector3(p, -p, -p);
                    case 2: return new Vector3(p, p, -p);
                    case 3: return new Vector3(-p, p, -p);
                    case 4: return new Vector3(-p, p, p);
                    case 5: return new Vector3(p, p, p);
                    case 6: return new Vector3(p, -p, p);
                    case 7: return new Vector3(-p, -p, p);
                    default: return Vector3.zero;
                }
            }

            public static int[] GetTriangle(int index)
            {
                switch (index)
                {
                    case 0: return new int[] {0, 3, 2, 1}; // back
                    case 1: return new int[] {7, 4, 3, 0}; // left
                    case 2: return new int[] {6, 5, 4, 7}; // forward
                    case 3: return new int[] {1, 2, 5, 6}; // right
                    case 4: return new int[] {7, 0, 1, 6}; // down
                    case 5: return new int[] {3, 4, 5, 2}; // up
                    default: return new int[]{};
                }
            }
        }
        
        
        public void Render(Voxmap voxmap)
        {
            //    Look at each voxel
            var voxelSize = voxmap.VoxelSize;
            var voxmapStart = voxmap.scale * -0.5f;
            //    For each voxel, render all sides of it
            for (var x=0; x<voxmap.Width; x++)
            {
                var xmin = voxmapStart.x + (voxelSize.x * x);
                var xmax = xmin + voxelSize.x;
                for (var y=0; y<voxmap.Height; y++)
                {
                    var ymin = voxmapStart.y + (voxelSize.y * y);
                    var ymax = ymin + voxelSize.y;
                    for (var z=0; z<voxmap.Depth; z++)
                    {
                        // look at each surrounding voxel side to see if it needs to be rendered.
                        
                        if(voxmap[x,y,z].m==0) continue;
                        var zmin = voxmapStart.z + (voxelSize.z * z);
                        var zmax = zmin + voxelSize.z;

                    }
                }
            }
        }
    }
}