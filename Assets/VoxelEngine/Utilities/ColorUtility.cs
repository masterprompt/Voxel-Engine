using UnityEngine;

namespace VoxelEngine.Utilities
{
    public static class ColorUtility
    {
        public static Color32 GetDistinctColor(int level)
        {
            level = Mathf.Clamp(level, 0, 20);
            switch (level)
            {
                case 0: return new Color32(230, 25, 75, 255);
                case 1: return new Color32(60, 180, 75, 255);
                case 2: return new Color32(255, 225, 25, 255);
                case 3: return new Color32(0, 130, 200, 255);
                case 4: return new Color32(245, 130, 48, 255);
                case 5: return new Color32(145, 30, 180, 255);
                case 6: return new Color32(70, 240, 240, 255);
                case 7: return new Color32(240, 50, 230, 255);
                case 8: return new Color32(210, 245, 60, 255);
                case 9: return new Color32(250, 190, 190, 255);
                case 10: return new Color32(0, 128, 128, 255);
                case 11: return new Color32(230, 190, 255, 255);
                case 12: return new Color32(170, 110, 40, 255);
                case 13: return new Color32(255, 250, 200, 255);
                case 14: return new Color32(128, 0, 0, 255);
                case 15: return new Color32(170, 255, 195, 255);
                case 16: return new Color32(128, 128, 0, 255);
                case 17: return new Color32(255, 215, 180, 255);
                case 18: return new Color32(0, 0, 128, 255);
                case 19: return new Color32(128, 128, 128, 255);
                case 20: return new Color32(255, 255, 255, 255);
                default: return new Color32(255, 255, 255, 255);
            }
        }
    }
}