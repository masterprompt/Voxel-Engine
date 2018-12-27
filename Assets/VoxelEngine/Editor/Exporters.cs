using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using VoxelEngine.Loaders;
using VoxmapUtility;

namespace VoxelEngine
{
    public class Exporters
    {
        private static string dataPath;
        static Exporters()
        {
            dataPath = Application.dataPath;
        }
        
        [MenuItem("Voxel Engine/Export/Sphere")]
        static void ExportSphere()
        {
            var size = 50;
            var chunk = new Chunk(size, size, size);
            var loader = new SphereLoader();
            loader.Load(chunk);
            Export(chunk, "sphere");
        }
        
        [MenuItem("Voxel Engine/Export/Terrain")]
        static void ExportTerrain()
        {
            var size = 50;
            var chunk = new Chunk(size, size, size);
            var loader = new PerlinNoise();
            loader.Load(chunk);
            Export(chunk, "terrain");
        }
        
        [MenuItem("Voxel Engine/Export/Cube")]
        static void ExportCube()
        {
            var size = 50;
            var chunk = new Chunk(size, size, size);
            var loader = new FillLoader();
            loader.Load(chunk);
            Export(chunk, "cube");
        }

        [MenuItem("Voxel Engine/Export/Selected Object")]
        static void SelectedObject()
        {
            if (!IsSelectedObject()) return;
            ((GameObject) Selection.activeObject).GetComponent<Voxelizer>().Voxelize();
        }

        [MenuItem("Voxel Engine/Export/Selected Object", true)]
        static bool IsSelectedObject()
        {
            return Selection.activeObject.GetType() == typeof(GameObject)
                   && ((GameObject) Selection.activeObject).GetComponent<Voxelizer>() != null;
        }

        private static void Export(Chunk chunk, string name = "voxmap")
        {
            var filename = EditorUtility.SaveFilePanel("Save sphere voxmap", dataPath, $"{name}.bytes", "bytes");
            if (filename.Length == 0) return;
            if (dataPath != Path.GetDirectoryName(filename)) dataPath = Path.GetDirectoryName(filename);
            if (chunk.Save(filename)) Debug.Log($"Exported {filename}");
        }
    }
}

