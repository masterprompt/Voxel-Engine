using System;
using System.Collections;
using UnityEngine;


namespace VoxelEngine
{
    public class BlockRenderer : IVoxmapRenderer
    {
        private ChunkMesh _chunkMesh;

        public BlockRenderer(ChunkMesh chunkMesh)
        {
            this._chunkMesh = chunkMesh;
        }

        private bool HasVoxel(Chunk chunk, int x, int y, int z)
        {
            if (x < 0 || y < 0 || z < 0) return false;
            if (x >= chunk.Width || y >= chunk.Height || z >= chunk.Depth) return false;
            return chunk[x, y, z].material !=0;
        }
        
        public void Render(Chunk chunk)
        {
            //    Look at each voxel
            var voxelSize = chunk.VoxelSize;
            var bounds = new Bounds(Vector3.zero, chunk.VoxelSize);
            
            var voxmapStart = chunk.scale * -0.5f;
            //    For each voxel, render all sides of it
            for (var x=0; x<chunk.Width; x++)
            {
                var xmin = voxmapStart.x + voxelSize.x * x;
                for (var y=0; y<chunk.Height; y++)
                {
                    var ymin = voxmapStart.y + voxelSize.y * y;
                    for (var z=0; z<chunk.Depth; z++)
                    {
                        //Debug.Log($"{x},{y},{z}");
                        // look at each surrounding voxel side to see if it needs to be rendered.
                        
                        if(chunk[x,y,z].material==0) continue;
                        var color32 = chunk[x, y, z].ToColor32();
                        var zmin = voxmapStart.z + voxelSize.z * z;
                        var min = new Vector3(xmin, ymin, zmin);
                        var max = min + voxelSize;
                        bounds.SetMinMax(min, max);
                        if (!HasVoxel(chunk, x, y, z - 1)) Cube.RenderQuad(_chunkMesh, bounds, Cube.Sides.Back, color32);
                        if (!HasVoxel(chunk, x, y, z + 1)) Cube.RenderQuad(_chunkMesh, bounds, Cube.Sides.Forward, color32);
                        if (!HasVoxel(chunk, x-1, y, z)) Cube.RenderQuad(_chunkMesh, bounds, Cube.Sides.Left, color32);
                        if (!HasVoxel(chunk, x+1, y, z)) Cube.RenderQuad(_chunkMesh, bounds, Cube.Sides.Right, color32);
                        if (!HasVoxel(chunk, x, y+1, z)) Cube.RenderQuad(_chunkMesh, bounds, Cube.Sides.Up, color32);
                        if (!HasVoxel(chunk, x, y-1, z)) Cube.RenderQuad(_chunkMesh, bounds, Cube.Sides.Down, color32);


                    }
                }
            }
        }
    }
}