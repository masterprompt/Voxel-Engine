using System.IO;
using UnityEngine;

namespace VoxelEngine.Loaders
{
    public class FileLoader : IVoxmapLoader
    {
        private string filePath;
        public FileLoader(string filePath)
        {
            this.filePath = filePath;
        }
        
        public void Load(Chunk chunk)
        {
            StreamReader reader = new StreamReader(filePath);
            var json = reader.ReadToEnd();
            JsonUtility.FromJsonOverwrite(json, chunk);
            reader.Close();
        }
    }
}