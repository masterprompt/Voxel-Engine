using System;
using UnityEngine;

namespace VoxelEngine.Loaders
{
    public class PerlinNoise : IVoxmapLoader
    {
        public void Load(Voxmap voxmap)
        {
            var voxelSize = voxmap.VoxelSize;
            for(var x=0;x < voxmap.Width; x++)
            for(var z=0;z < voxmap.Depth; z++)
            {
                var xSample = voxelSize.x * x;
                var zSample = voxelSize.z * z;
                var ySample = Mathf.PerlinNoise(xSample, zSample);
                float yMax = Mathf.FloorToInt(ySample * voxmap.Height);
                for (var y = 0; y < yMax; y++)
                    voxmap[x,y,z] = Voxel.Create(1);
            }
        }
    }
}