using System;
using UnityEngine;

namespace VoxelEngine.Loaders
{
    public class SphereLoader: IVoxmapLoader
    {
       
        public void Load(Chunk chunk)
        {
            //    Go through each voxel in the chunk and test if it in the given distance from the center
            
            //    Calculate center
            var size = chunk.Width;
            var center = new Vector3(size * 0.5f, size * 0.5f, size * 0.5f);
            var radius = Mathf.FloorToInt(size * 0.5f - 1);
            for(var x=0; x<chunk.Width; x++)
            for(var y=0; y<chunk.Height; y++)
            for (var z = 0; z < chunk.Depth; z++)
            {
                var position = new Vector3(x, y, z);
                var distanceToCenter = Vector3.Distance(position, center);
                if(Mathf.FloorToInt(distanceToCenter) > radius) continue;
                
                chunk[x,y,z] = Voxel.Create(Utilities.Material.RandomMaterial());
            }
        }
    }
}