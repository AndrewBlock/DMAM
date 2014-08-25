using System;
using System.Runtime.InteropServices;

namespace DMAM.Interop.IO
{
    public struct _CDROM_TOC
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType=UnmanagedType.U1, SizeConst=2)]
        public byte[] Length;
        public byte FirstTrack;
        public byte LastTrack;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType=UnmanagedType.Struct, SizeConst=100)]
        public _TRACK_DATA[] TrackData;
    }
}
