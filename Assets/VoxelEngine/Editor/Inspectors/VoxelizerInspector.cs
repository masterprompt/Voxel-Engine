using UnityEngine;
using System.Collections;
using UnityEditor;

namespace VoxelEngine.Inspectors
{
    [CustomEditor(typeof(Voxelizer))]
    public class VoxelizerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            Voxelizer voxelizer = (Voxelizer)target;
            DrawDefaultInspector();
            if(GUILayout.Button("Build Chunk")) voxelizer.Voxelize();
        }
    }
}