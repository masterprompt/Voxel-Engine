using System.Collections.Generic;
using UnityEngine;
/*
 .csa (chunk slice array)
 Format:
 "CSAFILE"
 string: version
 int64: chunk width
 int64: chunk height
 int64: chunk depth
 double: assembly normal X
 double: assembly normal Y
 double: assembly normal Z
 int32: slice count
 int32: slice size
 [bytes]: png encoded image slice
 int32: slice size
 [bytes]: png encoded image slice
 */
namespace VoxelEngine
{
    public class Voxelmap
    {
        private static string HEADER = "VXMFILE";
        private static string VERSION = "1.0.0";
        public int width;
        public int height;
        public int depth;
        public Slice[] slices;

        public class Slice
        {
            // png encoded byte array
            public byte[] bytes;
        }
    }
}