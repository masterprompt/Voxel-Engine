using UnityEngine;

namespace VoxelEngine
{
    public class FullRenderer : IVoxmapRenderer
    {
        private ChunkMesh _chunkMesh;

        public FullRenderer(ChunkMesh chunkMesh)
        {
            this._chunkMesh = chunkMesh;
        }
        
        public void Render(Chunk chunk)
        {
            var voxelSize = chunk.VoxelSize;
            var voxmapStart = chunk.scale * -0.5f;
            //var quadStart = 1f * -0.5f;
            //    For each voxel, render all sides of it
            for (var x=0; x<chunk.Width; x++)
            {
                var xmin = voxmapStart.x + (voxelSize.x * x);
                var xmax = xmin + voxelSize.x;
                for (var y=0; y<chunk.Height; y++)
                {
                    var ymin = voxmapStart.y + (voxelSize.y * y);
                    var ymax = ymin + voxelSize.y;
                    for (var z=0; z<chunk.Depth; z++)
                    {
                        if(chunk[x,y,z].m==0) continue;
                        var zmin = voxmapStart.z + (voxelSize.z * z);
                        var zmax = zmin + voxelSize.z;
                        AddQuad(_chunkMesh, CreateFaceX(xmin, xmax, ymin, ymax, zmin));
                        AddQuad(_chunkMesh, CreateFaceX(xmax, xmin, ymin, ymax, zmax));
                        AddQuad(_chunkMesh, CreateFaceZ(zmin, zmax, ymin, ymax, xmax));
                        AddQuad(_chunkMesh, CreateFaceZ(zmax, zmin, ymin, ymax, xmin));
                        AddQuad(_chunkMesh, CreateFaceY(xmin, xmax, zmin, zmax, ymax));
                        AddQuad(_chunkMesh, CreateFaceY(xmax, xmin, zmin, zmax, ymin));
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

        private void AddQuad(ChunkMesh chunkMesh, Vector3[] vertices)
        {
            chunkMesh.vertices.Add(vertices[0]);
            chunkMesh.vertices.Add(vertices[1]);
            chunkMesh.vertices.Add(vertices[2]);
            chunkMesh.vertices.Add(vertices[3]);
                    
            chunkMesh.triangles.Add(chunkMesh.vertices.Count - 4);
            chunkMesh.triangles.Add(chunkMesh.vertices.Count - 3);
            chunkMesh.triangles.Add(chunkMesh.vertices.Count - 2);
            chunkMesh.triangles.Add(chunkMesh.vertices.Count - 2);
            chunkMesh.triangles.Add(chunkMesh.vertices.Count - 1);
            chunkMesh.triangles.Add(chunkMesh.vertices.Count - 4);
        }
    }
}