using System;
using UnityEngine;

namespace VoxelEngine.Loaders
{
    public class PerlinNoise : IVoxmapLoader
    {
        public void Load(Chunk chunk)
        {
            var voxelSize = chunk.VoxelSize;
            for(var x=0;x < chunk.Width; x++)
            for(var z=0;z < chunk.Depth; z++)
            {
                var xSample = voxelSize.x * x;
                var zSample = voxelSize.z * z;
                var ySample = Mathf.PerlinNoise(xSample, zSample);
                float yMax = Mathf.FloorToInt(ySample * chunk.Height);
                for (var y = 0; y < yMax; y++)
                    chunk[x,y,z] = Voxel.Create(1);
            }
        }
    }
}