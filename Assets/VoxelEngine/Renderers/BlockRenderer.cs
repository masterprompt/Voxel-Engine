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

        private bool HasVoxel(Voxmap voxmap, int x, int y, int z)
        {
            if (x < 0 || y < 0 || z < 0) return false;
            if (x >= voxmap.Width || y >= voxmap.Height || z >= voxmap.Depth) return false;
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
                        var min = new Vector3(xmin, ymin, zmin);
                        var max = min + voxelSize;
                        bounds.SetMinMax(min, max);
                        if (!HasVoxel(voxmap, x, y, z - 1)) Cube.RenderQuad(chunk, bounds, Cube.Sides.Back);
                        if (!HasVoxel(voxmap, x, y, z + 1)) Cube.RenderQuad(chunk, bounds, Cube.Sides.Forward);
                        if (!HasVoxel(voxmap, x-1, y, z)) Cube.RenderQuad(chunk, bounds, Cube.Sides.Left);
                        if (!HasVoxel(voxmap, x+1, y, z)) Cube.RenderQuad(chunk, bounds, Cube.Sides.Right);
                        if (!HasVoxel(voxmap, x, y+1, z)) Cube.RenderQuad(chunk, bounds, Cube.Sides.Up);
                        if (!HasVoxel(voxmap, x, y-1, z)) Cube.RenderQuad(chunk, bounds, Cube.Sides.Down);


                    }
                }
            }
        }
    }
}