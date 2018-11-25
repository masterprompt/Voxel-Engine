namespace VoxelEngine.Loaders
{
    public class FillLoader : IVoxmapLoader
    {
        public void Load(Voxmap voxmap)
        {
            for(var x=0; x<voxmap.Width; x++)
                for(var y=0; y<voxmap.Height; y++)
                for (var z = 0; z < voxmap.Depth; z++)
                    voxmap[x, y, z] = Voxel.Create(1); 
        }
    }
}