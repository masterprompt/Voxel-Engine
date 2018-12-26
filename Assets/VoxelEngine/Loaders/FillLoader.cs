using VoxelEngine.Utilities;

namespace VoxelEngine.Loaders
{
    public class FillLoader : IVoxmapLoader
    {
        public void Load(Chunk chunk)
        {
            for(var x=0; x<chunk.Width; x++)
                for(var y=0; y<chunk.Height; y++)
                for (var z = 0; z < chunk.Depth; z++)
                    chunk[x, y, z] = Voxel.Create(Material.RandomMaterial()); 
        }
    }
}