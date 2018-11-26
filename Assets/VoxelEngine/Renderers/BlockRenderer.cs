namespace VoxelEngine
{
    public class BlockRenderer : IVoxmapRenderer
    {
        private Chunk chunk;

        public BlockRenderer(Chunk chunk)
        {
            this.chunk = chunk;
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