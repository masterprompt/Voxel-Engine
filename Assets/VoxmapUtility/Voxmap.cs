using System;
using System.IO;

namespace VoxmapUtility
{
    public class Voxmap
    {
        public int width;
        public int height;
        public int depth;
        public byte[][] slices;

        public byte[] Export()
        {
            return Exporters.Exporter.Export(this);
        }

        public void Import(byte[] bytes)
        {
            Importers.Importer.Import(this, bytes);
        }

        public static Voxmap Create(byte[] bytes)
        {
            var voxmap = new Voxmap();
            voxmap.Import(bytes);
            return voxmap;
        }

        public bool Save(string filename)
        {
            try
            {
                var bytes = Export();
                File.WriteAllBytes(filename, bytes);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing file: {ex}");
                return false;
            }
        }

        public bool Load(string filename)
        {
            try
            {
                var bytes = File.ReadAllBytes(filename);
                Import(bytes);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing file: {ex}");
                return false;
            }
        }
    }
}