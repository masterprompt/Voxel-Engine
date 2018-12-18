using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VoxmapUtility.Exporters
{
    public static class Exporter
    {
        public const string HEADER = "VMP";
        public const string CURRENT_VERSION = "1.0.0";
        
        // TODO: Add conditional version logic
        public static byte[] Export(Voxmap voxmap, string version = CURRENT_VERSION)
        {
            using(MemoryStream stream = new MemoryStream())
            {
                var encoding = new UnicodeEncoding();
                var writer = new BinaryWriter(stream, encoding);
                try
                {
                    writer.Write(HEADER);
                    writer.Write(version);
                    writer.Write(voxmap.width);
                    writer.Write(voxmap.height);
                    writer.Write(voxmap.depth);
                    writer.Write(voxmap.slices.Length);
                    foreach (var sliceBytes in voxmap.slices)
                    {
                        writer.Write(sliceBytes.Length);
                        writer.Write(sliceBytes);
                    }
                    writer.Flush();
                    return stream.ToArray();
                }
                finally
                {
                    writer.Dispose();
                }
            }
        }
    }
}