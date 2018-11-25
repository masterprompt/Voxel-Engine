using System.IO;
using UnityEngine;

namespace VoxelEngine
{
    public class FileRenderer : IVoxmapRenderer
    {
        private string filePath;
        public FileRenderer(string filePath)
        {
            this.filePath = filePath;
        }
        public void Render(Voxmap voxmap)
        {
            StreamWriter writer = new StreamWriter(filePath, false);
            var json = JsonUtility.ToJson(voxmap);
            writer.Write(json);
            writer.Close();
        }
    }
}