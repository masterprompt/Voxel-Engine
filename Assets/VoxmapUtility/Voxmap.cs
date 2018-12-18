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
    }
}