using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;
using System.IO;
using System.Threading.Tasks;

namespace VoxelEngine
{
    public static class ChunkMeshExtensions
    {
        public static void Optimize(this ChunkMesh chunkMesh)
        {
            var vertices = new List<Vector3>();
            var colors = new List<Color32>();
            
            //    Map out all the triangles
            var triangles = new List<Triangle>();
            for(var t=0; t<chunkMesh.triangles.Count; t=t+3)
                triangles.Add(new Triangle(chunkMesh.triangles[t],chunkMesh.triangles[t+1],chunkMesh.triangles[t+2]));

            var optimizedIndices = new List<int>();

            for (var index = 0; index < chunkMesh.vertices.Count; index++)
            {
                if(optimizedIndices.Contains(index)) continue;
                
                var vertex = chunkMesh.vertices[index];
                var color = chunkMesh.colors[index];
                
                //    Find all indices that are duplicates
                var indices = new List<int>();
                for(var i=0; i<chunkMesh.vertices.Count; i++)
                    if (chunkMesh.vertices[i] == vertex)
                    {
                        optimizedIndices.Add(i);
                        indices.Add(i);
                    }
                        
                
                //    Add to optimized list
                vertices.Add(vertex);
                colors.Add(color);
                var newIndex = vertices.Count-1;
                
                //    Replace triangle indices that share that vertex
                foreach(var dupe in indices)
                foreach(var triangle in triangles)
                    for(var i=0; i<3; i++)
                        if (triangle.sourceIndicies[i] == dupe)
                            triangle.targetIndices[i] = newIndex;
                    
                
            }

            //    Rebuild the triangles array
            var newTriangles = new List<int>();
            foreach (var triangle in triangles)
                for(var i=0; i<3; i++)
                    newTriangles.Add(triangle.targetIndices[i]);
            
            chunkMesh.triangles = newTriangles.ToList();
            chunkMesh.vertices = vertices.ToList();
            chunkMesh.colors = colors.ToList();



        }

    }

    public struct Triangle
    {
        public int[] sourceIndicies;
        public int[] targetIndices;
        public Triangle(int v1, int v2, int v3)
        {
            sourceIndicies = new int[]
            {
                v1,
                v2,
                v3
            };
            targetIndices = new int[]
            {
                0, 0, 0
            };
        }
    }
}