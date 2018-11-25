using UnityEngine;

namespace VoxelEngine
{
    public class FullRenderer : IVoxmapRenderer
    {
        private Chunk chunk;

        public FullRenderer(Chunk chunk)
        {
            this.chunk = chunk;
        }
        
        public void Render(Voxmap voxmap)
        {
            var voxelSize = voxmap.VoxelSize;
            var voxmapStart = voxmap.scale * -0.5f;
            //var quadStart = 1f * -0.5f;
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
                        if(voxmap[x,y,z].m==0) continue;
                        var zmin = voxmapStart.z + (voxelSize.z * z);
                        var zmax = zmin + voxelSize.z;
                        AddQuad(chunk, CreateFaceX(xmin, xmax, ymin, ymax, zmin));
                        AddQuad(chunk, CreateFaceX(xmax, xmin, ymin, ymax, zmax));
                        AddQuad(chunk, CreateFaceZ(zmin, zmax, ymin, ymax, xmax));
                        AddQuad(chunk, CreateFaceZ(zmax, zmin, ymin, ymax, xmin));
                        AddQuad(chunk, CreateFaceY(xmin, xmax, zmin, zmax, ymax));
                        AddQuad(chunk, CreateFaceY(xmax, xmin, zmin, zmax, ymin));
                    }
                }
            }
        }

        private Vector3[] CreateFaceX(float xmin, float xmax, float ymin, float ymax, float z)
        {
            return new Vector3[]
            {
                new Vector3(xmin, ymin, z),
                new Vector3(xmin, ymax, z),
                new Vector3(xmax, ymax, z),
                new Vector3(xmax, ymin, z)
            };
        }
        
        private Vector3[] CreateFaceY(float xmin, float xmax, float zmin, float zmax, float y)
        {
            return new Vector3[]
            {
                new Vector3(xmin, y, zmin),
                new Vector3(xmin, y, zmax),
                new Vector3(xmax, y, zmax),
                new Vector3(xmax, y, zmin)
            };
        }
        
        private Vector3[] CreateFaceZ(float zmin, float zmax, float ymin, float ymax, float x)
        {
            return new Vector3[]
            {
                new Vector3(x, ymin, zmin),
                new Vector3(x, ymax, zmin),
                new Vector3(x, ymax, zmax),
                new Vector3(x, ymin, zmax)
            };
        }

        private void AddQuad(Chunk chunk, Vector3[] vertices)
        {
            chunk.vertices.Add(vertices[0]);
            chunk.vertices.Add(vertices[1]);
            chunk.vertices.Add(vertices[2]);
            chunk.vertices.Add(vertices[3]);
                    
            chunk.triangles.Add(chunk.vertices.Count - 4);
            chunk.triangles.Add(chunk.vertices.Count - 3);
            chunk.triangles.Add(chunk.vertices.Count - 2);
            chunk.triangles.Add(chunk.vertices.Count - 2);
            chunk.triangles.Add(chunk.vertices.Count - 1);
            chunk.triangles.Add(chunk.vertices.Count - 4);
        }
    }
}