using UnityEngine;

namespace VoxelEngine
{
    [CreateAssetMenu(menuName="Scriptables/VoxelEngine/Chunk")]
    public class ScriptableChunk : ScriptableObject
    {
        public Chunk chunk;
    }
}