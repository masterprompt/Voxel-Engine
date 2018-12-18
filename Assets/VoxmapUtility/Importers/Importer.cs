using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VoxmapUtility.Exporters;

namespace VoxmapUtility.Importers
{
    public static class Importer
    {
        public static void Import(Voxmap voxmap, byte[] bytes)
        {
            using(MemoryStream stream = new MemoryStream(bytes))
            {
                var encoding = new UnicodeEncoding();
                var sr = new BinaryReader(stream, encoding);
                try
                {
                    var header = sr.ReadString();
                    if (header != Exporter.HEADER) throw new Exception("Not valid format");
                    var version = sr.ReadString();
                    // TODO: Do conditional version logic here
                    if (version != Exporter.CURRENT_VERSION)
                        throw new Exception($"Expected version {Exporter.CURRENT_VERSION} but read {version}");


                    voxmap.width = sr.ReadInt32();
                    voxmap.height = sr.ReadInt32();
                    voxmap.depth = sr.ReadInt32();
                    var sliceCount = sr.ReadInt32();
                    var slices = new List<byte[]>();
                    for (var i = 0; i < sliceCount; i++)
                    {
                        var sliceSize = sr.ReadInt32();
                        var slice = sr.ReadBytes(sliceSize);
                        slices.Add(slice);
                    }
                    voxmap.slices = slices.ToArray();
                }
                finally
                {
                    sr.Dispose();
                }
            }
        }
    }
}